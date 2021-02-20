using CroudSeek.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CroudSeek.Identity
{
    public class CroudSeekIdentityDbContext : IdentityDbContext<ApplicationUserCS>
    {
        public CroudSeekIdentityDbContext(DbContextOptions<CroudSeekIdentityDbContext> options) : base(options)
        {
        }
    }
}
