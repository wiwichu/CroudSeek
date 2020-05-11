using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CroudSeek.Shared
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
