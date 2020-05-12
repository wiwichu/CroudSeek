using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public class QuestDetailBase : ComponentBase
    {
        public List<QuestDto> Quests { get; set; }

        [Parameter]
        public string QuestId { get; set; }
        public QuestDto Quest { get; set; } = new QuestDto();
        protected override Task OnInitializedAsync()
        {
            InitializeQuests();
            Quest = Quests.FirstOrDefault(q => q.Id == int.Parse(QuestId));
            return base.OnInitializedAsync();
        }
        private void InitializeQuests()
        {
            Quests = new List<QuestDto>()
            {
                               new QuestDto()
                {
                    Id = 1,
                    Name = "BigQuest",
                    Description = "The Big Quest",
                    IsPrivate = false,
                    OwnerId = 1,
                    ZoneId = 1,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new QuestDto()
                {
                    Id = 2,
                    Name = "BigBigQuest",
                    Description = "The Big Big Quest",
                    IsPrivate = false,
                    OwnerId = 2,
                    ZoneId = 2,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new QuestDto()
                {
                    Id = 3,
                    Name = "VeryBigQuest",
                    Description = "The Very Big Quest",
                    IsPrivate = false,
                    OwnerId = 3,
                    ZoneId = 3,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new QuestDto()
                {
                    Id = 4,
                    Name = "ReallyBigQuest",
                    Description = "The Really Big Quest",
                    IsPrivate = false,
                    OwnerId = 4,
                    ZoneId = 4,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                },
                new QuestDto()
                {
                    Id = 5,
                    Name = "ReallyVeryBigQuest",
                    Description = "The Really Very Big Quest",
                    IsPrivate = false,
                    OwnerId = 5,
                    ZoneId = 5,
                    CreateTime = DateTimeOffset.Now,
                    UpdateTime = DateTimeOffset.Now
                }

            };
        }
    }
}
