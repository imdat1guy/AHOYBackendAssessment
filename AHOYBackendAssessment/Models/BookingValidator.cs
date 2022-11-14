using AHOYBackendAssessment.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models
{
    public class BookingValidator : IBookingValidator
    {
        private readonly HotelCoreDBContext _context;

        public BookingValidator(HotelCoreDBContext context)
        {
            _context = context;
        }

        public int[] GetAvailbleRooms(BookingRequest booking)
        {
            //get the capacity of the hotel
            var rooms = _context.Rooms.Where(i => i.HotelID == booking.HotelID).Select(i => i.RoomID);
            
            //make sure the hotel exists and has rooms
            if (rooms.Any())
            {
                //IQueryable of booked rooms in selected period
                IQueryable<int> ExistingBookings = (from b in _context.Bookings.Where(i => i.HotelID == booking.HotelID
                                                                && ((i.CheckIn >= booking.CheckIn && i.CheckIn <= booking.CheckOut) || (i.CheckOut >= booking.CheckIn && i.CheckOut <= booking.CheckOut)))
                                        join r in _context.RoomBookings on b.BookingID equals r.BookingID
                                        select r.RoomID);

                //return list of available Rooms
                return rooms.Except(ExistingBookings).ToArray();
            }
            else
                return new int[0];
        }
    }
}
