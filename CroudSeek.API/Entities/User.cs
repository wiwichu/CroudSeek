using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CroudSeek.API.Entities
{
    public class User : BaseEntity
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
