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
