using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Entities
{
    public class UserWeight
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public double Weight { get; set; }
    }
}
