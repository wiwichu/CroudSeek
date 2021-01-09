using AutoMapper;
using CroudSeek.Client.Services;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Components
{
    public partial class AddDataPointDialog
    {
        private DataPointForUpdateDto _dataPoint =  new DataPointForUpdateDto { Name="dp",Description="dp",TimeStamp=DateTime.Now
    };
        public DataPointForUpdateDto DataPoint { get; set; }


        [Inject]
        IMapper Mapper { get; set; }

        [Inject]
        public IDataPointDataService DataPointDataService { get; set; }
        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        [Parameter]
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public InputText NameInputText { get; set; }
        public InputText DescriptionInputText { get; set; }

        public void Show()
        {
            //ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            DataPoint = new DataPointForUpdateDto { Name = "dp", Description = "dp", TimeStamp = DateTime.Now };
        }

        protected async Task HandleValidSubmit()
        {
            var newDP = Mapper.Map<DataPointForCreationDto>(DataPoint);

            var addedDP = await DataPointDataService.CreateDataPointForQuest(QuestId, newDP);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
        }

    }
}
