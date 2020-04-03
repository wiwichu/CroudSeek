using CroudSeek.API.Entities;
using CroudSeek.API.Models;
using CroudSeek.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Controllers
{
    [ApiController]
    [Route("api/quests")]

    public class QuestsController :ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;

        public QuestsController(ICroudSeekRepository croudSeekRepository)
        {
            _croudSeekRepository = croudSeekRepository;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<QuestDto>> GetQuests()
        {
            var result = new List<QuestDto>();
            var questsFromRepo = _croudSeekRepository.GetQuests();
            foreach (var quest in questsFromRepo)
            {
                var q = new QuestDto()
                {
                    Id = quest.Id,
                    Description = quest.Description,
                    IsPrivate = quest.IsPrivate,
                    Name = quest.Name,
                    OwnerId = quest.OwnerId,
                    ZoneId = quest.ZoneId
                };
                result.Add(q);
            }
            return Ok(result);
        }

    }
}
