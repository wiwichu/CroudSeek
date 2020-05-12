using CroudSeek.Core.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public class QuestOverviewBase:ComponentBase
    {
        [Inject]
        public IQuestDataService QuestDataService { get; set; }
        public List<QuestDto> Quests { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Quests = (await QuestDataService.GetAllQuests()).ToList();
        }
    }
}
