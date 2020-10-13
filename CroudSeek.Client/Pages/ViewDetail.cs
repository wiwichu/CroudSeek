using CroudSeek.Client.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Pages
{
    public partial class ViewDetail 
    {
        [Inject]
        public IViewDataService ViewDataService { get; set; }

        [Parameter]
        public string QuestId { get; set; }
        [Parameter]
        public string ViewId { get; set; }
        public ViewDto View { get; set; } = new ViewDto();
        protected override async Task OnInitializedAsync()
        {
            int.TryParse(QuestId, out var questId);
            int.TryParse(ViewId, out var viewId);

            View = await ViewDataService.GetViewById(questId,viewId,true);
        }
    }
}
