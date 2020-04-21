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

    }
}
