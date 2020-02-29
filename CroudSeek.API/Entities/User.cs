﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CroudSeek.API.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
