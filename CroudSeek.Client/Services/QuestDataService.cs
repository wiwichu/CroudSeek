using AutoMapper;
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
    public class QuestDataService : BaseDataService, IQuestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IDataPointDataService _dataPointService;
        public QuestDataService(HttpClient hhttpClient, IMapper mapper, IDataPointDataService dataPointService, IClient client, ILocalStorageService localStorage, NavigationManager navigation) : base(client, localStorage, navigation)
        {
            _httpClient = client.HttpClient;
            _mapper = mapper;
            _dataPointService = dataPointService;
        }
        public async Task<QuestDto> AddQuest(QuestForCreationDto quest)
        {
            if (await AddBearerToken())
            {
                var json = JsonSerializer.Serialize(quest);

                var questJson =
                    new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/quests", questJson);

                if (response.IsSuccessStatusCode)
                {
                    return await JsonSerializer.DeserializeAsync<QuestDto>(await response.Content.ReadAsStreamAsync());
                }
            }
            return null;
        }

        public async Task DeleteQuest(int questId)
        {
            if (await AddBearerToken())
            {
                await _httpClient.DeleteAsync($"api/quests/{questId}");
            }
        }

        public async Task<IEnumerable<QuestDto>> GetAllQuests()
        {
            if (await AddBearerToken())
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<QuestDto>>
                    (await _httpClient.GetStreamAsync($"api/quests"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return null;

        }

        public async Task<QuestWithDataPointsDto> GetQuestDetails(int questId)
        {
            if (await AddBearerToken())
            {
                var quest = await JsonSerializer.DeserializeAsync<QuestDto>
                    (await _httpClient.GetStreamAsync($"api/quests/{questId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                var dpQuest = _mapper.Map<QuestWithDataPointsDto>(quest);

                var dataPoints = await _dataPointService.GetDataPointsForQuest(questId);
                dpQuest.DataPoints = new List<DataPointDto>(dataPoints);

                return dpQuest;
            }
            return null;
        }

        public async Task UpdateQuest(QuestForUpdateDto quest,int questId)
        {
            if (await AddBearerToken())
            {
                var json = JsonSerializer.Serialize(quest);

                var questJson =
                    new StringContent(json, Encoding.UTF8, "application/json");


                var result = await _httpClient.PutAsync($"api/quests/{questId}", questJson);
            }
        }
    }
}
