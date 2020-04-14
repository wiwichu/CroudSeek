﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Entities
{
    public class Quest : BaseEntity
    {
        //[Key]
        //public int Id { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        [Required]
        public User Owner { get; set; }
        public int ZoneId { get; set; }
        [ForeignKey("ZoneId")]
        [Required]
        //Indicates boundaries of this Quest.
        public Zone Zone { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        //Whether Quest can be viewed by other Users
        public bool IsPrivate { get; set; }
        public ICollection<DataPoint> DataPoints { get; set; }
    = new List<DataPoint>();

    }
}
