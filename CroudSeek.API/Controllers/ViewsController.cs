using AutoMapper;
using CroudSeek.API.Models;
using CroudSeek.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

namespace CroudSeek.API.Controllers
{
    [ApiController]
    [Route("api/quests/{questId}/views")]

    public class ViewsController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public ViewsController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get Views
        /// </summary>
        /// <returns>Collection of Views</returns>
        [HttpGet()]
        public ActionResult<IEnumerable<ViewDto>> GetViews(int questId)
        {
            var viewsFromRepo = _croudSeekRepository.GetViewsByQuest(questId);
            return Ok(_mapper.Map<IEnumerable<ViewWithoutUserWeightsDto>>(viewsFromRepo));
        }
        /// <summary>
        /// Get View with/without UserWeights
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <param name="viewId">Id of View to get</param>
        /// <param name="includeUserWeights">True to include UserWeights</param>
        /// <returns></returns>
        [HttpGet("{viewId}", Name = "GetView")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetView(int questId, int viewId, bool includeUserWeights=false)
        {
            var viewFromRepo = _croudSeekRepository.GetViewByQuest(questId,viewId,includeUserWeights);

            if (viewFromRepo == null)
            {
                return NotFound();
            }
            if (includeUserWeights)
            {
                var result = _mapper.Map<ViewDto>(viewFromRepo);
                return Ok(result);
            }
            return Ok(_mapper.Map<ViewWithoutUserWeightsDto>(viewFromRepo));
        }
        /// <summary>
        /// Create a View
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <param name="view">View details</param>
        /// <returns>New View</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        public ActionResult<ViewDto> CreateViewForQuest(int questId,ViewForCreationDto view)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var viewEntity = _mapper.Map<Entities.View>(view);
            _croudSeekRepository.AddViewByQuest(questId,viewEntity);
            _croudSeekRepository.Save();

            var viewToReturn = _mapper.Map<ViewDto>(viewEntity);
            return CreatedAtRoute("GetView",
                new {questId=questId, 
                    viewId = viewToReturn.Id },
                viewToReturn);
        }
        /// <summary>
        /// Update a View
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <param name="viewId">Id of View</param>
        /// <param name="view">Deatils of View Updates</param>
        /// <returns>Updated View</returns>
        [HttpPut("{viewId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        public ActionResult<ViewDto> UpdateViewForQuest(int questId,int viewId, ViewForUpdateDto view)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var viewFromRepo = _croudSeekRepository.GetViewByQuest(questId,viewId,true);

            if (viewFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(view, viewFromRepo);

            viewFromRepo.QuestId = questId;

            _croudSeekRepository.UpdateView(viewFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Partially Update a View
        /// </summary>
        /// <param name="viewId">Id of View</param>
        /// <param name="patchDocument">The set of operations to apply to the View</param>
        /// <returns>Updated View</returns>
        /// <remarks>
        /// Sample request (this request updates the name and description) \
        /// PATCH views/viewId\
        /// [ \
        ///     { \
        ///         "op": "replace", \
        ///         "path": "/name", \
        ///         "value": "NEWNAME" \
        ///     }
        /// ]
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json-patch+json")]
        [HttpPatch("{viewId}")]
        public ActionResult PartiallyUpdateView(int viewId,
            JsonPatchDocument<ViewForUpdateDto> patchDocument)
        {

            var viewFromRepo = _croudSeekRepository.GetView(viewId,true);

            if (viewFromRepo == null)
            {
                return NotFound();
            }

            var viewToPatch = _mapper.Map<ViewForUpdateDto>(viewFromRepo);
            // add validation
            patchDocument.ApplyTo(viewToPatch, ModelState);

            if (!TryValidateModel(viewToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(viewToPatch, viewFromRepo);

            _croudSeekRepository.UpdateView(viewFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Delete a View
        /// </summary>
        /// <param name="questId">Id of Quest</param>
        /// <param name="viewId">Id of View to be deleted</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{viewId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult DeleteViewForQuest(int questId,int viewId)
        {
            var viewFromRepo = _croudSeekRepository.GetViewByQuest(questId,viewId,true);

            if (viewFromRepo == null)
            {
                return NotFound();
            }

            _croudSeekRepository.DeleteView(viewFromRepo);
            _croudSeekRepository.Save();

            return NoContent();
        }
    }
}
