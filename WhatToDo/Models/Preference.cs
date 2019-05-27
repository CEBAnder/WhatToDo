using System;
using System.Collections.Generic;

namespace WhatToDo.Models
{
    public partial class Preference
    {
        public int PrefId { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public bool? IsLike { get; set; }

        public Place Place { get; set; }
        public User User { get; set; }
    }
}
