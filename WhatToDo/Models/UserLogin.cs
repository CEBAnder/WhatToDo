using System.ComponentModel.DataAnnotations;

namespace WhatToDo.Models
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
