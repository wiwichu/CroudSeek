using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Entities
{
    public class Quest
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid Owner { get; set; }
        [ForeignKey("ZoneId")]
        public Guid Zone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
