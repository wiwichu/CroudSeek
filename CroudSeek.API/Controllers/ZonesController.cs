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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetZone(int zoneId)
        {
            var zoneFromRepo = _croudSeekRepository.GetZone(zoneId);

            if (zoneFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ZoneDto>(zoneFromRepo));
        }
        /// <summary>
        /// Create a Zone
        /// </summary>
        /// <param name="zone">Zone details</param>
        /// <returns>New Zone</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        public ActionResult<ZoneDto> CreateZone(ZoneForCreationDto zone)
        {
            if (zone.Name == zone.Description)
            {
                ModelState.AddModelError("Description",
                    "Description must be different from Name.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneEntity = _mapper.Map<Entities.Zone>(zone);
            _croudSeekRepository.AddZone(zoneEntity);
            _croudSeekRepository.Save();

            var zoneToReturn = _mapper.Map<ZoneDto>(zoneEntity);
            return CreatedAtRoute("GetZone",
                new { zoneId = zoneToReturn.Id },
                zoneToReturn);
        }
        /// <summary>
        /// Update a Zone
        /// </summary>
        /// <param name="zoneId">Id of Zone</param>
        /// <param name="zone">Deatils of Zone Updates</param>
        /// <returns>Updated Zone</returns>
        [HttpPut("{zoneId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        public ActionResult<ZoneDto> UpdateZone(int zoneId, ZoneForUpdateDto zone)
        {
            if (zone.Name == zone.Description)
            {
                ModelState.AddModelError("Description",
                    "Description must be different from Name.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneFromRepo = _croudSeekRepository.GetZone(zoneId);

            if (zoneFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(zone, zoneFromRepo);

            _croudSeekRepository.UpdateZone(zoneFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Partially Update a Zone
        /// </summary>
        /// <param name="zoneId">Id of Zone</param>
        /// <param name="patchDocument">The set of operations to apply to the Zone</param>
        /// <returns>Updated Zone</returns>
        /// <remarks>
        /// Sample request (this request updates the name and description) \
        /// PATCH zones/zoneId\
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
        [HttpPatch("{zoneId}")]
        public ActionResult PartiallyUpdateQuest(int zoneId,
            JsonPatchDocument<ZoneForUpdateDto> patchDocument)
        {

            var zoneFromRepo = _croudSeekRepository.GetZone(zoneId);

            if (zoneFromRepo == null)
            {
                return NotFound();
            }

            var zoneToPatch = _mapper.Map<ZoneForUpdateDto>(zoneFromRepo);
            // add validation
            patchDocument.ApplyTo(zoneToPatch, ModelState);

            if (!TryValidateModel(zoneToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(zoneToPatch, zoneFromRepo);

            _croudSeekRepository.UpdateZone(zoneFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Delete a Zone
        /// </summary>
        /// <param name="zoneId">Id of Zone to be deleted</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{zoneId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult DeleteZone(int zoneId)
        {
            var zoneFromRepo = _croudSeekRepository.GetZone(zoneId);

            if (zoneFromRepo == null)
            {
                return NotFound();
            }

            _croudSeekRepository.DeleteZone(zoneFromRepo);
            _croudSeekRepository.Save();

            return NoContent();
        }

    }
}
