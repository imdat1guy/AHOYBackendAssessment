﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOYBackendAssessment.Models
{
    public class Hotel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float PricePerNight { get; set; }
        public List<Image> Images { get; set; }
        public string Thumbnail { get; set; }
        public float Rating { get; set; }
        public int NumberOfRooms { get; set; }
        public bool IsRecommended { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Facility> Facilities { get; set; }
    }
}
