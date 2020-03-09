using CroudSeek.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.DbContexts
{
    public class CroudSeekContext : DbContext
    {
        public CroudSeekContext(DbContextOptions<CroudSeekContext> options)
   : base(options)
        {
        }

        public DbSet<DataPoint> DataPoints { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWeight> UserWeights { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}
