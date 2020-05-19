using CroudSeek.Core.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public class ZoneOverviewBase:ComponentBase
    {
        [Inject]
        public IZoneDataService ZoneDataService { get; set; }
        public List<ZoneDto> Zones { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Zones = (await ZoneDataService.GetAllZones()).ToList();
        }

    }
}
