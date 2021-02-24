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
    [RoutePrefix("api/referee")]
    public class RefereeController : ApiController
    {
        private readonly IRefereeService _refereeService;
        private readonly IMapper _mapper;

        public RefereeController(IRefereeService refereeService, IMapper mapper)
        {
            _refereeService = refereeService;
            _mapper = mapper;
        }

        [Route("{id}", Name = nameof(GetRefereeByIdAsync))]
        [HttpGet]
        public async Task<IHttpActionResult> GetRefereeByIdAsync(Guid id)
        {
            IReferee Referee = await _refereeService.GetRefereeByIdAsync(id);

            if (Referee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RefereeViewModel>(Referee));
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindRefereesAsync([FromUri] RefereeParameters parameters)
        {
            if (!parameters.IsValid())
            {
                return BadRequest();
            }

            PagedList<IReferee> playerList = await _refereeService.FindRefereesAsync(parameters);

            if (playerList == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PagedList<IReferee>, IEnumerable<RefereeViewModel>>(playerList));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateRefereeAsync([FromBody] RefereeRest refereeRest)
        {
            IReferee referee = _mapper.Map<IReferee>(refereeRest);

            referee = await _refereeService.CreateRefereeAsync(referee);

            if (referee == null)
            {
                BadRequest();
            }

            return CreatedAtRoute(nameof(GetRefereeByIdAsync), new { referee.Id }, _mapper.Map<RefereeViewModel>(referee));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRefereeAsync(Guid id)
        {
            IReferee referee = await _refereeService.GetRefereeByIdAsync(id);

            if (referee == null)
            {
                return NotFound();
            }

            if (!(await _refereeService.DeleteRefereeAsync(id, referee.ByUser)))
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRefereeAsync(Guid id, [FromBody] RefereeRest RefereeRest)
        {
            IReferee referee = await _refereeService.GetRefereeByIdAsync(id);

            if (referee == null)
            {
                return NotFound();
            }

            referee = _mapper.Map(RefereeRest, referee);

            if (!(await _refereeService.UpdateRefereeAsync(referee)))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
