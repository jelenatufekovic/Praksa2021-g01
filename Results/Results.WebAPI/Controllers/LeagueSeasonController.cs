using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.LeagueSeason;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/LeagueSeason")]
    public class LeagueSeasonController : ApiController
    {
        private readonly ILeagueSeasonService _leagueSeasonService;
        private readonly IMapper _mapper;

        public LeagueSeasonController(ILeagueSeasonService leagueSeasonService, IMapper mapper)
        {
            _leagueSeasonService = leagueSeasonService;
            _mapper = mapper;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLeagueSeasonIdAsync()
        {
            List<ILeagueSeason> leagueSeason = await _leagueSeasonService.GetLeagueSeasonIdAsync();

            if (leagueSeason == null)
            {
                return NotFound();
            }

            List<LeagueSeasonViewModel> view = _mapper.Map<List<ILeagueSeason>, List<LeagueSeasonViewModel>>(leagueSeason); 
            return Ok(view);
        }

        [Route("Get/Id")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLeagueSeasonByBothIdAsync([FromUri] LeagueSeasonIdProviderRest provider)
        {

            ILeagueSeason leagueSeason = await _leagueSeasonService.GetLeagueSeasonByBothIdAsync(_mapper.Map<ILeagueSeason>(provider));

            if (leagueSeason == null)
            {
                return NotFound();
            }

            LeagueSeasonViewModel view = _mapper.Map<LeagueSeasonViewModel>(leagueSeason); 
            return Ok(view);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> LeagueSeasonRegistrationAsync([FromBody] CreateLeagueSeasonRest newLeagueSeason)
        {
            ILeagueSeason leagueSeason = await _leagueSeasonService.GetLeagueSeasonByBothIdAsync(_mapper.Map<ILeagueSeason>(newLeagueSeason));

            if (leagueSeason != null)
            {
                return BadRequest("Season for that league already exists");
            }

            leagueSeason = _mapper.Map<LeagueSeason>(newLeagueSeason);
            Guid leagueSeasonId = await _leagueSeasonService.LeagueSeasonRegistrationAsync(leagueSeason);

            if (leagueSeason != null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(leagueSeasonId);
        }
    }
}
