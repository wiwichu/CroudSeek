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
    [Route("api/users")]

    public class UsersController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public UsersController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>Collection of Users</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var usersFromRepo = _croudSeekRepository.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
        }
        /// <summary>
        /// Get a User by Id
        /// </summary>
        /// <param name="userId">Id of User to get</param>
        /// <returns>User</returns>
        [HttpGet("{userId}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int userId)
        {
            var userFromRepo = _croudSeekRepository.GetUser(userId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(userFromRepo));
        }
        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="user">User details</param>
        /// <returns>New User</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<UserDto> CreateUser(UserForCreationDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = _mapper.Map<Entities.User>(user);
            _croudSeekRepository.AddUser(userEntity);
            _croudSeekRepository.Save();

            var userToReturn = _mapper.Map<QuestDto>(userEntity);
            return CreatedAtRoute("GetUser",
                new { userId = userToReturn.Id },
                userToReturn);
        }
        /// <summary>
        /// Update a User
        /// </summary>
        /// <param name="userId">Id of User</param>
        /// <param name="user">Deatils of User Updates</param>
        /// <returns>Updated User</returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<UserDto> UpdateUser(int userId, UserForUpdateDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFromRepo = _croudSeekRepository.GetUser(userId);

            if (userFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(user, userFromRepo);

            _croudSeekRepository.UpdateUser(userFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Partially Update a User
        /// </summary>
        /// <param name="userId">Id of User</param>
        /// <param name="patchDocument">The set of operations to apply to the User</param>
        /// <returns>Updated User</returns>
        /// <remarks>
        /// Sample request (this request updates the name and description) \
        /// PATCH quests/questId\
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
        [HttpPatch("{userId}")]
        public ActionResult PartiallyUpdateUser(int userId,
            JsonPatchDocument<UserForUpdateDto> patchDocument)
        {

            var userFromRepo = _croudSeekRepository.GetUser(userId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<UserForUpdateDto>(userFromRepo);
            // add validation
            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(userToPatch, userFromRepo);

            _croudSeekRepository.UpdateUser(userFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="userId">Id of User to be deleted</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult DeleteUser(int userId)
        {
            var userFromRepo = _croudSeekRepository.GetUser(userId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            _croudSeekRepository.DeleteUser(userFromRepo);
            _croudSeekRepository.Save();

            return NoContent();
        }
    }
}
