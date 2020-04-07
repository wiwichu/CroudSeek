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
    [Route("api/views")]

    public class ViewsController : ControllerBase
    {
        private readonly ICroudSeekRepository _croudSeekRepository;
        private IMapper _mapper;

        public ViewsController(ICroudSeekRepository croudSeekRepository, IMapper mapper)
        {
            _croudSeekRepository = croudSeekRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<ViewDto>> GetViews()
        {
            var viewsFromRepo = _croudSeekRepository.GetViews();
            return Ok(_mapper.Map<IEnumerable<ViewWithoutUserWeightsDto>>(viewsFromRepo));
        }

        [HttpGet("{viewId}", Name = "GetView")]
        public IActionResult GetView(int viewId,bool includeUserWeights=false)
        {
            var viewFromRepo = _croudSeekRepository.GetView(viewId,includeUserWeights);

            if (viewFromRepo == null)
            {
                return NotFound();
            }
            if (includeUserWeights)
            {
                var result = _mapper.Map<ViewDto>(viewFromRepo);
                return Ok(result);
            }
            return Ok(_mapper.Map<ViewWithoutUserWeightsDto>(viewFromRepo));
        }
    }
}
