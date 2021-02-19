using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
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

        [Route("Get/{Guid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTableByLeagueSeasonAsync([FromUri] Guid guid)
        {
            List<IStandings> standings = await _standingsService.GetTableByLeagueSeasonAsync(guid);

            if (standings == null)
            {
                return NotFound();
            }

            List<StandingsViewModel> view = _mapper.Map<List<IStandings>, List<StandingsViewModel>>(standings);
            return Ok(view);
        }

        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateTableByLeagueSeasonAsync([FromBody] CreateDeleteStandingsRest createStandingRest)
        {
            IStandings standings = _mapper.Map<IStandings>(createStandingRest);

            string result = await _standingsService.CheckExistingClubForLeagueSeasonAsync(standings);
            if (result == "Exist")
            {
                return BadRequest("Standings for that club already exist for this League-Season");
            }

            if (result == "Deleted")
            {
                if(await _standingsService.UpdateTableFromDelete(standings)) return Ok("Standings for club successfully created");

                return BadRequest("Something went wrong with reviveing standings for that club");
            }

            if (result == "NoExist")
            {
                bool success = await _standingsService.CreateTableByLeagueSeasonAsync(standings);
                if (success)
                {
                    return Ok("Standings for club successfully created");
                }
            }

            return BadRequest("Something went wrong");
        }

        [Route("Put")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTableForClubAsync([FromBody] UpdateStandingsRest updateStandingRest)
        {
            IStandings standings = _mapper.Map<IStandings>(updateStandingRest);

            bool result = await _standingsService.UpdateTableForClubAsync(standings);
            if (result)
            {
                return Ok("Standings for Club successfully updated");
            }

            return BadRequest("Something went wrong");
        }

        [Route("Delete/LeagueSeason")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTableByLeagueSeasonAsync([FromBody] CreateDeleteStandingsRest deleteStandingRest)
        {
            IStandings standings = _mapper.Map<IStandings>(deleteStandingRest);

            bool result = await _standingsService.DeleteTableByLeagueSeasonAsync(standings);
            if (result)
            {
                return Ok("Standings for League-Season successfully deleted");
            }

            return BadRequest("Something went wrong");
        }

        [Route("Delete/ClubByLeagueSeason")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteClubTableByLeagueSeasonAsync([FromBody] CreateDeleteStandingsRest deleteStandingRest)
        {
            IStandings standings = _mapper.Map<IStandings>(deleteStandingRest);

            bool result = await _standingsService.DeleteClubTableByLeagueSeasonAsync(standings);
            if (result)
            {
                return Ok("Standings for club successfully deleted");
            }

            return BadRequest("Something went wrong");
        }
    }
}
