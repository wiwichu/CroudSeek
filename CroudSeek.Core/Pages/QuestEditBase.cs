using CroudSeek.Core.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public class QuestEditBase : ComponentBase
    {
        [Inject]
        public IQuestDataService QuestDataService { get; set; }
        [Inject]
        public IZoneDataService ZoneDataService { get; set; }
        [Parameter]
        public string QuestId { get; set; }
        public QuestDto Quest { get; set; } = new QuestDto();
        public List<ZoneDto> Zones { get; set; } = new List<ZoneDto>();
        public string ZoneId;

        protected override async Task OnInitializedAsync()
        {
            Zones = (await ZoneDataService.GetAllZones()).ToList();
            Quest = await QuestDataService.GetQuestDetails(int.Parse(QuestId));
            ZoneId = Quest.ZoneId.ToString();
        }
        public InputText NameInputText { get; set; }
        public InputText DescriptionInputText { get; set; }
    }

}
