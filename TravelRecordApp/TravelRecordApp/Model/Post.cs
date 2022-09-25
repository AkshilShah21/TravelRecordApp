using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Experience { get; set; }
        public string VenueName { get; set; }
        public string CatagoryId { get; set; }
        public string CatagoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
        // public string UserId { get; set; }
    }
}
