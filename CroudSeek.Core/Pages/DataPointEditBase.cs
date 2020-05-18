using AutoMapper;
using CroudSeek.Core.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public class DataPointEditBase : ComponentBase
    {
        [Inject]
        public IQuestDataService QuestDataService { get; set; }
        [Inject]
        public IDataPointDataService DataPointDataService { get; set; }
        [Inject]
        public IZoneDataService ZoneDataService { get; set; }
        [Inject]
        IMapper Mapper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string QuestId { get; set; }
        [Parameter]
        public string DataPointId { get; set; }
        public DataPointForUpdateDto DataPoint { get; set; } = new DataPointForUpdateDto();
        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        public InputText NameInputText { get; set; }
        public InputText DescriptionInputText { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            int.TryParse(QuestId, out var questId);

            int.TryParse(DataPointId, out var dataPointId);

            if(questId==0)
            {
                return;
            }

            if (dataPointId == 0) //new quest is being created
            {
                //add some defaults
                DataPoint = new DataPointForUpdateDto
                {
                    IsPrivate = false,
                    IsNegative=false
                };
            }
            else
            {
                var dataPoint = await DataPointDataService.GetDataPointForQuest(questId, dataPointId);

                DataPoint = Mapper.Map<DataPointForUpdateDto>(dataPoint);
            }
        }

        protected async Task HandleValidSubmit()
        {

        }
        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/questoverview");
        }
        protected async Task DeleteDataPoint()
        {
            await DataPointDataService.DeleteDataPointForQuest(int.Parse(QuestId),int.Parse(DataPointId));

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

    }
}
