using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Results.Service.Common;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.TeamSeason;
using Results.Common.Utils.QueryParameters;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.WebAPI.Models.ViewModels;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/teamseason")]
    public class TeamSeasonController : ApiController
    {
        private readonly ITeamSeasonService _teamSeasonService;
        private readonly IMapper _mapper;

        public TeamSeasonController(ITeamSeasonService teamSeasonService, IMapper mapper)
        {
            _teamSeasonService = teamSeasonService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateTeamSeasonAsync([FromBody] CreateTeamSeason teamSeasonCreate) {
            ITeamSeason teamSeason = _mapper.Map<ITeamSeason>(teamSeasonCreate);
            Guid id = await _teamSeasonService.CreateTeamSeasonAsync(teamSeason);
            return Ok(id);
        }

        [Route("registerteam/{teamSeasonId}")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterTeamAsync(Guid teamSeasonId, [FromBody] List<TeamRegistrationRest> registrationList) {
            List<ITeamRegistration> toRegister = new List<ITeamRegistration>();
            foreach (var player in registrationList) {
                if (player.IsValid())
                {
                    ITeamRegistration playerReg = _mapper.Map<ITeamRegistration>(player);
                    playerReg.TeamSeasonId = teamSeasonId;
                    toRegister.Add(playerReg);
                }
                else {
                    return BadRequest("Invalid player inserted");
                }
            }
            if (await _teamSeasonService.RegisterTeamAsync(toRegister))
                return Ok("Success");
            return BadRequest();
        }

        [Route("{clubId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByQueryAsync(Guid clubID) { 
            List<ITeamSeason> teamSeasons = await _teamSeasonService.GetTeamSeasonByQueryAsync(clubID);
            
            if (teamSeasons.Count == 0) {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ITeamSeason>, IEnumerable<TeamSeasonView>>(teamSeasons));

        }
        [Route("team/{teamSeasonID}")]
        [HttpGet]

        public async Task<IHttpActionResult> GetTeamByIdAsync(Guid teamSeasonID) {

            List<ITeamRegistration> teamRegistrations = await _teamSeasonService.GetTeamByIdAsync(teamSeasonID);

            if (teamRegistrations.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ITeamRegistration>, IEnumerable<TeamRegistrationView>>(teamRegistrations));

        }
        [Route("team/{teamSeasonID}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTeamAsync(Guid teamSeasonID, [FromBody] UpdateTeamSeason updateTeamSeason) {
            List<ITeamRegistration> toRegister = new List<ITeamRegistration>();
            foreach (var player in updateTeamSeason.ToRegister)
            {
                if (player.IsValid())
                {
                    ITeamRegistration playerReg = _mapper.Map<ITeamRegistration>(player);
                    playerReg.TeamSeasonId = teamSeasonID;
                    toRegister.Add(playerReg);
                }
                else
                {
                    return BadRequest("Invalid player inserted");
                }
            }
            if (await _teamSeasonService.UpdateTeamAsync(updateTeamSeason.ToDelete, toRegister))
                return Ok("Success");
            return BadRequest();
        }

        [Route("{teamSeasonID}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTeamSeasonAsync(Guid teamSeasonID)
        { 
            bool result = await _teamSeasonService.DeleteTeamAsync(teamSeasonID);

            if (!result)
            {
                return BadRequest();
            }

            return Ok("Success");
        }

    }
}
