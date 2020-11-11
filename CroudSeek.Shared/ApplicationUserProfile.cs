using System;
using System.Collections.Generic;
using System.Text;

namespace CroudSeek.Shared
{
    public class ApplicationUserProfile
    { 
        public Guid Id { get; set; }         
        public string Subject { get; set; } 
        public string SubscriptionLevel { get; set; }
    }
} 