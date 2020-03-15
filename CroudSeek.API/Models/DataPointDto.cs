using System;

namespace CroudSeek.API.Models
{
    public class DataPointDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double RadiusMeters { get; set; }
        public double Certainty { get; set; }
        public bool IsNegative { get; set; }
        public bool IsPrivate { get; set; }
    }
}
