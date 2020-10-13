using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CroudSeek.Client.Services
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
        public async Task<ZoneDto> AddZone(ZoneForCreationDto zone)
        {
            var json = JsonSerializer.Serialize(zone);

            var zoneJson =
                new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/zones", zoneJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ZoneDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteZone(int zoneId)
        {
            await _httpClient.DeleteAsync($"api/zones/{zoneId}");
        }
        public async Task UpdateZone(ZoneForUpdateDto zone, int zoneId)
        {
            var json = JsonSerializer.Serialize(zone);

            var zoneJson =
                new StringContent(json, Encoding.Unicode, "application/json");


            var result = await _httpClient.PutAsync($"api/zones/{zoneId}", zoneJson);
        }

    }
}
