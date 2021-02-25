using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.Common.Utils.QueryParameters;
using Results.WebAPI.Models.RestModels.Standing;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/Standings")]
    public class StandingsController : ApiController
    {
        private readonly IStandingsService _standingsService;
        private readonly IMapper _mapper;

        public StandingsController(IStandingsService standingService, IMapper mapper)
        {
            _standingsService = standingService;
            _mapper = mapper;
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStandingsByLeagueSeasonAsync([FromUri] Guid id)
        {
            List<IStandings> standings = await _standingsService.GetStandingsByLeagueSeasonAsync(id);

            if (standings == null)
            {
                return NotFound();
            }

            List<StandingsViewModel> view = _mapper.Map<List<IStandings>, List<StandingsViewModel>>(standings);
            return Ok(view);
        }

        [Route("Get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStandingsByQueryAsync([FromBody] StandingsIdProviderRest provider)
        {
            IStandings standings = await _standingsService.GetStandingsByQueryAsync(_mapper.Map<StandingsParameters>(provider));

            if (standings == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StandingsViewModel>(standings));
        }

        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateStandingsForClubAsync([FromBody] StandingsIdProviderRest provider)
        {
            IStandings standings = _mapper.Map<IStandings>(provider);

            bool result = await _standingsService.CheckStandingsForClubAsync(standings);
            if (result)
            {
                return BadRequest("Standings for that club already exist for this League-Season");
            }

            bool success = await _standingsService.CreateStandingsForClubAsync(standings);
            if (success)
            {
                return Ok("Standings for club successfully created");
            }

            return BadRequest("Something went wrong");
        }

        [Route("Put")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateStandingsForClubAsync([FromBody] UpdateStandingsRest updateStandingRest)
        {
            IStandings standings = _mapper.Map<IStandings>(updateStandingRest);

            bool result = await _standingsService.UpdateStandingsForClubAsync(standings);
            if (result)
            {
                return Ok("Standings for Club successfully updated");
            }

            return BadRequest("Something went wrong");
        }

        [Route("Delete/LeagueSeason")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteLeagueSeasonStandingsAsync([FromBody] StandingsIdProviderRest provider)
        {
            IStandings standings = _mapper.Map<IStandings>(provider);

            bool result = await _standingsService.DeleteLeagueSeasonStandingsAsync(standings);
            if (result)
            {
                return Ok("Standings for League-Season successfully deleted");
            }

            return BadRequest("Something went wrong");
        }

        [Route("Delete/ClubByLeagueSeason")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteClubStandingsAsync([FromBody] StandingsIdProviderRest provider)
        {
            IStandings standings = _mapper.Map<IStandings>(provider);

            bool result = await _standingsService.DeleteClubStandingsAsync(standings);
            if (result)
            {
                return Ok("Standings for club successfully deleted");
            }

            return BadRequest("Something went wrong");
        }
    }
}
