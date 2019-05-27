using System;
using System.Collections.Generic;

namespace WhatToDo.Models
{
    public partial class PlaceEventConnection
    {
        public int Peid { get; set; }
        public int PlaceId { get; set; }
        public int EventId { get; set; }

        public Event Event { get; set; }
        public Place Place { get; set; }
    }
}
