﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CroudSeek.API.Entities
{
    public class View : BaseEntity
    {
        public int QuestId { get; set; }
        [ForeignKey("QuestId")]
        [Required]
        public Quest Quest { get; set; }
        [Required]
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
        //public ICollection<User> ExcludedUsers { get; set; }
        //    = new List<User>();
        //public ICollection<User> IncludedUsers { get; set; }
        //    = new List<User>();
        //Collection of parameters per user indicating how their weights contribute to the result.
        public ICollection<ViewUserWeight> ViewUserWeights { get; set; }
    = new List<ViewUserWeight>();
        public ICollection<UserWeight> UserWeights { get; set; }
    = new List<UserWeight>();
    }
}
