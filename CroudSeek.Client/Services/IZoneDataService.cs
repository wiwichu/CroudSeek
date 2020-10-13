using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Services
{
    public interface IZoneDataService
    {
        Task<IEnumerable<ZoneDto>> GetAllZones();
        Task<ZoneDto> GetZoneById(int Id);
        Task<ZoneDto> AddZone(ZoneForCreationDto zone);
        Task UpdateZone(ZoneForUpdateDto zone, int zoneId);
        Task DeleteZone(int zoneId);

    }
}
