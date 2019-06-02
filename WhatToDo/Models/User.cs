using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatToDo.Models
{
    public partial class User : UserLogin
    {
        public User() : base()
        {
            Preference = new HashSet<Preference>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CityId { get; set; }

        [NotMapped]
        public string JWT { get; set; }

        public ICollection<Preference> Preference { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }

        public User(string login, string pwd) : base(login, pwd)
        {
        }
    }
}
