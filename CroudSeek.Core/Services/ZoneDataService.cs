using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CroudSeek.Core.Services
{
    public class ZoneDataService : IZoneDataService
    {
        private readonly HttpClient _httpClient;
        public ZoneDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ZoneDto>> GetAllZones()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<ZoneDto>>
                (await _httpClient.GetStreamAsync($"api/zones"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ZoneDto> GetZoneById(int Id)
        {
            return await JsonSerializer.DeserializeAsync<ZoneDto>
                (await _httpClient.GetStreamAsync($"api/zones/{Id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
