using CroudSeek.API.DbContexts;
using CroudSeek.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CroudSeek.API.Services
{
    public class CroudSeekRepository : ICroudSeekRepository
    {
        private readonly CroudSeekContext _context;
        public CroudSeekRepository(CroudSeekContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public DataPoint GetDataPoint(int id)
        {
            return _context.DataPoints.Where((d) => d.Id == id).FirstOrDefault();
        }

        public IEnumerable<DataPoint> GetDataPoints()
        {
            return _context.DataPoints.OrderBy((d) => d.Name).ToList();
        }

        public IEnumerable<DataPoint> GetDataPointsByOwner(int ownerId)
        {
            return _context.DataPoints.Where((d) => d.OwnerId == ownerId).OrderBy((d) => d.Name).ToList();
        }

        public IEnumerable<DataPoint> GetDataPointsByQuest(int questId)
        {
            return _context.DataPoints.Where((d) => d.QuestId == questId).OrderBy((d) => d.Name).ToList();
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

        public View GetView(int id,bool includeUserWeights)
        {
            if (includeUserWeights)
            {
                var newView = _context.Views.Where((d) => d.Id == id)
                    .Select((v) =>
                    new
                    {
                        View = v,
                        UserWeights = v.ViewUserWeights.Select(vu=>vu.UserWeight).ToList()
                    })
                    .FirstOrDefault();
                var endView = newView.View;
                endView.UserWeights = newView.UserWeights;
                return endView;
            }
            return _context.Views.Where((d) => d.Id == id).FirstOrDefault();
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
    }
}
