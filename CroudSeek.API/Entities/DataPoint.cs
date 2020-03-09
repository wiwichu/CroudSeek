using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CroudSeek.API.Entities
{
    public class DataPoint
    {
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        [Required]
        public User Owner { get; set; }
        public int QuestId { get; set; }
        [ForeignKey("QuestId")]
        [Required]
        public int Quest { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        //How big the circle around this point is.
        public double RadiusMeters { get; set; }
        //Arbitrary number indicating the level of certainty on this datapoint
        public double Certainty { get; set; }
        //Whether this DataPoint indicates the Quest is not found here.
        public bool IsNegative { get; set; }
        //Whether other users can view this datapoint.
        public bool IsPrivate { get; set; }
    }
}
