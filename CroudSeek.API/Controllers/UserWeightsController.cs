using AutoMapper;
using CroudSeek.Shared;
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
    [Route("api/userweights")]

    public class UserWeightsController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public UserWeightsController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<UserWeightDto>> GetUserWeights()
        {
            var userWeightsFromRepo = _croudSeekRepository.GetUserWeights();
            return Ok(_mapper.Map<IEnumerable<UserWeightDto>>(userWeightsFromRepo));
        }

        [HttpGet("{userWeightId}", Name = "GetUserWeight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetUserWeight(int userWeightId)
        {
            var userWeightFromRepo = _croudSeekRepository.GetUserWeight(userWeightId);

            if (userWeightFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserWeightDto>(userWeightFromRepo));
        }
        /// <summary>
        /// Create a UserWeight
        /// </summary>
        /// <param name="userWeight">UserWeight details</param>
        /// <returns>New UserWeight</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        public ActionResult<UserWeightDto> CreateUserWeight(UserWeightForCreationDto userWeight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userFromRepo = _croudSeekRepository.GetUser(userWeight.UserId);

            if (userFromRepo == null)
            {
                return NotFound();
            }
            var userWeightEntity = _mapper.Map<Entities.UserWeight>(userWeight);
            _croudSeekRepository.AddUserWeight(userWeightEntity);
            _croudSeekRepository.Save();

            var userWeightToReturn = _mapper.Map<UserWeightDto>(userWeightEntity);
            return CreatedAtRoute("GetUserWeight",
                new { userWeightId = userWeightToReturn.Id },
                userWeightToReturn);
        }
        /// <summary>
        /// Update a UserWeight
        /// </summary>
        /// <param name="userWeightId">Id of UserWeight</param>
        /// <param name="userWeight">Details of UserWeight Updates</param>
        /// <returns>Updated UserWeight</returns>
        [HttpPut("{userWeightId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        public ActionResult<UserWeightDto> UpdateUserWeight(int userWeightId, UserWeightForUpdateDto userWeight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userWeightFromRepo = _croudSeekRepository.GetUserWeight(userWeightId);

            if (userWeightFromRepo == null)
            {
                return NotFound();
            }
            var userFromRepo = _croudSeekRepository.GetUser(userWeight.UserId);

            if (userFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(userWeight, userWeightFromRepo);

            _croudSeekRepository.UpdateUserWeight(userWeightFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Partially Update a UserWeight
        /// </summary>
        /// <param name="userWeightId">Id of UserWeight</param>
        /// <param name="patchDocument">The set of operations to apply to the UserWeight</param>
        /// <returns>Updated UserWeight</returns>
        /// <remarks>
        /// Sample request (this request updates the name and description) \
        /// PATCH userWeights\Id\
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
        [HttpPatch("{userWeightId}")]
        public ActionResult PartiallyUpdateUserWeight(int userWeightId,
            JsonPatchDocument<UserWeightForUpdateDto> patchDocument)
        {

            var userWeightFromRepo = _croudSeekRepository.GetUserWeight(userWeightId);

            if (userWeightFromRepo == null)
            {
                return NotFound();
            }

            var userWeightToPatch = _mapper.Map<UserWeightForUpdateDto>(userWeightFromRepo);
            // add validation
            patchDocument.ApplyTo(userWeightToPatch, ModelState);

            if (!TryValidateModel(userWeightToPatch))
            {
                return ValidationProblem(ModelState);
            }
            var userFromRepo = _croudSeekRepository.GetUser(userWeightToPatch.UserId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(userWeightToPatch, userWeightFromRepo);

            _croudSeekRepository.UpdateUserWeight(userWeightFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Delete a UserWeight
        /// </summary>
        /// <param name="userWeightId">Id of UserWeight to be deleted</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{userWeightId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult DeleteUserWeight(int userWeightId)
        {
            var userWeightFromRepo = _croudSeekRepository.GetUserWeight(userWeightId);

            if (userWeightFromRepo == null)
            {
                return NotFound();
            }

            _croudSeekRepository.DeleteUserWeight(userWeightFromRepo);
            _croudSeekRepository.Save();

            return NoContent();
        }
    }
}
