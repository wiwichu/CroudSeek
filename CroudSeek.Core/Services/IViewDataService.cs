using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CroudSeek.Core.Services
{
    public interface IViewDataService
    {
        Task<IEnumerable<ViewDto>> GetAllViews(int questId);
        Task<ViewDto> GetViewById(int questId,int viewId,bool includeUserWeights);
        Task<ViewDto> AddView(int questId,ViewForCreationDto view);
        Task<HttpResponseMessage> UpdateView(ViewForUpdateDto view, int viewId,int questId);
        Task DeleteView(int questId,int viewId);

    }
}
