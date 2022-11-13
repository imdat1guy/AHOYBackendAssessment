using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models
{
    public class HotelReview
    {
        public int HotelReviewID { get; set; }
        public int CustomerID { get; set; }
        public int HotelID { get; set; }
        public Hotel hotel { get; set; }
        public float Rating { get; set; }
    }
}
