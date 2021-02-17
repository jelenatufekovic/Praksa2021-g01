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
        public async Task<IHttpActionResult> GetAllLeagueSeasonIdAsync()
        {

            List<ILeagueSeasonModel> leagueSeason = await _leagueSeasonService.GetAllLeagueSeasonIdAsync();
            List<LeagueSeasonViewModel> view = new List<LeagueSeasonViewModel>();

            if (leagueSeason == null)
            {
                return NotFound();
            }
            
            else
            {
                foreach (LeagueSeasonModel result in leagueSeason)
                {
                    view.Add(_mapper.Map<LeagueSeasonViewModel>(result));
                }

                return Ok(view);
            }
        }

        [Route("Get/Id")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLeagueSeasonByBothIdAsync([FromUri] LeagueSeasonIdProviderRest provider)
        {

            ILeagueSeasonModel leagueSeason = await _leagueSeasonService.GetLeagueSeasonByBothIdAsync(_mapper.Map<ILeagueSeasonModel>(provider));

            if (leagueSeason == null)
            {
                return NotFound();
            }

            else
            {
                LeagueSeasonViewModel view = _mapper.Map<LeagueSeasonViewModel>(leagueSeason);

                return Ok(view);
            }
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> LeagueSeasonRegistrationAsync([FromBody] CreateLeagueSeasonRest newLeagueSeason)
        {
            ILeagueSeasonModel leagueSeason = await _leagueSeasonService.GetLeagueSeasonByBothIdAsync(_mapper.Map<ILeagueSeasonModel>(newLeagueSeason));

            if (leagueSeason != null)
            {
                ModelState.AddModelError("League-Season Information", "Season for that league already exists");
            }

            leagueSeason = _mapper.Map<LeagueSeasonModel>(newLeagueSeason);
            Guid leagueSeasonId = await _leagueSeasonService.LeagueSeasonRegistrationAsync(leagueSeason);

            return Ok(leagueSeasonId);
        }
    }
}
