using System;
using System.Collections.Generic;

namespace CroudSeek.API.Models
{
    public class ViewDto : BaseDto
    {
       // public int Id { get; set; }
        public int OwnerId { get; set; }
        public int Quest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public bool ExcludeByDefault { get; set; }
        public List<UserWeightDto> UserWeights { get; set; } = new List<UserWeightDto>();
    }
}
