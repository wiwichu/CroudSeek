using CroudSeek.Core.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public class DataPointDetailBase : ComponentBase
    {
        [Inject]
        public IDataPointDataService DataPointDataService { get; set; }

        [Parameter]
        public string QuestId { get; set; }
        [Parameter]
        public string DataPointId { get; set; }
        public DataPointDto DataPoint { get; set; } = new DataPointDto();
        protected override async Task OnInitializedAsync()
        {
            int.TryParse(QuestId, out var questId);
            int.TryParse(DataPointId, out var dataPointId);

            DataPoint = await DataPointDataService.GetDataPointForQuest(questId, dataPointId);
        }
    }
}
