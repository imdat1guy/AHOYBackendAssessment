using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AHOYBackendAssessment.Models;
using AHOYBackendAssessment.Models.interfaces;
using AHOYBackendAssessment.Models.Entities;
using Microsoft.Extensions.Logging;

namespace AHOYBackendAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly HotelCoreDBContext _context;
        private readonly IBookingValidator _bookingValidator;
        private readonly ILogger<HotelsController> _logger;

        public BookingsController(HotelCoreDBContext context, IBookingValidator bookingValidator, ILogger<HotelsController> logger)
        {
            _context = context;
            _bookingValidator = bookingValidator;
            _logger = logger;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingRequest bookingRequest)
        {
            try
            {
                //check availability of rooms
                var availableRooms = _bookingValidator.GetAvailbleRooms(bookingRequest);
                if (availableRooms.Length >= bookingRequest.NumberOfRooms)
                {
                    //Create booking object
                    var numberOfDays = (bookingRequest.CheckOut - bookingRequest.CheckIn).Days;
                    var price = _context.Hotels.Where(i => i.HotelID == bookingRequest.HotelID).Select(i => i.PricePerNight).FirstOrDefault();

                    Booking booking = new Booking
                    {
                        CheckIn = bookingRequest.CheckIn,
                        CheckOut = bookingRequest.CheckOut,
                        CustomerID = bookingRequest.CustomerID,
                        HotelID = bookingRequest.HotelID,
                        Timestamp = DateTime.UtcNow,
                        TotalPrice = numberOfDays * bookingRequest.NumberOfRooms * price
                    };
                    //add booking
                    _context.Bookings.Add(booking);
                    await _context.SaveChangesAsync();
                    //book rooms
                    _context.RoomBookings.AddRange(availableRooms.Take(bookingRequest.NumberOfRooms).Select(i => new RoomBooking { BookingID = booking.BookingID, RoomID = i }).ToArray());
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
                }
                else
                    return ValidationProblem("No Rooms Available");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
