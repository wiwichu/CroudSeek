using CroudSeek.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Services
{
    public interface IDataPointDataService
    {
        Task<IEnumerable<DataPointDto>> GetDataPointsForQuest(int questId);
        Task<DataPointDto> CreateDataPointForQuest(int questId, DataPointForCreationDto dataPoint);
        Task DeleteDataPointForQuest(int questId, int dataPointId);
        Task UpdateDataPointForQuest(int questId, int dataPointId, DataPointForUpdateDto dataPoint);
        Task<DataPointDto> GetDataPointForQuest(int questId, int dataPointId);
    }
}

