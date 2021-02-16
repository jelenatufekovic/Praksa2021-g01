using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.User;
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
        public async Task<IHttpActionResult> GetLeagueSeasonByIdAsync()
        {

            List<ILeagueSeasonModel> leagueSeason = await _leagueSeasonService.GetLeagueSeasonByIdAsync();
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

                //LeagueSeasonViewModel leagueSeasonViewModel = _mapper.Map<LeagueSeasonViewModel>(leagueSeason);

                return Ok(view);
            }


        }

    }
}
