using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CroudSeek.Core.Services
{
    public class QuestDataService : IQuestDataService
    {
        private readonly HttpClient _httpClient;
        public QuestDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<QuestDto> AddQuest(QuestForCreationDto quest)
        {
            var json = JsonSerializer.Serialize(quest);

            var questJson =
                new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("api/quests", questJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<QuestDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteQuest(int questId)
        {
                await _httpClient.DeleteAsync($"api/quests/{questId}");
        }

        public async Task<IEnumerable<QuestDto>> GetAllQuests()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<QuestDto>>
                (await _httpClient.GetStreamAsync($"api/quests"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<QuestDto> GetQuestDetails(int questId)
        {
            return await JsonSerializer.DeserializeAsync<QuestDto>
                (await _httpClient.GetStreamAsync($"api/quests/{questId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateQuest(QuestForUpdateDto quest,int questId)
        {
            var json = JsonSerializer.Serialize(quest);

            var questJson =
                new StringContent(json, Encoding.Unicode, "application/json");


            var result = await _httpClient.PutAsync($"api/quests/{questId}", questJson);
        }
    }
}
