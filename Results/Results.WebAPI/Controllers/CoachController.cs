using AutoMapper;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.Person;
using Results.WebAPI.Models.ViewModels.Person;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/coach")]
    public class CoachController : ApiController
    {
        private readonly ICoachService _coachService;
        private readonly IMapper _mapper;

        public CoachController(ICoachService coachService, IMapper mapper)
        {
            _coachService = coachService;
            _mapper = mapper;
        }

        [Route("{id}", Name = nameof(GetCoachByIdAsync))]
        [HttpGet]
        public async Task<IHttpActionResult> GetCoachByIdAsync(Guid id)
        {
            ICoach coach = await _coachService.GetCoachByIdAsync(id);

            if (coach == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CoachViewModel>(coach));
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindCoachesAsync([FromUri] CoachParameters parameters)
        {
            if (!parameters.IsValid())
            {
                return BadRequest();
            }

            PagedList<ICoach> playerList = await _coachService.FindCoachesAsync(parameters);

            if (playerList == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PagedList<ICoach>, IEnumerable<CoachViewModel>>(playerList));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateCoachAsync([FromBody] CoachRest coachRest)
        {
            ICoach coach = _mapper.Map<ICoach>(coachRest);

            coach = await _coachService.CreateCoachAsync(coach);

            if (coach == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute(nameof(GetCoachByIdAsync), new { coach.Id }, _mapper.Map<CoachViewModel>(coach));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCoachAsync(Guid id)
        {
            ICoach coach = await _coachService.GetCoachByIdAsync(id);

            if (coach == null)
            {
                return NotFound();
            }

            if (!(await _coachService.DeleteCoachAsync(id, coach.ByUser)))
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCoachAsync(Guid id, [FromBody] CoachRest coachRest)
        {
            ICoach coach = await _coachService.GetCoachByIdAsync(id);

            if (coach == null)
            {
                return NotFound();
            }

            coach = _mapper.Map(coachRest, coach);

            if (!(await _coachService.UpdateCoachAsync(coach)))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
