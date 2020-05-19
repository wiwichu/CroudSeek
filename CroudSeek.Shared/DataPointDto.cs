using System;

namespace CroudSeek.Shared
{
    public class DataPointDto : BaseDto
    {
        /// <summary>
        /// Id of Quest this DataPoint belongs to
        /// </summary>
        public int QuestId { get; set; }
        /// <summary>
        /// Name of DataPoint
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of DataPoint
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Latitude of DataPoint
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude of DataPoint
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Altitude of DataPoint
        /// </summary>
        public double Altitude { get; set; }
        /// <summary>
        /// Radius of DataPoint in Meters
        /// </summary>
        public double RadiusMeters { get; set; }
        /// <summary>
        /// Certainty of Observation
        /// </summary>
        public double Certainty { get; set; }
        /// <summary>
        /// Whether this DataPoint indicates where the Quest is not found
        /// </summary>
        public bool IsNegative { get; set; }
        /// <summary>
        /// Whether DataPoint can be shared with other users
        /// </summary>
        public bool IsPrivate { get; set; }
        /// <summary>
        /// When DataPoint was found
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }

    }
}
