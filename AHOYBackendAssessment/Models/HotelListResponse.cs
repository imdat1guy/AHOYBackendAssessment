using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models
{
    public class HotelListResponse
    {
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
        public List<HotelSummary> Hotels { get; set; }
    }

    public class HotelDetailsResponse
    {
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
        public HotelDetails Hotel { get; set; }
    }
}
