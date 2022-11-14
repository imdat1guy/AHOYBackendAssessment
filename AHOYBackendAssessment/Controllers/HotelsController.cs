using AHOYBackendAssessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ILogger<HotelsController> _logger;
        private readonly HotelCoreDBContext _context;

        public HotelsController(ILogger<HotelsController> logger, HotelCoreDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// API to list hotels with optional filtering parameters.
        /// </summary>
        /// <param name="batchsize">size of the batch of hotels to be returned (pagination)</param>
        /// <param name="batchNum">the number of the batch, or the page, to be retrieved</param>
        /// <param name="name">The name of the hotel</param>
        /// <param name="city">city of the hotel</param>
        /// <param name="isRecommended">Recommended filter</param>
        /// <param name="minprice">minimum price</param>
        /// <param name="maxprice">maximum price</param>
        /// <param name="checkin">Check-in Date</param>
        /// <param name="checkout">Check-out Date</param>
        /// <param name="minrating">Minimum rating</param>
        /// <param name="maxrating">Maximum rating</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<HotelSummary>> Get(int batchsize = 0, int batchNum = 0, string name = "", string city = "", bool? isRecommended = null, float? minprice = null, float? maxprice = null, DateTime? checkin = null, DateTime? checkout = null, float? minrating = null, float? maxrating = null)
        {
            try
            {
                List<HotelSummary> Results = new List<HotelSummary>();

                //query ddb for hotels with filters
                IQueryable<HotelSummary> hotels = _context.Hotels.Where(i => (name == "" || i.Name.Contains(name))
                                                             && (city == "" || i.City == name)
                                                             && (isRecommended == null || i.IsRecommended == isRecommended)
                                                             && (minprice == null || i.PricePerNight >= minprice)
                                                             && (maxprice == null || i.PricePerNight <= maxprice)
                                                             && (minrating == null || i.Rating >= minrating)
                                                             && (maxrating == null || i.Rating >= maxrating)).Select(i => new HotelSummary
                                                             {
                                                                 ID = i.HotelID,
                                                                 Address = i.Address,
                                                                 Name = i.Name,
                                                                 NumberOfReviews = i.HotelReviews.Count(),
                                                                 PricePerNight = i.PricePerNight,
                                                                 Rating = i.Rating,
                                                                 Thumbnail = i.Thumbnail
                                                             });
                if (batchsize != 0)
                {
                    //paged call. Take only batchsize. Skip the previous pages.
                    var skip = batchNum * batchsize;
                    Results = hotels.Skip(skip).Take(batchsize).ToList();
                }
                else
                {
                    //no pagination. retrieve all elements
                    Results = hotels.ToList();
                }

                return Results;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// API to retrieve specific hotel details
        /// </summary>
        /// <param name="id">Hotel ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<HotelDetails> GetHotel(int id)
        {
            //Query Hotel
            var hotel = _context.Hotels.Where(i => i.HotelID == id).Select(i => new HotelDetails
            {
                Address = i.Address,
                City = i.City,
                Description = i.Description,
                Facilities = i.Facilities,
                HotelID = i.HotelID,
                Images = i.Images,
                Latitude = i.Latitude,
                Longitude = i.Longitude,
                Name = i.Name,
                PricePerNight = i.PricePerNight,
                Rating = i.Rating,
                HotelReviews = i.HotelReviews.ToList()
            }).FirstOrDefault();

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }
    }
}
