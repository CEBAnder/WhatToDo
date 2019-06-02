using System.ComponentModel.DataAnnotations;

namespace akciocore.Models
{
    public class UserLogin
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public UserLogin()
        {
        }

        public UserLogin(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
