using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Shared
{
    public class UserWeightForCreationDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
        //Exclude User from view
        public bool ExcludeUser { get; set; }
        //How to weight this users DataPoint. Arbitrary number relative to other users. 
        public double Weight { get; set; }
    }
}
