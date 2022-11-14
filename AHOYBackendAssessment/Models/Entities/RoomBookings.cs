using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models.Entities
{
    public class RoomBooking
    {
        public int RoomBookingID { get; set; }
        public int RoomID { get; set; }
        public int BookingID { get; set; }
    }
}
