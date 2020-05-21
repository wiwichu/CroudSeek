using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CroudSeek.Shared.Helpers;

namespace CroudSeek.Shared
{
    public class ViewForManipulationDto  
    {
        [Required(ErrorMessage = "QuestId is required.")]
        public int QuestId { get; set; }
        [Required(ErrorMessage = "Name is equired.")]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        //This view cannot be viewedby others
        public bool IsPrivate { get; set; }
        //Users must specifically be added as a userweight to be considered.
        public bool ExcludeByDefault { get; set; }
        /// <summary>
        /// Maximum age in days of datapoints. <0 = no limit.
        /// </summary>
        public int Age { get; set; } = -1;
        [ViewValidator(ErrorMessage =
           "There can only be one UserWeight per User")]
        public ICollection<UserWeightForCreationDto> UserWeights { get; set; }
            = new List<UserWeightForCreationDto>();
    }
}
