using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Models
{
    public class QuestForCreationDto
    {
        [Required(ErrorMessage ="UserId is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage ="ZoneId is required.")]
        public int ZoneId { get; set; }
        [Required(ErrorMessage ="Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        //Whether Quest can be viewed by other Users
        public bool IsPrivate { get; set; }
    }
}
