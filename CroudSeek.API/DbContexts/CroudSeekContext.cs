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
            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        Id = 1,
                        Email = "Joe.Schmo@CroudSeek.com",
                        Name = "JoeSchmo"
                    },
                    new User()
                    {
                        Id = 2,
                        Email = "Shiny.Hiney@CroudSeek.com",
                        Name = "ShinyHiney"
                    },
                    new User()
                    {
                        Id = 3,
                        Email = "Silly.Billy@CroudSeek.com",
                        Name = "SillyBilly"
                    },
                    new User()
                    {
                        Id = 4,
                        Email = "Holy.Moley@CroudSeek.com",
                        Name = "HolyMoley"
                    },
                    new User()
                    {
                        Id = 5,
                        Email = "Funny.Bunny@CroudSeek.com",
                        Name = "FunnyBunny"
                    }
                );
            modelBuilder.Entity<Zone>()
                .HasData(
                new Zone()
                {
                    Id = 1,
                    OwnerId = 1,
                    Description = "This is Zone1",
                    IsPrivate = false,
                    MaxAltitude = 999999.9999,
                    MaxLatitude = 999999.9999,
                    MaxLongitude = 999999.999,
                    MinAltitude = 11111.111,
                    MinLatitude = 11111.11,
                    MinLongitude = 111111.11,
                    Name = "Zone1",
                    SpotLatitude = 5555.555,
                    SpotLongitude = 55555.55,
                    SpotRadiusMeters = 1000.7,
                },
               new Zone()
               {
                   Id = 2,
                   OwnerId = 2,
                   Description = "This is Zone2",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 999999.9999,
                   MaxLongitude = 999999.999,
                   MinAltitude = 11111.111,
                   MinLatitude = 11111.11,
                   MinLongitude = 111111.11,
                   Name = "Zone2",
                   SpotLatitude = 5555.555,
                   SpotLongitude = 55555.55,
                   SpotRadiusMeters = 1000.7,
               },
               new Zone()
               {
                   Id = 3,
                   OwnerId = 3,
                   Description = "This is Zone3",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 999999.9999,
                   MaxLongitude = 999999.999,
                   MinAltitude = 11111.111,
                   MinLatitude = 11111.11,
                   MinLongitude = 111111.11,
                   Name = "Zone3",
                   SpotLatitude = 5555.555,
                   SpotLongitude = 55555.55,
                   SpotRadiusMeters = 1000.7,
               },
               new Zone()
               {
                   Id = 4,
                   OwnerId = 4,
                   Description = "This is Zone4",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 999999.9999,
                   MaxLongitude = 999999.999,
                   MinAltitude = 11111.111,
                   MinLatitude = 11111.11,
                   MinLongitude = 111111.11,
                   Name = "Zone4",
                   SpotLatitude = 5555.555,
                   SpotLongitude = 55555.55,
                   SpotRadiusMeters = 1000.7,
               },
               new Zone()
               {
                   Id = 5,
                   OwnerId = 5,
                   Description = "This is Zone5",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 999999.9999,
                   MaxLongitude = 999999.999,
                   MinAltitude = 11111.111,
                   MinLatitude = 11111.11,
                   MinLongitude = 111111.11,
                   Name = "Zone5",
                   SpotLatitude = 5555.555,
                   SpotLongitude = 55555.55,
                   SpotRadiusMeters = 1000.7,
               }
                );
            modelBuilder.Entity<Quest>()
                .HasData(
                new Quest()
                {
                    Id = 1,
                    Name = "BigQuest",
                    Description = "The Big Quest",
                    IsPrivate = false,
                    OwnerId = 1,
                    ZoneId = 1
                },
                new Quest()
                {
                    Id = 2,
                    Name = "BigBigQuest",
                    Description = "The Big Big Quest",
                    IsPrivate = false,
                    OwnerId = 2,
                    ZoneId = 2
                },
                new Quest()
                {
                    Id = 3,
                    Name = "VeryBigQuest",
                    Description = "The Very Big Quest",
                    IsPrivate = false,
                    OwnerId = 3,
                    ZoneId = 3
                },
                new Quest()
                {
                    Id = 4,
                    Name = "ReallyBigQuest",
                    Description = "The Really Big Quest",
                    IsPrivate = false,
                    OwnerId = 4,
                    ZoneId = 4
                },
                new Quest()
                {
                    Id = 5,
                    Name = "ReallyVeryBigQuest",
                    Description = "The Really Very Big Quest",
                    IsPrivate = false,
                    OwnerId = 5,
                    ZoneId = 5
                }
        ); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
