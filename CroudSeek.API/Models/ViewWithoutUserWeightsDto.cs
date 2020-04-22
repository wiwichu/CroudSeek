using System;
using System.Collections.Generic;

namespace CroudSeek.API.Models
{
    public class ViewWithoutUserWeightsDto : BaseDto
    {
        public int Id { get; set; }
        public int Quest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public bool ExcludeByDefault { get; set; }
    }
}
