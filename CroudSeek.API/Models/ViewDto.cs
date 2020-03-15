using System;
using System.Collections.Generic;

namespace CroudSeek.API.Models
{
    public class ViewDto
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Quest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public bool ExcludeByDefault { get; set; }
        public HashSet<int> ExcludedUsers { get;} = new HashSet<int>();
        public HashSet<int> IncludedUsers { get;} = new HashSet<int>();
        public IDictionary<int, double> UserWeights { get;} = new Dictionary<int, double>();
    }
}
