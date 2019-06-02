using System;
using System.Collections.Generic;

namespace akciocore.Models
{
    public partial class Place
    {
        public Place()
        {
            PlaceEventConnection = new HashSet<PlaceEventConnection>();
            Preference = new HashSet<Preference>();
        }

        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }

        public ICollection<PlaceEventConnection> PlaceEventConnection { get; set; }
        public ICollection<Preference> Preference { get; set; }
    }
}
