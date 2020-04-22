using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Models
{
    public class QuestForCreationDto
    {
        [Required(ErrorMessage ="ZoneId is required.")]
        public int ZoneId { get; set; }
        [Required(ErrorMessage ="Name is required.")]
        [MaxLength(100, ErrorMessage ="Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        //Whether Quest can be viewed by other Users
        public bool IsPrivate { get; set; }
        public ICollection<DataPointForCreationDto> DataPoints { get; set; }
  = new List<DataPointForCreationDto>();

    }
}
