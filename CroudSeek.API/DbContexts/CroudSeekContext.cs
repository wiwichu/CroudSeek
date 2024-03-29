﻿using CroudSeek.API.Entities;
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

        public DbSet<ApplicationUserProfile> ApplicationUserProfiles { get; set; }

        public DbSet<DataPoint> DataPoints { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWeight> UserWeights { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<ViewUserWeight> ViewUserWeights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<ViewUserWeight>().HasKey(v => new { v.ViewId, v.UserWeightId });
            modelBuilder.Entity<ViewUserWeight>().HasData(
                new ViewUserWeight()
                {
                    UserWeightId = 1,
                    ViewId = 1,
                },
                new ViewUserWeight()
                {
                    UserWeightId = 1,
                    ViewId = 2
                },
                new ViewUserWeight()
                {
                    UserWeightId = 2,
                    ViewId = 1
                },
                new ViewUserWeight()
                {
                    UserWeightId = 2,
                    ViewId = 2
                }
                );
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Name)
                        .IsUnique();
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();
            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        Id = -1,
                        OwnerId = -1,
                        Email = "Boss@CroudSeek.com",
                        Name = "Boss"
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    }, new User()
                    {
                        Id = 1,
                        OwnerId = 1,
                        Email = "Joe.Schmo@CroudSeek.com",
                        Name = "JoeSchmo"
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    },
                   new User()
                   {
                       Id = 11,
                       OwnerId=11,
                       Email = "1Joe.Schmo@CroudSeek.com",
                       Name = "1JoeSchmo"
                        ,
                       CreateTime = DateTimeOffset.Now,
                       UpdateTime = DateTimeOffset.Now
                   },
                    new User()
                    {
                        Id = 2,
                        OwnerId = 2,
                        Email = "Shiny.Hiney@CroudSeek.com",
                        Name = "ShinyHiney"
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    },
                    new User()
                    {
                        Id = 3,
                        OwnerId = 3,
                        Email = "Silly.Billy@CroudSeek.com",
                        Name = "SillyBilly"
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    },
                    new User()
                    {
                        Id = 4,
                        OwnerId = 4,
                        Email = "Holy.Moley@CroudSeek.com",
                        Name = "HolyMoley"
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    },
                    new User()
                    {
                        Id = 5,
                        OwnerId = 5,
                        Email = "Funny.Bunny@CroudSeek.com",
                        Name = "FunnyBunny"
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    }
                );
            modelBuilder.Entity<Zone>()
                .HasData(
                new Zone()
                {
                    Id = -1,
                    OwnerId = -1,
                    Description = "The whole worlds",
                    IsPrivate = false,
                    MaxAltitude = 999999.9999,
                    MaxLatitude = 90,
                    MaxLongitude = 180,
                    MinAltitude = -200,
                    MinLatitude = -90,
                    MinLongitude = -180,
                    Name = "World",
                    SpotLatitude = 40,
                    SpotLongitude = -100,
                    SpotRadiusMeters = 10000.7
                        ,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
               new Zone()
               {
                   Id = 1,
                   OwnerId = 1,
                   Description = "This is Zone1",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 20,
                   MaxLongitude = -60,
                   MinAltitude = -200,
                   MinLatitude = 20,
                   MinLongitude = 160,
                   Name = "Zone1",
                   SpotLatitude = 40,
                   SpotLongitude = -100,
                   SpotRadiusMeters = 10000.7
                        ,
                   CreateTime = DateTimeOffset.Now,
                   UpdateTime = DateTimeOffset.Now
               },
               new Zone()
               {
                   Id = 2,
                   OwnerId = 2,
                   Description = "This is Zone2",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 20,
                   MaxLongitude = -60,
                   MinAltitude = -200,
                   MinLatitude = 20,
                   MinLongitude = 160,
                   Name = "Zone2",
                   SpotLatitude = 40,
                   SpotLongitude = -100,
                   SpotRadiusMeters = 10000.7
                        ,
                   CreateTime = DateTimeOffset.Now,
                   UpdateTime = DateTimeOffset.Now
               },
               new Zone()
               {
                   Id = 3,
                   OwnerId = 3,
                   Description = "This is Zone3",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 20,
                   MaxLongitude = -60,
                   MinAltitude = -200,
                   MinLatitude = 20,
                   MinLongitude = 160,
                   Name = "Zone3",
                   SpotLatitude = 40,
                   SpotLongitude = -100,
                   SpotRadiusMeters = 10000.7
                        ,
                   CreateTime = DateTimeOffset.Now,
                   UpdateTime = DateTimeOffset.Now
               },
               new Zone()
               {
                   Id = 4,
                   OwnerId = 4,
                   Description = "This is Zone4",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 20,
                   MaxLongitude = -60,
                   MinAltitude = -200,
                   MinLatitude = 20,
                   MinLongitude = 160,
                   Name = "Zone4",
                   SpotLatitude = 40,
                   SpotLongitude = -100,
                   SpotRadiusMeters = 10000.7
                        ,
                   CreateTime = DateTimeOffset.Now,
                   UpdateTime = DateTimeOffset.Now
               },
               new Zone()
               {
                   Id = 5,
                   OwnerId = 5,
                   Description = "This is Zone5",
                   IsPrivate = false,
                   MaxAltitude = 999999.9999,
                   MaxLatitude = 20,
                   MaxLongitude = -60,
                   MinAltitude = -200,
                   MinLatitude = 20,
                   MinLongitude = 160,
                   Name = "Zone5",
                   SpotLatitude = 40,
                   SpotLongitude = -100,
                   SpotRadiusMeters = 10000.7
                        ,
                   CreateTime = DateTimeOffset.Now,
                   UpdateTime = DateTimeOffset.Now
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
                        ,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new Quest()
                {
                    Id = 2,
                    Name = "BigBigQuest",
                    Description = "The Big Big Quest",
                    IsPrivate = false,
                    OwnerId = 2,
                    ZoneId = 2
                        ,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new Quest()
                {
                    Id = 3,
                    Name = "VeryBigQuest",
                    Description = "The Very Big Quest",
                    IsPrivate = false,
                    OwnerId = 3,
                    ZoneId = 3
                        ,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new Quest()
                {
                    Id = 4,
                    Name = "ReallyBigQuest",
                    Description = "The Really Big Quest",
                    IsPrivate = false,
                    OwnerId = 4,
                    ZoneId = 4
                        ,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new Quest()
                {
                    Id = 5,
                    Name = "ReallyVeryBigQuest",
                    Description = "The Really Very Big Quest",
                    IsPrivate = false,
                    OwnerId = 5,
                    ZoneId = 5
                        ,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new Quest()
                {
                    Id = 6,
                    Name = "NYC Sights",
                    Description = "Sights of NYC",
                    IsPrivate = false,
                    OwnerId = 5,
                    ZoneId = 5
                        ,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }
        );
        modelBuilder.Entity<DataPoint>()
                .HasData(
                new DataPoint()
                {
                    Id = 1,
                    Altitude = 0,
                    Certainty = 20,
                    Description = "Could be here",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 25,
                    Longitude = -120,
                    Name = "Quest1 Possible Sighting",
                    OwnerId = 1,
                    QuestId = 1,
                    RadiusMeters = 1,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new DataPoint()
                {
                    Id = 2,
                    Altitude = 10,
                    Certainty = 28,
                    Description = "Another possibility",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 50,
                    Longitude = -65,
                    Name = "Quest1 Maybe here",
                    OwnerId = 1,
                    QuestId = 1,
                    RadiusMeters = 100,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new DataPoint()
                {
                    Id = 3,
                    Altitude = -10,
                    Certainty = 50,
                    Description = "Buried Treasure",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 40,
                    Longitude = -80,
                    Name = "Quest1 Digging",
                    OwnerId = 3,
                    QuestId = 1,
                    RadiusMeters = 10,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }, new DataPoint()
                {
                    Id = 4,
                    Altitude = 0,
                    Certainty = 80,
                    Description = "Checked already",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 30,
                    Longitude = -90,
                    Name = "Quest1 Don't Bother",
                    OwnerId = 2,
                    QuestId = 1,
                    RadiusMeters = 1000,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new DataPoint()
                {
                    Id = 5,
                    Altitude = 0,
                    Certainty = 20,
                    Description = "Could be here",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 25,
                    Longitude = -120,
                    Name = "Quest2 Possible Sighting",
                    OwnerId = 2,
                    QuestId = 2,
                    RadiusMeters = 1,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new DataPoint()
                {
                    Id = 6,
                    Altitude = 10,
                    Certainty = 28,
                    Description = "Another possibility",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 50,
                    Longitude = -65,
                    Name = "Quest2 Maybe here",
                    OwnerId = 2,
                    QuestId = 2,
                    RadiusMeters = 100,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new DataPoint()
                {
                    Id = 7,
                    Altitude = -10,
                    Certainty = 50,
                    Description = "Buried Treasure",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 40,
                    Longitude = -80,
                    Name = "Quest2 Digging",
                    OwnerId = 3,
                    QuestId = 2,
                    RadiusMeters = 10,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }, new DataPoint()
                {
                    Id = 9,
                    Altitude = 0,
                    Certainty = 80,
                    Description = "Empire State Building",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 40.748817,
                    Longitude = -73.985428,
                    Name = "Empire State Building",
                    OwnerId = 3,
                    QuestId = 6,
                    RadiusMeters = 1000,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }, new DataPoint()
                {
                    Id = 10,
                    Altitude = 0,
                    Certainty = 80,
                    Description = "Chrysler Building",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 40.751652,
                    Longitude = -73.975311,
                    Name = "Chrysler Building",
                    OwnerId = 3,
                    QuestId = 6,
                    RadiusMeters = 1000,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }, new DataPoint()
                {
                    Id = 11,
                    Altitude = 0,
                    Certainty = 80,
                    Description = "Flatiron Building",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 40.741112,
                    Longitude = -73.989723,
                    Name = "Flatiron Building",
                    OwnerId = 3,
                    QuestId = 6,
                    RadiusMeters = 1000,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }, new DataPoint()
                {
                    Id = 12,
                    Altitude = 0,
                    Certainty = 80,
                    Description = "One World Trade Center",
                    IsNegative = false,
                    IsPrivate = false,
                    Latitude = 40.712742,
                    Longitude = -74.013382,
                    Name = "One World Trade Center",
                    OwnerId = 3,
                    QuestId = 6,
                    RadiusMeters = 1000,
                    TimeStamp = DateTimeOffset.Now,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }
, new DataPoint()
{
    Id = 8,
    Altitude = 0,
    Certainty = 80,
    Description = "Checked already",
    IsNegative = false,
    IsPrivate = false,
    Latitude = 30,
    Longitude = -90,
    Name = "Quest2 Don't Bother",
    OwnerId = 3,
    QuestId = 2,
    RadiusMeters = 1000,
    TimeStamp = DateTimeOffset.Now,
    CreateTime = DateTimeOffset.Now,
    UpdateTime = DateTimeOffset.Now
}


        );
            modelBuilder.Entity<UserWeight>()
                .HasData(
            new UserWeight()
            {
                ExcludeUser = false,
                Id = 1,
                OwnerId = 1,
                UserId = 3,
                Weight = 90
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            },
            new UserWeight()
            {
                ExcludeUser = false,
                Id = 2,
                OwnerId = 1,
                UserId = 2,
                Weight = 10
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            }, new UserWeight()
            {
                ExcludeUser = false,
                Id = 3,
                OwnerId = 1,
                UserId = 2,
                Weight = 30
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            },
            new UserWeight()
            {
                ExcludeUser = true,
                Id = 4,
                OwnerId = 1,
                UserId = 4,
                Weight = 1000
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            },
            new UserWeight()
            {
                ExcludeUser = false,
                Id = 5,
                OwnerId = 2,
                UserId = 3,
                Weight = 10000
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            },
            new UserWeight()
            {
                ExcludeUser = false,
                Id = 6,
                OwnerId = 2,
                UserId = 3,
                Weight = 0
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            }, new UserWeight()
            {
                ExcludeUser = false,
                Id = 7,
                OwnerId = 2,
                UserId = 2,
                Weight = 3000
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            },
            new UserWeight()
            {
                ExcludeUser = true,
                Id = 8,
                OwnerId = 2,
                UserId = 4,
                Weight = 1000
                        ,
                CreateTime = DateTimeOffset.Now,
                UpdateTime = DateTimeOffset.Now
            }
        );
            modelBuilder.Entity<View>()
                    .HasData(
                    new View()
                    {
                        Description = "What a View",
                        Id = 1,
                        ExcludeByDefault = false,
                        ImageUrl = null,
                        IsPrivate = false,
                        Name = "WhatAView",
                        OwnerId = 1,
                        QuestId = 1
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    },
                    new View()
                    {
                        Description = "You View",
                        Id = 2,
                        ExcludeByDefault = false,
                        ImageUrl = null,
                        IsPrivate = false,
                        Name = "YouView",
                        OwnerId = 1,
                        QuestId = 1
                        ,
                        CreateTime = DateTimeOffset.Now,
                        UpdateTime = DateTimeOffset.Now
                    }
            ); 

    base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            var newEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added &&
                    x.Entity != null &&
                    x.Entity as BaseEntity != null
                    )
                .Select(x => x.Entity as BaseEntity);

            var modifiedEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Modified &&
                    x.Entity != null &&
                    x.Entity as BaseEntity != null
                    )
                .Select(x => x.Entity as BaseEntity);

            foreach (var newEntity in newEntities)
            {
                newEntity.CreateTime = DateTime.UtcNow;
                newEntity.UpdateTime = DateTime.UtcNow;
            }

            foreach (var modifiedEntity in modifiedEntities)
            {
                modifiedEntity.UpdateTime = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }
}
