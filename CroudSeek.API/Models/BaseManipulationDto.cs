using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Models
{
    public class BaseManipulationDto : BaseDto
    {
        public int OwnerId { get; set; }
    }
}
