using AutoMapper;
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
        private IMapper _mapper;

        public QuestsController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<QuestDto>> GetQuests()
        {
            var questsFromRepo = _croudSeekRepository.GetQuests();
            return Ok(_mapper.Map <IEnumerable<QuestDto>>(questsFromRepo));
        }

        [HttpGet("{questId}", Name = "GetQuest")]
        public IActionResult GetQuest(int questId)
        {
            var questFromRepo = _croudSeekRepository.GetQuest(questId);

            if (questFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<QuestDto>(questFromRepo));
        }
        [HttpPost]
        public ActionResult<QuestDto> CreateQuest(QuestForCreationDto quest)
        {
            if(quest.Name==quest.Description)
            {
                ModelState.AddModelError("Description",
                    "Description must be different from Name.");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questEntity = _mapper.Map<Entities.Quest>(quest);
            _croudSeekRepository.AddQuest(questEntity);
            _croudSeekRepository.Save();

            var questToReturn = _mapper.Map<QuestDto>(questEntity);
            return CreatedAtRoute("GetQuest",
                new { questId = questToReturn.Id },
                questToReturn);
        }
    }
}
