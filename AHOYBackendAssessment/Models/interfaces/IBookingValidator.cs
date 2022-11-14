using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models.interfaces
{
    public interface IBookingValidator
    {
        //method to check for available rooms
        public int[] GetAvailbleRooms(BookingRequest booking);
    }
}
