using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public Task<QuestDto> AddQuest(QuestForCreationDto quest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteQuest(int questId)
        {
            throw new NotImplementedException();
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

        public Task<QuestForUpdateDto> UpdateQuest(QuestForUpdateDto quest)
        {
            throw new NotImplementedException();
        }
    }
}
