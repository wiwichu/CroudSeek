using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CroudSeek.API.Entities
{
    public class View
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid Owner { get; set; }
        [ForeignKey("QuestId")]
        public Guid Quest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public bool ExcludeByDefault { get; set; }

        public ICollection<User> ExcludedUsers { get; set; }
            = new List<User>();
        public ICollection<User> IncludedUsers { get; set; }
            = new List<User>();
        public ICollection<UserWeight> UserWeights { get; set; }
            = new List<UserWeight>();
    }
}
