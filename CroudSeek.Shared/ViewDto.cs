using System;
using System.Collections.Generic;

namespace CroudSeek.Shared
{
    public class ViewDto : BaseDto
    {
        public int QuestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public bool ExcludeByDefault { get; set; }
        /// <summary>
        /// Maximum age in days of datapoints. <0 = no limit.
        /// </summary>
        public int age { get; set; } = -1;

        public List<UserWeightDto> UserWeights { get; set; } = new List<UserWeightDto>();
    }
}
