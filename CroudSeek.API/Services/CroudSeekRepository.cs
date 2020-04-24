using CroudSeek.API.DbContexts;
using CroudSeek.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CroudSeek.API.Services
{
    public class CroudSeekRepository : ICroudSeekRepository, IDisposable
    {
        private readonly CroudSeekContext _context;
        public CroudSeekRepository(CroudSeekContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<DataPoint> GetDataPointsByQuest(int questId)
        {
            return _context.DataPoints.Where((d) => d.QuestId == questId).OrderBy((d) => d.Name).ToList();
        }

        public DataPoint GetDataPoint(int questId, int dataPointId)
        {
            return _context.DataPoints
              .Where(c => c.QuestId == questId && c.Id == dataPointId).FirstOrDefault();
        }
        public void AddDataPoint(int questId, DataPoint dataPoint)
        {
            if (dataPoint == null)
            {
                throw new ArgumentNullException(nameof(dataPoint));
            }
            // always set the AuthorId to the passed-in authorId
            dataPoint.QuestId = questId;
            _context.DataPoints.Add(dataPoint);
        }
        public void UpdateDataPoint(DataPoint dataPoint)
        {
            // no code in this implementation
        }
        public void DeleteDataPoint(DataPoint dataPoint)
        {
            _context.DataPoints.Remove(dataPoint);
        }
        public View GetView(int id, bool includeUserWeights)
        {
            if (includeUserWeights)
            {
                var newView = _context.Views.Where((d) => d.Id == id)
                    .Select((v) =>
                    new
                    {
                        View = v,
                        UserWeights = v.ViewUserWeights.Select(vu => vu.UserWeight).ToList()
                    })
                    .FirstOrDefault();
                var endView = newView.View;
                endView.UserWeights = newView.UserWeights;
                return endView;
            }
            return _context.Views.Where((d) => d.Id == id).FirstOrDefault();
        }
        public View GetViewByQuest(int questId,int viewId, bool includeUserWeights)
        {
            if (includeUserWeights)
            {
                var newView = _context.Views.Where((v) => v.QuestId == questId && v.Id == viewId)
                    .Select((v) =>
                    new
                    {
                        View = v,
                        UserWeights = v.ViewUserWeights.Select(vu => vu.UserWeight).ToList()
                    })
                    .FirstOrDefault();
                if (newView == null)
                {
                    return null;
                }
                var endView = newView.View;
                endView.UserWeights = newView.UserWeights;
                return endView;
            }
            return _context.Views.Where((v) => v.QuestId == questId && v.Id == viewId).FirstOrDefault();
        }

        public IEnumerable<View> GetViews()
        {
            return _context.Views.OrderBy((d) => d.Name).ToList();
        }

        public IEnumerable<View> GetViewsByOwner(int ownerId)
        {
            return _context.Views.Where((d) => d.OwnerId == ownerId).OrderBy((d) => d.Name).ToList();
        }

        public IEnumerable<View> GetViewsByQuest(int questId)
        {
            return _context.Views.Where((d) => d.QuestId == questId).OrderBy((d) => d.Name).ToList();
        }
        public View AddView(View view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            var newView = _context.Views.Add(view);
            UpdateViewUserWeights(view);
            return newView?.Entity;
        }
        private void UpdateViewUserWeights(View view)
        {
            _context.ViewUserWeights.RemoveRange(_context.ViewUserWeights.Where((vuw) => vuw.ViewId == view.Id));
            foreach (var userWeight in view.UserWeights)
            {
                var newEntity = AddUserWeight(userWeight);
                _context.ViewUserWeights.Add(
                   new ViewUserWeight
                   {
                       ViewId = view.Id,
                       UserWeightId = newEntity.Id,
                       View = view,
                       UserWeight = newEntity
                   });
            }
        }
        public void UpdateView(View view)
        {
            UpdateViewUserWeights(view);
        }
        public void DeleteView(View view)
        {
            _context.ViewUserWeights.RemoveRange(_context.ViewUserWeights.Where((vuw) => vuw.ViewId == view.Id));
            _context.Views.Remove(view);
        }
        public View AddViewByQuest(int questId, View view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            // always set the AuthorId to the passed-in authorId
            view.QuestId = questId;
            return AddView(view);
        }

        public Quest GetQuest(int id)
        {
            return _context.Quests.Where((d) => d.Id == id).FirstOrDefault();
        }

        public IEnumerable<Quest> GetQuests()
        {
            return _context.Quests.OrderBy((d) => d.Name).ToList();
        }

        public IEnumerable<Quest> GetQuestsByOwner(int ownerId)
        {
            return _context.Quests.Where((d) => d.OwnerId == ownerId).OrderBy((d) => d.Name).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where((d) => d.Id == id).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy((d) => d.Name).ToList();
        }

        public UserWeight GetUserWeight(int id)
        {
            return _context.UserWeights.Where((d) => d.Id == id).FirstOrDefault();
        }

        public IEnumerable<UserWeight> GetUserWeights()
        {
            return _context.UserWeights.OrderBy((d) => d.UserId).ToList();
        }

        public IEnumerable<UserWeight> GetUserWeightsByOwner(int ownerId)
        {
            return _context.UserWeights.Where((d) => d.OwnerId == ownerId).ToList();
        }

        public IEnumerable<UserWeight> GetUserWeightsByUser(int userId)
        {
            return _context.UserWeights.Where((d) => d.UserId == userId).ToList();
        }

        public IEnumerable<UserWeight> GetUserWeightsByView(int viewId)
        {
            return _context.ViewUserWeights
                .Where((u) => u.ViewId == viewId)
                .SelectMany((i) => _context.UserWeights
                .Where((w) => w.Id == i.UserWeightId))
                .AsEnumerable().ToList();
        }
        public Zone GetZone(int id)
        {
            return _context.Zones.Where((d) => d.Id == id).FirstOrDefault();
        }

        public IEnumerable<Zone> GetZones()
        {
            return _context.Zones.OrderBy((d) => d.Name).ToList();
        }

        public IEnumerable<Zone> GetZonesByOwner(int ownerId)
        {
            return _context.Zones.Where((d) => d.OwnerId == ownerId).OrderBy((d) => d.Name).ToList();
        }
        public void AddQuest(Quest quest)
        {
            if (quest == null)
            {
                throw new ArgumentNullException(nameof(quest));
            }
            _context.Quests.Add(quest);
        }
        public void UpdateQuest(Quest quest)
        {
            //no code in this implementation
        }
        public void DeleteQuest(Quest quest)
        {
            _context.Quests.Remove(quest);
        }
        //
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }
        public UserWeight AddUserWeight(UserWeight userWeight)
        {
            if (userWeight == null)
            {
                throw new ArgumentNullException(nameof(userWeight));
            }
            return _context.UserWeights.Add(userWeight)?.Entity;
        }
        public void UpdateUserWeight(UserWeight userWeight)
        {
            //no code in this implementation
        }
        public void DeleteUserWeight(UserWeight userWeight)
        {
            _context.UserWeights.Remove(userWeight);
        }

        public void UpdateUser(User user)
        {
            //no code in this implementation
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }


        public bool QuestExists(int questId)
        {

            return _context.Quests.Any(a => a.Id == questId);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        //public IEnumerable<DataPoint> GetDataPoints()
        //{
        //    return _context.DataPoints.OrderBy((d) => d.Name).ToList();
        //}

        //public IEnumerable<DataPoint> GetDataPointsByOwner(int ownerId)
        //{
        //    return _context.DataPoints.Where((d) => d.OwnerId == ownerId).OrderBy((d) => d.Name).ToList();
        //}
        //public DataPoint GetDataPoint(int id)
        //{
        //    return _context.DataPoints.Where((d) => d.Id == id).FirstOrDefault();
        //}
    }
}
