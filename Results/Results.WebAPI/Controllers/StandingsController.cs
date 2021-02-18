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
            List<IStandingsModel> standings = await _standingsService.GetTableByLeagueSeasonAsync(guid);

            if (standings == null)
            {
                return NotFound();
            }

            List<StandingsViewModel> view = _mapper.Map<List<IStandingsModel>, List<StandingsViewModel>>(standings);
            return Ok(view);
        }

        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateTableByLeagueSeasonAsync([FromUri] CreateStandingsRest createStandingRest) //kreirat iz body, takoder vidjet hoce li biti 0 po defaultu
        {
            IStandingsModel standings = _mapper.Map<IStandingsModel>(createStandingRest);

            bool result = await _standingsService.CheckExistingClubForLeagueSeasonAsync(standings);
            if (result)
            {
                return BadRequest("Standings for that club already exist for this League-Season");
            }

            bool success = await _standingsService.CreateTableByLeagueSeasonAsync(standings);
            if (success)
            {
                return Ok("Standing successfully created");
            }

            return BadRequest("Something went wrong");
            
        }
    }
}
