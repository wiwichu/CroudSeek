using AutoMapper;
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
        [Inject]
        IMapper Mapper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string QuestId { get; set; }
        public QuestForUpdateDto Quest { get; set; } = new QuestForUpdateDto();
        public QuestWithDataPointsDto QuestDp { get; set; } = new QuestWithDataPointsDto();
        public List<ZoneDto> Zones { get; set; } = new List<ZoneDto>();
        public List<DataPointDto> DataPoints { get; set; } = new List<DataPointDto>();
        public string ZoneId;
        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            int.TryParse(QuestId, out var questId);

            if (questId == 0) //new quest is being created
            {
                //add some defaults
                Quest = new QuestForUpdateDto
                {
                    IsPrivate = false,
                    ZoneId = 1
                };
            }
            else
            {
                QuestDp = await QuestDataService.GetQuestDetails(questId);
                DataPoints = QuestDp.DataPoints;

                Quest = Mapper.Map<QuestForUpdateDto>(QuestDp);
                Quest.DataPoints = Mapper.Map<IEnumerable<DataPointForCreationDto>>(QuestDp.DataPoints).ToList();
                    
            }


            Zones = (await ZoneDataService.GetAllZones()).ToList();
            ZoneId = Quest.ZoneId.ToString();
        }
        public InputText NameInputText { get; set; }
        public InputText DescriptionInputText { get; set; }
        protected async Task HandleValidSubmit()
        {
            int.TryParse(QuestId, out var questId);

            Quest.ZoneId = int.Parse(ZoneId);

            if (questId == 0) //new
            {
                var newQuest = Mapper.Map<QuestForCreationDto>(Quest);

                var addedQuest = await QuestDataService.AddQuest(newQuest);
                if (addedQuest != null)
                {
                    StatusClass = "alert-success";
                    Message = "New quest added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new quest. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                var newQuest = Mapper.Map<QuestForUpdateDto>(Quest);

                await QuestDataService.UpdateQuest(newQuest,questId);
                StatusClass = "alert-success";
                Message = "Quest updated successfully.";
                Saved = true;
            }
        }
        protected async Task DeleteQuest()
        {
            int.TryParse(QuestId, out var questId);

            await QuestDataService.DeleteQuest(questId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/questoverview");
        }

    }

}
