using AutoMapper;
using CroudSeek.API.Models;
using CroudSeek.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        [HttpGet()]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var usersFromRepo = _croudSeekRepository.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
        }

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

    }
}
