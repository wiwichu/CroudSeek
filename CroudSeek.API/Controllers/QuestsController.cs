using AutoMapper;
using CroudSeek.API.Entities;
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
    [Route("api/quests")]

    public class QuestsController :ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public QuestsController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<QuestDto>> GetQuests()
        {
            var questsFromRepo = _croudSeekRepository.GetQuests();
            return Ok(_mapper.Map <IEnumerable<QuestDto>>(questsFromRepo));
        }

    }
}
