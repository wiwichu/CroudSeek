﻿using System;

namespace CroudSeek.Application.Models.Authentication
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
