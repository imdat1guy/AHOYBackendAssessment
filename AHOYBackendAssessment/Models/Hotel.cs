using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models
{
    public class Hotel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float PricePerNight { get; set; }
        public string[] ImagesPaths { get; set; }
        public string Thumbnail { get; set; }
        public float Rating { get; set; }
        public int NumberOfRooms { get; set; }
        public Location location { get; set; }
        public bool IsRecommended { get; set; }
    }
}
