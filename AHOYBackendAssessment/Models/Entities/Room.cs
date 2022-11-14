using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models.Entities
{
    public class Room
    {
        public int RoomID { get; set; }
        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }
    }
}
