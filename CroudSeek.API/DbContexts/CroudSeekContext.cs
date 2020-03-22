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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //    modelBuilder.Entity<User>()
            //        .HasData(
            //            new User()
            //            {
            //                Id = 1,
            //                Email = "Joe.Schmo@CroudSeek.com",
            //                Name = "JoeSchmo"
            //            },
            //            new User()
            //            {
            //                Id = 2,
            //                Email = "Shiny.Hiney@CroudSeek.com",
            //                Name = "ShinyHiney"
            //            },
            //            new User()
            //            {
            //                Id = 3,
            //                Email = "Silly.Billy@CroudSeek.com",
            //                Name = "SillyBilly"
            //            },
            //            new User()
            //            {
            //                Id = 4,
            //                Email = "Holy.Moley@CroudSeek.com",
            //                Name = "HolyMoley"
            //            },
            //            new User()
            //            {
            //                Id = 5,
            //                Email = "Funny.Bunny@CroudSeek.com",
            //                Name = "FunnyBunny"
            //            }
            //        );
            //    modelBuilder.Entity<Quest>()
            //        .HasData(
            //        new Quest()
            //        {
            //            Id = 1,
            //            Name = "BigQuest",
            //            Description = "The Big Quest",
            //            IsPrivate = false,
            //            UserId=1
            //        }
            //); ;

            base.OnModelCreating(modelBuilder);
        }
    }
}
