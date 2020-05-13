using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Services
{
    public interface IZoneDataService
    {
        Task<IEnumerable<ZoneDto>> GetAllZones();
        Task<ZoneDto> GetZoneById(int Id);
    }
}
