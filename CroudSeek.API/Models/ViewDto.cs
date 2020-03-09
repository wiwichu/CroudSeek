﻿using System;
using System.Collections.Generic;

namespace CroudSeek.API.Models
{
    public class ViewDto
    {
        public int Id { get; set; }
        public int Owner { get; set; }
        public int Quest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public bool ExcludeByDefault { get; set; }
        public HashSet<Guid> ExcludedUsers { get; set; } = new HashSet<Guid>();
        public HashSet<Guid> IncludedUsers { get; set; } = new HashSet<Guid>();
        public IDictionary<Guid, double> UserWeights { get; set; } = new Dictionary<Guid, double>();
    }
}
