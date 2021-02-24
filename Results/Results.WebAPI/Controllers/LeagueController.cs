using AutoMapper;
using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.League;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/league")]
    public class LeagueController : ApiController
    {
        private readonly ILeagueService _leagueService;
        private readonly IMapper _mapper;

        public LeagueController(ILeagueService leagueService, IMapper mapper)
        {
            _leagueService = leagueService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateLeagueAsync([FromBody]CreateLeagueRest newLeague)
        {
            LeagueParameters parameters = new LeagueParameters();
            parameters.Name = newLeague.Name;
            ILeague league = await _leagueService.GetLeagueByQueryAsync(parameters);

            if (league != null)
            {
                ModelState.AddModelError("League Name", "League with this name already exists!");
                return BadRequest(ModelState);
            }

            league = _mapper.Map(newLeague, league);

            bool result = await _leagueService.CreateLeagueAsync(league);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteLeagueAsync(Guid Id)
        {
            ILeague league = await _leagueService.GetLeagueByIdAsync(Id);
            if (league == null)
            {
                return NotFound();
            }

            bool result = await _leagueService.DeleteLeagueAsync(Id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        [Route("{id}")]
        [AcceptVerbs("GET")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLeagueByIdAsync(Guid Id)
        {
            ILeague league = await _leagueService.GetLeagueByIdAsync(Id);
            if (league == null)
            {
                return NotFound();
            }

            LeagueViewModel viewModel = _mapper.Map<LeagueViewModel>(league);

            return Ok(viewModel);
        }
        [HttpPut]
        public async Task<IHttpActionResult> UpdateLeagueAsync([FromBody]UpdateLeagueRest updateLeague)
        {
            ILeague league = await _leagueService.GetLeagueByIdAsync(updateLeague.Id);
            if (league == null)
            {
                return NotFound();
            }

            league = _mapper.Map(updateLeague, league);

            bool result = await _leagueService.UpdateLeagueAsync(league);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}