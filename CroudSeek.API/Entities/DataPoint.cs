using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CroudSeek.API.Entities
{
    public class DataPoint
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double CircumferenceMeters { get; set; }
        public double Certainty { get; set; }
        public bool IsNegative { get; set; }
    }
}
