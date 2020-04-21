using AutoMapper;
using CroudSeek.API.Entities;
using CroudSeek.API.Models;
using CroudSeek.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
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
        [HttpPut("{questId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<QuestDto> UpdateQuestint(int questId,QuestForUpdateDto quest)
        {
            if (quest.Name == quest.Description)
            {
                ModelState.AddModelError("Description",
                    "Description must be different from Name.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questFromRepo = _croudSeekRepository.GetQuest(questId);

            if (questFromRepo == null)
            {
                return NotFound();
                //var questEntity = _mapper.Map<Entities.Quest>(quest);
                //_croudSeekRepository.AddQuest(questEntity);
                //_croudSeekRepository.Save();

                //var questToReturn = _mapper.Map<QuestDto>(questEntity);
                //return CreatedAtRoute("GetQuest",
                //    new { questId = questToReturn.Id },
                //    questToReturn);
            }
            _mapper.Map(quest, questFromRepo);

            _croudSeekRepository.UpdateQuest(questFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{questId}")]
        public ActionResult PartiallyUpdateQuest(int questId,
            JsonPatchDocument<QuestForUpdateDto> patchDocument)
        {

            var questFromRepo = _croudSeekRepository.GetQuest(questId);

            if (questFromRepo == null)
            {
                return NotFound();
            }

            var questToPatch = _mapper.Map<QuestForUpdateDto>(questFromRepo);
            // add validation
            patchDocument.ApplyTo(questToPatch, ModelState);

            if (!TryValidateModel(questToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(questToPatch, questFromRepo);

            _croudSeekRepository.UpdateQuest(questFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        [HttpDelete("{questId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult DeleteQuest(int questId)
        {
            var questFromRepo = _croudSeekRepository.GetQuest(questId);

            if (questFromRepo == null)
            {
                return NotFound();
            }

            _croudSeekRepository.DeleteQuest(questFromRepo);
            _croudSeekRepository.Save();

            return NoContent();
        }
    }
}
