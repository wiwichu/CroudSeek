﻿using CroudSeek.Client.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Pages
{
    public partial class ViewOverview
    {
        [Inject]
        public IViewDataService ViewDataService { get; set; }
        [Parameter]
        public String QuestId { get; set; }
        public List<ViewDto> Views { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(QuestId, out var questId);
            Views = (await ViewDataService.GetAllViews(questId)).ToList();
        }

    }
}
