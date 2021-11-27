

using System;
using System.ComponentModel.DataAnnotations;

namespace CroudSeek.Shared
{
    //[DataPointNameMustBeDifferentFromDescription(
    //  ErrorMessage = "Name must be different from description.")]

    public abstract class DataPointForManipulationDto
    {
        /// <summary>
        /// Id of Quest this DatraPoint belongs to
        /// </summary>
        //[Required(ErrorMessage = "Quest is required.")]
        //public int QuestId { get; set; }
        /// <summary>
        /// Name of DataPoint
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Description of DataPoint
        /// </summary>
        [MaxLength(1000)]
        public virtual string Description { get; set; }
        [Range(-90, 90)]
        /// <summary>
        /// Latitude of DataPoint
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude of DataPoint
        /// </summary>
        [Range(-180, 180)]
        public double Longitude { get; set; }
        /// <summary>
        /// Altitude of DataPoint
        /// </summary>
        public double Altitude { get; set; }
        /// <summary>
        /// How big the circle around this point is.
        /// </summary>
        [Range(1, Double.PositiveInfinity)]
        public double RadiusMeters { get; set; } = 1;
        /// <summary>
        /// Arbitrary number indicating the level of certainty on this datapoint
        /// </summary>
        [Range(1, 100)]
        public double Certainty { get; set; }
        /// <summary>
        /// Whether this DataPoint indicates the Quest is not found here.
        /// </summary>
        public bool IsNegative { get; set; }
        /// <summary>
        /// Whether other users can view this datapoint.
        /// </summary>
        public bool IsPrivate { get; set; }
        /// <summary>
        /// Email to be used when verifying
        /// </summary>

        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// When DataPoint was found
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }
    }
}
