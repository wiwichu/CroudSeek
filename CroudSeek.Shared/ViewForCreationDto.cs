using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Shared
{
    public class ViewForCreationDto
    {
        [Required(ErrorMessage ="QuestId is required.")]
        public int QuestId { get; set; }
        [Required(ErrorMessage ="Name is equired.")]
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
        public int age { get; set; } = -1;
        public ICollection<UserWeightForCreationDto> UserWeights { get; set; }
            = new List<UserWeightForCreationDto>();
    }
}
