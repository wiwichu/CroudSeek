using CroudSeek.Core.Services;
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
        [Inject]
        public IQuestDataService QuestDataService { get; set; }

        [Parameter]
        public string QuestId { get; set; }
        public QuestDto Quest { get; set; } = new QuestDto();
        protected override async Task OnInitializedAsync()
        {
            Quest = await QuestDataService.GetQuestDetails(int.Parse(QuestId));
        }
    }
}
