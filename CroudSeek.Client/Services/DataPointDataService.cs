﻿using AutoMapper;
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
    public class DataPointDataService : BaseDataService, IDataPointDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public DataPointDataService(HttpClient httpClient, IMapper mapper, IClient client, ILocalStorageService localStorage, NavigationManager navigation
            , CroudSeek.Client.Contracts.IAuthenticationService authenticationService) 
            : base(client, localStorage, navigation, authenticationService)
        {
            //_httpClient = httpClient;
            _httpClient = client.HttpClient;
            _mapper = mapper;
        }

        public async Task<DataPointDto> CreateDataPointForQuest(int questId, DataPointForCreationDto dataPoint)
        {
            if (await AddBearerToken())
            {
                var json = JsonSerializer.Serialize(dataPoint);

                var dpJson = new StringContent(json, Encoding.UTF8, "application/json");

                var url = _httpClient.BaseAddress + "api/quests/3​/views";

                var uri = new Uri(_httpClient.BaseAddress + $"api/quests/{questId}/datapoints");


                var response = await _httpClient.PostAsync($"api/quests/{questId}/datapoints", dpJson);

                if (response.IsSuccessStatusCode)
                {
                    return await JsonSerializer.DeserializeAsync<DataPointDto>(await response.Content.ReadAsStreamAsync());
                }
            }
            return null;
        }

        public async Task DeleteDataPointForQuest(int questId, int dataPointId)
        {
            //await AddBearerToken();
            await _httpClient.DeleteAsync($"api/quests/{questId}/datapoints/{dataPointId}");
        }

        public async Task<DataPointDto> GetDataPointForQuest(int questId, int dataPointId)
        {
            if (await AddBearerToken(true))
            {
                var dataPoint = await JsonSerializer.DeserializeAsync<DataPointDto>
                    (await _httpClient.GetStreamAsync($"api/quests/{questId}/datapoints/{dataPointId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return dataPoint;
            }
            return null;
        }

        public async Task<IEnumerable<DataPointDto>> GetDataPointsForQuest(int questId)
        {
            if (await AddBearerToken(true))
            {
                var dataPoints = await JsonSerializer.DeserializeAsync<IEnumerable<DataPointDto>>
                    (await _httpClient.GetStreamAsync($"api/quests/{questId}/datapoints"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return dataPoints;
            }
            return null;
        }

        public async Task UpdateDataPointForQuest(int questId, int dataPointId, DataPointForUpdateDto dataPoint)
        {
            //await AddBearerToken();
            var json = JsonSerializer.Serialize(dataPoint);

            var dpJson =
                new StringContent(json, Encoding.UTF8, "application/json");


            var result = await _httpClient.PutAsync($"api/quests/{questId}/datapoints/{dataPointId}", dpJson);
        }
    }
}
