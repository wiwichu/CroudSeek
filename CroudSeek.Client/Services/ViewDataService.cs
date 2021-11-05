using Blazored.LocalStorage;
using CroudSeek.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CroudSeek.Client.Services
{
    public class ViewDataService : BaseDataService, IViewDataService
    {
        private readonly HttpClient _httpClient;
        public ViewDataService(HttpClient httpClient, IClient client, ILocalStorageService localStorage, NavigationManager navigation
            , CroudSeek.Client.Contracts.IAuthenticationService authenticationService) : base(client, localStorage, navigation, authenticationService)
        {
            _httpClient = client.HttpClient;
        }

        public async Task<IEnumerable<ViewDto>> GetAllViews(int questId)
        {
            if (await AddBearerToken())
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<ViewDto>>
                    (await _httpClient.GetStreamAsync($"api/quests/{questId}/views"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<ViewDto> GetViewById(int questId, int viewId, bool includeUserWeights)
        {
            if (await AddBearerToken())
            {
                return await JsonSerializer.DeserializeAsync<ViewDto>
                    (await _httpClient.GetStreamAsync($"api/quests/{questId}/views/{viewId}/?includeUserWeights={includeUserWeights}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<ViewDto> AddView(int questId,ViewForCreationDto view)
        {
            //await AddBearerToken();
            var json = JsonSerializer.Serialize(view);

            var viewJson =
                new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"api/quests/{questId}/views";

            var response = await _httpClient.PostAsync(url, viewJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ViewDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<HttpResponseMessage> UpdateView(ViewForUpdateDto view, int viewId,int questId)
        {
            //await AddBearerToken();
            var json = JsonSerializer.Serialize(view);

            var viewJson =
                new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"api/quests/{questId}/views/{viewId}";

            var result = await _httpClient.PutAsync(url, viewJson);
            if (result.IsSuccessStatusCode)
            {
                return result;
            }
            return null;
        }

        public async Task DeleteView(int questId,int viewId)
        {
            //await AddBearerToken();
            var url = $"api/quests/{questId}/views/{viewId}";
            var result =  await _httpClient.DeleteAsync(url);
        }
    }
}
