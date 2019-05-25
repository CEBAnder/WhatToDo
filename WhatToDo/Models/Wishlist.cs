using System;
using System.Collections.Generic;

namespace WhatToDo.Models
{
    public partial class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
