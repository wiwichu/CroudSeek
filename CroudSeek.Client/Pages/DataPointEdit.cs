using AutoMapper;
using CroudSeek.Client.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Pages
{
    public partial class DataPointEdit
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
        public string QuestId { get; set; } = "0";
        [Parameter]
        public string DataPointId { get; set; } = "0";
        public DataPointForUpdateDto DataPoint { get; set; } = new DataPointForUpdateDto();
        public DataPointDto DataPointDto { get; set; } = new DataPointDto();
        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        public InputText NameInputText { get; set; }
        public InputText DescriptionInputText { get; set; }
        public List<QuestDto> Quests { get; set; } = new List<QuestDto>();
        public string Title { get; set; } = "Enter Details";

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            int.TryParse(QuestId, out var questId);
            int.TryParse(DataPointId, out var dataPointId);

            Quests = (await QuestDataService.GetAllQuests()).ToList();

            if (questId == 0 || dataPointId == 0) //new quest is being created
            {
                //add some defaults
                DataPoint = new DataPointForUpdateDto
                {
                    IsPrivate = false,
                    IsNegative=false,
                    TimeStamp=DateTimeOffset.Now
                };
            }
            else
            {
                DataPointDto = await DataPointDataService.GetDataPointForQuest(questId, dataPointId);

                QuestId = DataPointDto.QuestId.ToString();
                DataPoint = Mapper.Map<DataPointForUpdateDto>(DataPointDto);
                Title = $"Details for {DataPoint.Description}";

            }
        }

        protected async Task HandleValidSubmit()
        {
            int.TryParse(QuestId, out var questId);
            int.TryParse(DataPointId, out var dataPointId);

            if (dataPointId == 0) //new
            {
                var newDP = Mapper.Map<DataPointForCreationDto>(DataPoint);

                var addedDP = await DataPointDataService.CreateDataPointForQuest(questId, newDP);
                if (addedDP != null)
                {
                    StatusClass = "alert-success";
                    Message = "New datapoint added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new datapoint. Please try again.";
                    Saved = true;
                }
            }
            else
            {
                var newDP = Mapper.Map<DataPointForUpdateDto>(DataPoint);

                await DataPointDataService.UpdateDataPointForQuest(questId, dataPointId,newDP);
                StatusClass = "alert-success";
                Message = "DataPoint updated successfully.";
                Saved = true;
            }
        }
        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/questoverview");
        }
        protected async Task DeleteDataPoint()
        {
            int.TryParse(QuestId, out var questId);
            int.TryParse(DataPointId, out var dataPointId);

            await DataPointDataService.DeleteDataPointForQuest(questId,dataPointId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

    }
}
