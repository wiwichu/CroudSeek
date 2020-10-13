using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CroudSeek.Shared;

namespace CroudSeek.Client.Services
{
    public interface IQuestDataService
    {
        Task<IEnumerable<QuestDto>> GetAllQuests();
        Task<QuestWithDataPointsDto> GetQuestDetails(int questId);
        Task<QuestDto> AddQuest(QuestForCreationDto quest);
        Task UpdateQuest(QuestForUpdateDto quest, int questId);
        Task DeleteQuest(int questId);
    }
}
