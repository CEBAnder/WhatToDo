using System;
using System.Collections.Generic;

namespace WhatToDo.Models
{
    public partial class User
    {
        public User()
        {
            Preference = new HashSet<Preference>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CityId { get; set; }

        public ICollection<Preference> Preference { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
    }
}
