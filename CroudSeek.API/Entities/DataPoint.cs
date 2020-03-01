﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CroudSeek.API.Entities
{
    public class DataPointDto
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid Owner { get; set; }
        [ForeignKey("QuestId")]
        public Guid Quest { get; set; }
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
