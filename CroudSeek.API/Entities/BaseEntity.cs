using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public int OwnerId { get; set; } = 1;
        [EmailAddress]
        public string Email { get; set; }
        public bool Active { get; set; }
        [ForeignKey("OwnerId")]
        [Required]
        public User Owner { get; set; }
    }
}
