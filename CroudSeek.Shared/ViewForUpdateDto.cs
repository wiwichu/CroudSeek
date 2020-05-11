using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Shared
{
    public class ViewForUpdateDto
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
        //public ICollection<User> ExcludedUsers { get; set; }
        //    = new List<User>();
        //public ICollection<User> IncludedUsers { get; set; }
        //    = new List<User>();
        //Collection of parameters per user indicating how their weights contribute to the result.
        public ICollection<UserWeightForUpdateDto> UserWeights { get; set; }
            = new List<UserWeightForUpdateDto>();
    }
}
