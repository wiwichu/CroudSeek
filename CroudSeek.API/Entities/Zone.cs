using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Entities
{
    public class Zone
    {
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        [Required]
        public User Owner { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public double? MaxLatitude { get; set; }
        public double? MinLatitude { get; set; }
        public double? MaxLongitude { get; set; }
        public double? MinLongitude { get; set; }
        public double? MaxAltitude { get; set; }
        public double? MinAltitude { get; set; }
        public double? SpotLatitude { get; set; }
        public double? SpotLongitude { get; set; }
        public double? SpotRadiusMeters { get; set; }
        public bool IsPrivate { get; set; }
    }
}
