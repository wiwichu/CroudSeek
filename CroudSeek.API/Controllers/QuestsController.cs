using AutoMapper;
using CroudSeek.API.Entities;
using CroudSeek.Shared;
using CroudSeek.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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
        /// <summary>
        /// Get All Quests
        /// </summary>
        /// <returns>Collection of Quests</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize(Policy = CroudSeek.Shared.Policies.CanManageQuests)]
        public ActionResult<IEnumerable<QuestDto>> GetQuests()
        {
            var questsFromRepo = _croudSeekRepository.GetQuests();
            return Ok(_mapper.Map <IEnumerable<QuestDto>>(questsFromRepo));
        }
        /// <summary>
        /// Get a Quest by Id
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <returns>Quest</returns>
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
        /// <summary>
        /// Create a Quest
        /// </summary>
        /// <param name="quest">Quest details</param>
        /// <returns>New Quest</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
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
        /// <summary>
        /// Update a Quest
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <param name="quest">Deatils of Quest Updates</param>
        /// <returns>Updated Quest</returns>
        [HttpPut("{questId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        public ActionResult<QuestDto> UpdateQuest(int questId,[FromBody] QuestForUpdateDto quest)
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

            //Only update quest, not datapoints
            questFromRepo.DataPoints.Clear();
            _croudSeekRepository.UpdateQuest(questFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Partially Update a Quest
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <param name="patchDocument">The set of operations to apply to the Quest</param>
        /// <returns>Updated Quest</returns>
        /// <remarks>
        /// Sample request (this request updates the name and description) \
        /// PATCH quests/questId\
        /// [ \
        ///     { \
        ///         "op": "replace", \
        ///         "path": "/name", \
        ///         "value": "NEWNAME" \
        ///     }, \
        ///     { \
        ///         "op": "replace", \
        ///         "path": "/description", \
        ///         "value": "NEWDESCRIPTION" \
        ///     } \
        /// ]
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json-patch+json")]
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
        /// <summary>
        /// Delete a Quest
        /// </summary>
        /// <param name="questId">Id of Quest to be deleted</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{questId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult DeleteQuest(int questId)
        {
            var questFromRepo = _croudSeekRepository.GetQuest(questId,true);

            if (questFromRepo == null)
            {
                return NotFound();
            }
            foreach (var datapoint in questFromRepo.DataPoints)
            {
                _croudSeekRepository.DeleteDataPoint(datapoint);
            }
            _croudSeekRepository.DeleteQuest(questFromRepo);
            _croudSeekRepository.Save();

            return NoContent();
        }
    }
}
