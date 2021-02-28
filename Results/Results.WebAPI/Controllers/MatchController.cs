using AutoMapper;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.Match;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/Match")]
    public class MatchController : ApiController
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;

        public MatchController(IMatchService matchService, IMapper mapper)
        {
            _matchService = matchService;
            _mapper = mapper;
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMatchByIdAsync(Guid id)
        {
            IMatch match = await _matchService.GetMatchByIdAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MatchViewModel>(match));
        }
        [Route("Post/{id}")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateMatchAsync([FromBody] MatchIdProviderRest provider)
        {
            IMatch match = _mapper.Map<IMatch>(provider);

            bool result = await _matchService.CheckMatchExistingAsync(_mapper.Map<MatchParameters>(provider));
            if (result)
            {
                return BadRequest("That match already exist");
            }

            Guid id = await _matchService.CreateMatchAsync(match);
            if (id != Guid.Empty) //provjerit ovo sa empty
            {
                return Ok("Standings for club successfully created");
            }

            return BadRequest("Something went wrong");
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMatchAsync(Guid id)
        {
            IMatch match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            bool result = await _matchService.DeleteMatchAsync(id, match.ByUser); //odakle dobije usera
            if (result)
            {
                return Ok("Match successfully deleted");
            }
            return BadRequest("Something went wrong");
            
        }

        [Route("Put")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateMatchAsync([FromBody] UpdateMatchRest matchRest) 
        {
            IMatch match = await _matchService.GetMatchByIdAsync(matchRest.Id);
            if (match == null)
            {
                return NotFound();
            }

            bool result = await _matchService.UpdateMatchAsync(_mapper.Map<IMatch>(matchRest));
            if (result)
            {
                return Ok("Match successfully updated");
            }

            return BadRequest("Something went wrong");
        }
    }
}
