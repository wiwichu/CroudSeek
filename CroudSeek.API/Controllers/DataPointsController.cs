using AutoMapper;
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
    [Route("api/datapoints")]

    public class DataPointsController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public DataPointsController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<DataPointDto>> GetDataPoints()
        {
            var dataPointsFromRepo = _croudSeekRepository.GetDataPoints();
            return Ok(_mapper.Map<IEnumerable<DataPointDto>>(dataPointsFromRepo));
        }

        [HttpGet("{dataPointId}", Name = "GetDataPoint")]
        public IActionResult GetDataPoint(int dataPointId)
        {
            var dataPointFromRepo = _croudSeekRepository.GetDataPoint(dataPointId);

            if (dataPointFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DataPointDto>(dataPointFromRepo));
        }

    }
}
