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
    public partial class ViewEdit 
    {
        [Inject]
        public IQuestDataService QuestDataService { get; set; }
        [Inject]
        public IViewDataService ViewDataService { get; set; }

        [Inject]
        IMapper Mapper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string QuestId { get; set; } = "0";
        [Parameter]
        public string ViewId { get; set; } = "0";
        public ViewForUpdateDto View { get; set; } = new ViewForUpdateDto();
        public ViewDto ViewDto { get; set; } = new ViewDto();
        public string Title { get; set; } = "Enter Details";
        public List<QuestDto> Quests { get; set; } = new List<QuestDto>();
        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            int.TryParse(QuestId, out var questId);
            int.TryParse(ViewId, out var viewId);

            Quests = (await QuestDataService.GetAllQuests()).ToList();

            if (viewId == 0) //new quest is being created
            {
                //add some defaults
                View = new ViewForUpdateDto
                {
                    IsPrivate = false,
                    Age=-1
                };
            }
            else
            {
                ViewDto = await ViewDataService.GetViewById(questId,viewId,true);

                View = Mapper.Map<ViewForUpdateDto>(ViewDto);
                Title = $"Details for {View.Description}";
            }
        }
        public InputText NameInputText { get; set; }
        public InputText DescriptionInputText { get; set; }
        public InputText ImageInputText { get; set; }
        protected async Task HandleValidSubmit()
        {
            int.TryParse(QuestId, out var questId);
            int.TryParse(ViewId, out var viewId);

            if (viewId == 0) //new
            {
                var newView = Mapper.Map<ViewForCreationDto>(View);
                newView.Age = 1;

                var addedView = await ViewDataService.AddView(questId,newView);
                if (addedView != null)
                {
                    StatusClass = "alert-success";
                    Message = "New view added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new view. Please try again.";
                    Saved = true;
                }
            }
            else
            {
                var newView = Mapper.Map<ViewForUpdateDto>(View);

                var result = await ViewDataService.UpdateView(newView,viewId,questId);
                if (result != null)
                {
                    StatusClass = "alert-success";
                    Message = "View updates successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong updating the view. Please try again.";
                    Saved = true;
                }
            }
        }
        protected async Task DeleteView()
        {
            int.TryParse(QuestId, out var questId);
            int.TryParse(ViewId, out var viewId);

            await ViewDataService.DeleteView(questId,viewId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/questoverview");
        }

    }

}
