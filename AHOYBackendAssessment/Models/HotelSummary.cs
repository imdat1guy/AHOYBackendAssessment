using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models
{
    public class HotelSummary
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float PricePerNight { get; set; }
        public string Thumbnail { get; set; }
        public string Address { get; set; }
        public float Distance { get; set; }
        public float Rating { get; set; }
        public int NumberOfReviews { get; set; }
    }

    public class HotelDetails
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float PricePerNight { get; set; }
        public List<Image> Images { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Facility> Facilities { get; set; }
        public float Rating { get; set; }
        public int NumberOfReviews { get; set; }
    }
}
