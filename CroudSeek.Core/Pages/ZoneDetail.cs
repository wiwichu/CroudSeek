using CroudSeek.Core.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Pages
{
    public partial class ZoneDetail 
    {
        [Inject]
        public IZoneDataService ZoneDataService { get; set; }

        [Parameter]
        public string ZoneId { get; set; }
        public ZoneDto Zone { get; set; } = new ZoneDto();
        protected override async Task OnInitializedAsync()
        {
            int.TryParse(ZoneId, out var zoneId);

            Zone = await ZoneDataService.GetZoneById(zoneId);
        }
    }
}
