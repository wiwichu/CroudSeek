using CroudSeek.Client.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Pages
{
    public partial class ZoneOverview
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
