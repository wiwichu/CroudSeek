using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CroudSeek.Core.Services
{
    public class ViewDataService : IViewDataService
    {
        private readonly HttpClient _httpClient;
        public ViewDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ViewDto>> GetAllViews(int questId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<ViewDto>>
                (await _httpClient.GetStreamAsync($"api/quests/{questId}/views"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ViewDto> GetViewById(int questId, int viewId, bool includeUserWeights)
        {
            return await JsonSerializer.DeserializeAsync<ViewDto>
                (await _httpClient.GetStreamAsync($"api/quests/{questId}/views/{viewId}/?includeUserWeights={includeUserWeights}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ViewDto> AddView(int questId,ViewForCreationDto view)
        {
            var json = JsonSerializer.Serialize(view);

            var viewJson =
                new StringContent(json, Encoding.UTF8, "application/json");


            var uri = new Uri(_httpClient.BaseAddress + $"api/quests/{questId}/views/2");
            var uri2 = new Uri(_httpClient.BaseAddress + $"api/quests/{questId}/datapoints");

            var url = $"api/quests/{questId}/views";

            var response = await _httpClient.PostAsync(url, viewJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ViewDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateView(ViewForUpdateDto view, int viewId,int questId)
        {
            var json = JsonSerializer.Serialize(view);

            var viewJson =
                new StringContent(json, Encoding.UTF8, "application/json");

            //var uri = new Uri(_httpClient.BaseAddress + $"api/quests/{questId}/views/2");
            //var uri2 = new Uri(_httpClient.BaseAddress + $"api/quests/{questId}/datapoints");

            var url = $"​api/quests/{questId}​/views/{viewId}";

            var result = await _httpClient.PutAsync(url, viewJson);
        }

        public async Task DeleteView(int questId,int viewId)
        {
           var result =  await _httpClient.DeleteAsync($"api/quests/{questId}​/views/{viewId}");
        }
    }
}
