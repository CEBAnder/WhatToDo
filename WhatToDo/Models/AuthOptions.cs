using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace akciocore.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "akcio_core";
        public const string AUDIENCE = "akcio_web_app";
        public const int LIFETIME = 1;
        public static string JWTKey { get; set; }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTKey));
        }
    }
}
