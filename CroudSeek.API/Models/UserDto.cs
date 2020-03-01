using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CroudSeek.API.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
