using AutoMapper;
using CroudSeek.API.Models;
using CroudSeek.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Controllers
{
    [ApiController]
    [Route("api/quests/{questId}/datapoints")]

    public class DataPointsController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public DataPointsController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get the datapoints for a particular quest
        /// </summary>
        /// <param name="questId">The Id of the quest the datapoints belong to</param>
        /// <returns>A list of Datapoints belonging to the quest.</returns>
        /// <response code="200">Returns the DataPoints for the Quest Id</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<DataPointDto>> GetDataPointsForQuest(int questId)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }

            var dataPointsForQuestFromRepo = _croudSeekRepository.GetDataPoints(questId);
            return Ok(_mapper.Map<IEnumerable<DataPointDto>>(dataPointsForQuestFromRepo));
        }

        //[HttpGet()]
        //public ActionResult<IEnumerable<DataPointDto>> GetDataPoints()
        //{
        //    var dataPointsFromRepo = _croudSeekRepository.GetDataPoints();
        //    return Ok(_mapper.Map<IEnumerable<DataPointDto>>(dataPointsFromRepo));
        //}

        //[HttpGet("{dataPointId}", Name = "GetDataPoint")]
        //public IActionResult GetDataPoint(int dataPointId)
        //{
        //    var dataPointFromRepo = _croudSeekRepository.GetDataPoint(dataPointId);

        //    if (dataPointFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(_mapper.Map<DataPointDto>(dataPointFromRepo));
        //}
        [HttpPost]
        public ActionResult<DataPointDto> CreateDataPointForQuest(
            int questId, DataPointForCreationDto dataPoint)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }

            var dataPointEntity = _mapper.Map<Entities.DataPoint>(dataPoint);
            _croudSeekRepository.AddDataPoint(questId, dataPointEntity);
            _croudSeekRepository.Save();

            var dataPointToReturn = _mapper.Map<DataPointDto>(dataPointEntity);
            return CreatedAtRoute("GetDataPointForQuest",
                new { questId = questId, dataPointId = dataPointToReturn.Id },
                dataPointToReturn);
        }
        /// <summary>
        /// Partially update a DataPoint for a Quest
        /// </summary>
        /// <param name="questId">Id of Quest the DataPoint belongs to</param>
        /// <param name="dataPointId">Id of DAtaPoint</param>
        /// <param name="patchDocument">The set of operations to apply to the DataPoint</param>
        /// <returns>An ActionResult </returns>
        /// <remarks>
        /// Sample request (this request updates the name and description) \
        /// PATCH quests/questId/datapoints/dataPointId \
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
        [HttpPatch("{dataPointId}")]
        public ActionResult PartiallyUpdateDataPointForQuest(int questId,
            int dataPointId,
            JsonPatchDocument<DataPointForUpdateDto> patchDocument)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }

            var dataPointForQuestFromRepo = _croudSeekRepository.GetDataPoint(questId, dataPointId);

            if (dataPointForQuestFromRepo == null)
            {
                var dataPointDto = new DataPointForUpdateDto();
                patchDocument.ApplyTo(dataPointDto, ModelState);

                if (!TryValidateModel(dataPointDto))
                {
                    return ValidationProblem(ModelState);
                }

                var dataPointToAdd = _mapper.Map<Entities.DataPoint>(dataPointDto);
                dataPointToAdd.Id = dataPointId;

                _croudSeekRepository.AddDataPoint(questId, dataPointToAdd);
                _croudSeekRepository.Save();

                var dataPointToReturn = _mapper.Map<DataPointDto>(dataPointToAdd);

                return CreatedAtRoute("GetCourseForAuthor",
                    new { questId, dataPointId = dataPointToReturn.Id },
                    dataPointToReturn);
            }

            var dataPointToPatch = _mapper.Map<DataPointForUpdateDto>(dataPointForQuestFromRepo);
            // add validation
            patchDocument.ApplyTo(dataPointToPatch, ModelState);

            if (!TryValidateModel(dataPointToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(dataPointToPatch, dataPointForQuestFromRepo);

            _croudSeekRepository.UpdateDataPoint(dataPointForQuestFromRepo);

            _croudSeekRepository.Save();

            return NoContent();
        }
        [HttpDelete("{dataPointId}")]
        public ActionResult DeleteDataPointForQuest(int questId, int dataPointId)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }

            var dataPointForQuestFromRepo = _croudSeekRepository.GetDataPoint(questId, dataPointId);

            if (dataPointForQuestFromRepo == null)
            {
                return NotFound();
            }

            _croudSeekRepository.DeleteDataPoint(dataPointForQuestFromRepo);
            _croudSeekRepository.Save();

            return NoContent();
        }

        [HttpPut("{dataPointId}")]
        public IActionResult UpdateDataPointForQuest(int questId,
            int dataPointId,
            DataPointForUpdateDto dataPoint)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }

            var dataPointForQuestFromRepo = _croudSeekRepository.GetDataPoint(questId, dataPointId);

            if (dataPointForQuestFromRepo == null)
            {
                var dataPointToAdd = _mapper.Map<Entities.DataPoint>(dataPoint);
                dataPointToAdd.Id = dataPointId;

                _croudSeekRepository.AddDataPoint(questId, dataPointToAdd);

                _croudSeekRepository.Save();

                var dataPointToReturn = _mapper.Map<DataPointDto>(dataPointToAdd);

                return CreatedAtRoute("GetdataPointForQuest",
                    new { questId, dataPointId = dataPointToReturn.Id },
                    dataPointToReturn);
            }

            // map the entity to a CourseForUpdateDto
            // apply the updated field values to that dto
            // map the CourseForUpdateDto back to an entity
            _mapper.Map(dataPoint, dataPointForQuestFromRepo);

            _croudSeekRepository.UpdateDataPoint(dataPointForQuestFromRepo);

            _croudSeekRepository.Save();
            return NoContent();
        }

        [HttpGet("{dataPointId}", Name = "GetDataPointForQuest")]
        public ActionResult<DataPointDto> GetDataPointForQuest(int questId, int dataPointId)
        {
            if (!_croudSeekRepository.QuestExists(questId))
            {
                return NotFound();
            }

            var dataPointForQuestFromRepo = _croudSeekRepository.GetDataPoint(questId,dataPointId);

            if (dataPointForQuestFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DataPointDto>(dataPointForQuestFromRepo));
        }

    }
}
