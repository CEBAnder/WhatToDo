using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WhatToDo.Models;

namespace WhatToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly WhatToDoContext _context;

        public LoginController(WhatToDoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// /signup
        /// {
        ///   "Login": "johndoe",
        ///   "Password": "johndoe123"
        /// }
        /// </remarks>
        /// <param name="user">User's to create data</param>
        /// <returns>Full user info</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">Invalid request data error</response>
        [HttpPost]
        [Route("/signup")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostUser([FromBody] UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password, SaltRevision.Revision2Y);
            
            var fullUser = new User(user.Login, passwordHash);

            _context.User.Add(fullUser);
            await _context.SaveChangesAsync();
            fullUser.JWT = Token(fullUser.Login);
            fullUser.Password = String.Empty;

            return StatusCode((int)HttpStatusCode.Created, fullUser);
        }

        /// <summary>
        /// Authorizes user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// /signin
        /// {
        ///   "Login": "johndoe",
        ///   "Password": "johndoe123"
        /// }
        /// </remarks>
        /// <param name="user">User's data to authorize</param>
        /// <returns>Authorized user's data</returns>
        /// <response code="200">Returns authorized user's data</response>
        /// <response code="400">Invalid request data error</response>
        /// <response code="401">Unsuccessful authorization</response>
        [HttpPost]
        [Route("/signin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetUser([FromBody] UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var similarUser = _context.User.FirstOrDefault(u => u.Login == user.Login);
            if (similarUser == null)
                return StatusCode((int)HttpStatusCode.Unauthorized, "No users with same login found");
            if (BCrypt.Net.BCrypt.Verify(user.Password, similarUser.Password))
            {
                similarUser.Password = String.Empty;
                similarUser.JWT = Token(similarUser.Login);
                return Ok(similarUser);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, "Wrong password");
            }
        }

        private string Token(string login)
        {
            var identity = GetIdentity(login);
            var now = DateTime.UtcNow;

            // creating jwt token
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetIdentity(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username)
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}