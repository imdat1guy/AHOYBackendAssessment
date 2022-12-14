using AHOYBackendAssessment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int HotelID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public float TotalPrice { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<RoomBooking> RoomBookings { get; set; }
    }
}
