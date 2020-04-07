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
    [Route("api/Zones")]

    public class ZonesController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public ZonesController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<ZoneDto>> GetZones()
        {
            var zonesFromRepo = _croudSeekRepository.GetZones();
            return Ok(_mapper.Map<IEnumerable<ZoneDto>>(zonesFromRepo));
        }

        [HttpGet("{zoneId}", Name = "GetZone")]
        public IActionResult GetView(int zoneId)
        {
            var zoneFromRepo = _croudSeekRepository.GetZone(zoneId);

            if (zoneFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ZoneDto>(zoneFromRepo));
        }

    }
}
