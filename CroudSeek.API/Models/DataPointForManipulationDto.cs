﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Models
{
    //[DataPointNameMustBeDifferentFromDescription(
    //  ErrorMessage = "Name must be different from description.")]

    public abstract class DataPointForManipulationDto
    {
        [Required(ErrorMessage = "User is required.")]
        public int OwnerId { get; set; }
        [Required(ErrorMessage = "Quest is required.")]
        public int QuestId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public virtual string Description { get; set; }
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