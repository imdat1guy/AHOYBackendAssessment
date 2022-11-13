using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AHOYBackendAssessment.Models
{
    public class HotelCoreDBContext : DbContext
    {
        public HotelCoreDBContext(DbContextOptions<HotelCoreDBContext> options)
            : base(options)
        { }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<HotelReview> HotelReviews { get; set; }
    }
}
