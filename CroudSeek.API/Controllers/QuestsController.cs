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
using System.Security.Claims;

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
        public ActionResult<IEnumerable<QuestDto>> GetQuests()
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userEntity = _croudSeekRepository.GetUsers().Where((u) => u.Name == user).FirstOrDefault();
            var questsFromRepo = _croudSeekRepository.GetQuests().Where(q=>q.OwnerId==userEntity?.Id || !q.IsPrivate);
            var questDtos = _mapper.Map<IEnumerable<QuestDto>>(questsFromRepo).Select((q)=>
            {
                q.CanEdit = true;
                var questUserEntity = _croudSeekRepository.GetUser(q.OwnerId);
                q.IsOwner = questUserEntity?.Name == user;
                return q;
            });
            return  Ok(questDtos);
        }
        /// <summary>
        /// Get a Quest by Id
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <returns>Quest</returns>
        [HttpGet("{questId}", Name = "GetQuest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public IActionResult GetQuest(int questId)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userEntity = _croudSeekRepository.GetUsers().Where((u) => u.Name == user).FirstOrDefault();

            var questFromRepo = _croudSeekRepository.GetQuest(questId);

            if (questFromRepo == null)
            {
                return NotFound();
            }
            if (!(questFromRepo.OwnerId == userEntity?.Id || !questFromRepo.IsPrivate))
            {
                return Unauthorized();
            }

            var questDto = _mapper.Map<QuestDto>(questFromRepo);

            var questUserEntity = _croudSeekRepository.GetUser(questDto.OwnerId);
            questDto.CanEdit = true;
            questDto.IsOwner = questUserEntity?.Name == user;

            return Ok(questDto);
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
        //[Authorize(Policy = CroudSeek.Shared.Policies.CanManageQuests)]
        [Authorize]
        public ActionResult<QuestDto> CreateQuest(QuestForCreationDto quest)
        {
            //if(quest.Name==quest.Description)
            //{
            //    ModelState.AddModelError("Description",
            //        "Description must be different from Name.");
            //}
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questEntity = _mapper.Map<Entities.Quest>(quest);
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userEntity = _croudSeekRepository.GetUsers().Where((u) => u.Name == user).FirstOrDefault();
            if (userEntity != null)
            {
                questEntity.OwnerId = userEntity.Id;
            }
            _croudSeekRepository.AddQuest(questEntity);
            _croudSeekRepository.Save();

            var questToReturn = _mapper.Map<QuestDto>(questEntity);
            questToReturn.CanEdit = true;
            questToReturn.IsOwner = true;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        //[Authorize(Policy = CroudSeek.Shared.Policies.CanManageQuests)]
        [Authorize]
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
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userEntity = _croudSeekRepository.GetUsers().Where((u) => u.Name == user).FirstOrDefault();

            var questUserEntity = _croudSeekRepository.GetUser(questFromRepo.OwnerId);
            
            if(questUserEntity?.Name != user)
            {
                return Unauthorized();
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json-patch+json")]
        [HttpPatch("{questId}")]
        //[Authorize(Policy = CroudSeek.Shared.Policies.CanManageQuests)]
        [Authorize]
        public ActionResult PartiallyUpdateQuest(int questId,
            JsonPatchDocument<QuestForUpdateDto> patchDocument)
        {

            var questFromRepo = _croudSeekRepository.GetQuest(questId);

            if (questFromRepo == null)
            {
                return NotFound();
            }
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userEntity = _croudSeekRepository.GetUsers().Where((u) => u.Name == user).FirstOrDefault();

            var questUserEntity = _croudSeekRepository.GetUser(questFromRepo.OwnerId);

            if (questUserEntity?.Name != user)
            {
                return Unauthorized();
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        //[Authorize(Policy = CroudSeek.Shared.Policies.CanManageQuests)]
        [Authorize]
        public ActionResult DeleteQuest(int questId)
        {
            var questFromRepo = _croudSeekRepository.GetQuest(questId,true);

            if (questFromRepo == null)
            {
                return NotFound();
            }
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userEntity = _croudSeekRepository.GetUsers().Where((u) => u.Name == user).FirstOrDefault();

            var questUserEntity = _croudSeekRepository.GetUser(questFromRepo.OwnerId);

            if (questUserEntity?.Name != user)
            {
                return Unauthorized();
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
