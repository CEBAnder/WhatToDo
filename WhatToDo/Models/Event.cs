using System;
using System.Collections.Generic;

namespace akciocore.Models
{
    public partial class Event
    {
        public Event()
        {
            PlaceEventConnection = new HashSet<PlaceEventConnection>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PlaceEventConnection> PlaceEventConnection { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
    }
}
