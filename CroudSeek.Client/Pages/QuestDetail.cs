using ComponentsLibrary.Map;
using CroudSeek.Client.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Pages
{
    public partial class QuestDetail 
    {
        [Inject]
        public IQuestDataService QuestDataService { get; set; }
        [Inject]
        public IViewDataService ViewDataService { get; set; }
        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        [Parameter]
        public string QuestId { get; set; }
        public QuestWithDataPointsDto Quest { get; set; } = new QuestWithDataPointsDto();
        public List<ViewDto> Views = new List<ViewDto>();
        protected override async Task OnInitializedAsync()
        {
            int.TryParse(QuestId, out var questId);

            Quest = await QuestDataService.GetQuestDetails(questId);
            Views = new List<ViewDto>( await ViewDataService.GetAllViews(questId));
            MapMarkers = new List<Marker>();
            foreach (var dataPoint in Quest.DataPoints)
                MapMarkers.Add(
                     new Marker
                     {
                         Description = $"{dataPoint.Description}",
                         ShowPopup = false,
                         X = dataPoint.Longitude,
                         Y = dataPoint.Latitude,
                         RadiusMeters = dataPoint.RadiusMeters,
                         IsNegative = dataPoint.IsNegative,
                         Certainty = dataPoint.Certainty
                     });
        }
    }
}
