﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Entities
{
    public class UserWeight : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public User User { get; set; }
        //Exclude User from view
        public bool ExcludeUser { get; set; }
        //How to weight this users DataPoint. Arbitrary number relative to other users. 
        public double Weight { get; set; }
        public ICollection<ViewUserWeight> ViewUserWeights { get; set; }
    = new List<ViewUserWeight>();

    }
}
