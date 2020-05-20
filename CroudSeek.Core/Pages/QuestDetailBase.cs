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
        [Inject]
        public IViewDataService ViewDataService { get; set; }

        [Parameter]
        public string QuestId { get; set; }
        public QuestWithDataPointsDto Quest { get; set; } = new QuestWithDataPointsDto();
        public List<ViewDto> Views = new List<ViewDto>();
        protected override async Task OnInitializedAsync()
        {
            int.TryParse(QuestId, out var questId);

            Quest = await QuestDataService.GetQuestDetails(questId);
            Views = new List<ViewDto>( await ViewDataService.GetAllViews(questId));
        }
    }
}
