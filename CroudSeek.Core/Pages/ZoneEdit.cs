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
    public partial class ZoneEdit 
    {
        [Inject]
        public IZoneDataService ZoneDataService { get; set; }
        [Inject]
        IMapper Mapper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string ZoneId { get; set; }
        public ZoneForUpdateDto Zone { get; set; } = new ZoneForUpdateDto();
        public ZoneDto ZoneDto { get; set; } = new ZoneDto();
        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            int.TryParse(ZoneId, out var zoneId);

            if (zoneId != 0)//new zone is being created
            {
                ZoneDto = await ZoneDataService.GetZoneById(zoneId);

                Zone = Mapper.Map<ZoneForUpdateDto>(ZoneDto);
                Title = $"Details for {Zone.Description}";
            }
        }
        public InputText NameInputText { get; set; }
        public InputText DescriptionInputText { get; set; }
        public string Title { get; set; } = "Enter Details";

        protected async Task HandleValidSubmit()
        {
            int.TryParse(ZoneId, out var zoneId);

            if (zoneId == 0) //new
            {
                var newZone = Mapper.Map<ZoneForCreationDto>(Zone);

                var addedZone = await ZoneDataService.AddZone(newZone);
                if (addedZone != null)
                {
                    StatusClass = "alert-success";
                    Message = "New zone added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new zone. Please try again.";
                    Saved = true;
                }
            }
            else
            {
                var newZone = Mapper.Map<ZoneForUpdateDto>(Zone);

                await ZoneDataService.UpdateZone(newZone,zoneId);
                StatusClass = "alert-success";
                Message = "Zone updated successfully.";
                Saved = true;
            }
        }
        protected async Task DeleteZone()
        {
            int.TryParse(ZoneId, out var zoneId);

            await ZoneDataService.DeleteZone(zoneId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/zoneoverview");
        }

    }

}
