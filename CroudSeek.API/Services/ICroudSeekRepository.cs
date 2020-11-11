using CroudSeek.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Services
{
    public interface ICroudSeekRepository
    {
        IEnumerable<DataPoint> GetDataPointsByQuest(int questId);
        DataPoint GetDataPoint(int questId, int dataPointId);
        void UpdateDataPoint(DataPoint dataPoint);
        void DeleteDataPoint(DataPoint dataPoint);
        void AddDataPoint(int questId, DataPoint dataPoint);
        View GetView(int id, bool includeUserWeights);
        View GetViewByQuest(int questId, int viewId, bool includeUserWeights);
        IEnumerable<View> GetViews();
        IEnumerable<View> GetViewsByOwner(int ownerId);
        IEnumerable<View> GetViewsByQuest(int questId);
        View AddView(View view);
        void UpdateView(View view);
        void DeleteView(View view);
        View AddViewByQuest(int questId, View view);
        IEnumerable<Quest> GetQuests();
        IEnumerable<Quest> GetQuestsByOwner(int ownerId);
        IEnumerable<User> GetUsers();
        IEnumerable<UserWeight> GetUserWeights();
        IEnumerable<UserWeight> GetUserWeightsByView(int viewId);
        IEnumerable<UserWeight> GetUserWeightsByUser(int userId);
        IEnumerable<UserWeight> GetUserWeightsByOwner(int ownerId);
        IEnumerable<Zone> GetZones();
        IEnumerable<Zone> GetZonesByOwner(int ownerId);
        Quest GetQuest(int id, bool includeDataPoints=false);
        User GetUser(int id);
        UserWeight GetUserWeight(int id);
        Zone GetZone(int id);
        void AddZone(Zone zone);
        void AddQuest(Quest quest);
        void UpdateQuest(Quest quest);
        void UpdateZone(Zone zone);
        void DeleteQuest(Quest quest);
        void DeleteZone(Zone zone);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool QuestExists(int questId);
        bool Save();
        UserWeight AddUserWeight(UserWeight userWeight);
        void UpdateUserWeight(UserWeight userWeight);
        void DeleteUserWeight(UserWeight userWeight);
        //IEnumerable<DataPoint> GetDataPoints();
        //IEnumerable<DataPoint> GetDataPointsByOwner(int ownerId);
        //        DataPoint GetDataPoint(int id);
        ApplicationUserProfile GetApplicationUserProfile(string subject);
        void AddApplicationUserProfile(ApplicationUserProfile applicationUserProfile);
        bool ApplicationUserProfileExists(string subject);

    }
}
