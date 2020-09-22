using CroudSeek.Core.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public partial class QuestOverview
    {
        [Inject]
        public IQuestDataService QuestDataService { get; set; }
        [Inject]
        public IZoneDataService ZoneDataService { get; set; }
        public List<QuestDto> Quests { get; set; }
        public List<ZoneDto> Zones { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Quests = (await QuestDataService.GetAllQuests()).ToList();
            Zones = (await ZoneDataService.GetAllZones()).ToList();
        }
    }
}
