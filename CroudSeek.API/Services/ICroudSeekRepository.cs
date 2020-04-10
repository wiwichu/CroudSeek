using CroudSeek.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Services
{
    public interface ICroudSeekRepository
    {
        IEnumerable<DataPoint> GetDataPoints();
        IEnumerable<DataPoint> GetDataPointsByQuest(int questId);
        IEnumerable<DataPoint> GetDataPointsByOwner(int ownerId);
        IEnumerable<Quest> GetQuests();
        IEnumerable<Quest> GetQuestsByOwner(int ownerId);
        IEnumerable<User> GetUsers();
        IEnumerable<UserWeight> GetUserWeights();
        IEnumerable<UserWeight> GetUserWeightsByView(int viewId);
        IEnumerable<UserWeight> GetUserWeightsByUser(int userId);
        IEnumerable<UserWeight> GetUserWeightsByOwner(int ownerId);
        IEnumerable<View> GetViews();
        IEnumerable<View> GetViewsByQuest(int questId);
        IEnumerable<View> GetViewsByOwner(int ownerId);
        IEnumerable<Zone> GetZones();
        IEnumerable<Zone> GetZonesByOwner(int ownerId);
        DataPoint GetDataPoint(int id);
        Quest GetQuest(int id);
        User GetUser(int id);
        UserWeight GetUserWeight(int id);
        View GetView(int id, bool includeUserWeights);
        Zone GetZone(int id);
        void AddQuest(Quest quest);
        bool Save();
    }
}
