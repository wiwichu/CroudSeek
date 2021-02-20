using Microsoft.AspNetCore.Identity;

namespace CroudSeek.Identity.Models
{
    public class ApplicationUserCS : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
