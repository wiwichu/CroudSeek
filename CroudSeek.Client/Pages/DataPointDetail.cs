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
    public partial class DataPointDetail 
    {
        [Inject]
        public IDataPointDataService DataPointDataService { get; set; }

        [Parameter]
        public string QuestId { get; set; }
        [Parameter]
        public string DataPointId { get; set; }
        public DataPointDto DataPoint { get; set; } = new DataPointDto();
        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(QuestId, out var questId);
            int.TryParse(DataPointId, out var dataPointId);

            DataPoint = await DataPointDataService.GetDataPointForQuest(questId, dataPointId);
            MapMarkers = new List<Marker>
            {
                  new Marker{Description = $"{DataPoint.Description}",  ShowPopup = false, X = DataPoint.Longitude, Y = DataPoint.Latitude,
                                              RadiusMeters=DataPoint.RadiusMeters,IsNegative=DataPoint.IsNegative, Certainty=DataPoint.Certainty }
            };
        }
    }
}
