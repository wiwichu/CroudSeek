using AutoMapper;
using ComponentsLibrary.Map;
using CroudSeek.Client.Components;
using CroudSeek.Client.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Pages
{
    public partial class QuestEdit 
    {
        [Inject]
        public IQuestDataService QuestDataService { get; set; }
        [Inject]
        public IZoneDataService ZoneDataService { get; set; }
        [Inject]
        public IViewDataService ViewDataService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        IMapper Mapper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public List<Marker> MapMarkers { get; set; } = new List<Marker>();
        public bool MapEditable { get { return !string.IsNullOrWhiteSpace(QuestId); } }
        public string MapHeader { get {return !string.IsNullOrWhiteSpace(QuestId) ? "Right Click To Add Datapoint"  : ""; } }
        [Parameter]
        public string QuestId { get; set; }
        public QuestForUpdateDto Quest { get; set; } = new QuestForUpdateDto();
        public QuestWithDataPointsDto QuestDp { get; set; } = new QuestWithDataPointsDto();
        public List<ZoneDto> Zones { get; set; } = new List<ZoneDto>();
        public List<DataPointDto> DataPoints { get; set; } = new List<DataPointDto>();
        protected AddDataPointDialog AddDataPointDialog { get; set; }
        private Map _locationMap = null;
        public Map LocationMap
        {
            get { return _locationMap; }
            set
            {
                _locationMap = value;
                if (!string.IsNullOrWhiteSpace(QuestId) && AddDataPointDialog!=null)
                {
                    int.TryParse(QuestId, out var questId);
                    if (questId != 0) //new quest is being created
                    {
                        AddDataPointDialog.QuestId = questId;
                        _locationMap.RightCLickCallback = (lat, lng) =>
                        {
                            AddDataPointDialog.DataPoint =
                                new DataPointForUpdateDto { Name = "dpNew", Description = "dpNew", Latitude = lat, Longitude = lng, TimeStamp = DateTime.Now };
                            AddDataPointDialog.Show();
                        };
                    }
                }
            }
        }
        public string ZoneId { get; set; }
        public string Title { get; set; } = "Enter Quest Details";
        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        public List<ViewDto> Views = new List<ViewDto>();

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            int.TryParse(QuestId, out var questId);
            MapMarkers = new List<Marker>();

            if (questId == 0) //new quest is being created
            {
                //add some defaults
                Quest = new QuestForUpdateDto
                {
                    IsPrivate = false,
                    ZoneId = -1
                };
            }
            else
            {
                QuestDp = await QuestDataService.GetQuestDetails(questId);
                DataPoints = QuestDp.DataPoints;

                Quest = Mapper.Map<QuestForUpdateDto>(QuestDp);
                Quest.DataPoints = Mapper.Map<IEnumerable<DataPointForCreationDto>>(QuestDp.DataPoints).ToList();
                Title = $"Details for {Quest.Description}";
                Views = new List<ViewDto>(await ViewDataService.GetAllViews(questId));
                foreach (var dataPoint in Quest.DataPoints)
                {
                    MapMarkers.Add(
                        new Marker
                        {
                            Description = $"{dataPoint.Description}",
                            ShowPopup = false,
                            X = dataPoint.Longitude,
                            Y = dataPoint.Latitude
                        });
                }
            }

            Zones = (await ZoneDataService.GetAllZones()).ToList();
            ZoneId = Quest?.ZoneId.ToString();

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
                    Saved = true;
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
        public async void AddDataPointDialog_OnDialogClose()
        {
            int.TryParse(QuestId, out var qId);
            MapMarkers = new List<Marker>();

            if (qId != 0)
            {
                var quest = await QuestDataService.GetQuestDetails(qId);
                DataPoints = quest.DataPoints;
                foreach (var dataPoint in DataPoints)
                {
                    MapMarkers.Add(
                        new Marker
                        {
                            Description = $"{dataPoint.Description}",
                            ShowPopup = false,
                            X = dataPoint.Longitude,
                            Y = dataPoint.Latitude
                        });
                }
            }
            StateHasChanged();
        }
    }

}
