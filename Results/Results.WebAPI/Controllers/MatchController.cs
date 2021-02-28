using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.WebAPI.Models.RestModels.Match;
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

        [Route("Get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMatchByQueryAsync(QueryMatchRest matchRest)
        {
            PagedList<IMatch> match = await _matchService.GetMatchByQueryAsync(_mapper.Map<MatchQueryParameters>(matchRest));

            if (match == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<MatchViewModel>>(match));
        }

        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateMatchAsync([FromBody] CreateMatchRest provider)
        {
            IMatch match = _mapper.Map<IMatch>(provider);

            bool result = await _matchService.CheckMatchExistingAsync(_mapper.Map<MatchParameters>(provider));
            if (result)
            {
                return BadRequest("That match already exist");
            }

            Guid id = await _matchService.CreateMatchAsync(match);
            if (id != Guid.Empty) 
            {
                return Ok("Standings for club successfully created with ID = " + id);
            }

            return BadRequest("Something went wrong");
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMatchAsync([FromUri] Guid id, [FromUri] Guid byUser)
        {
            IMatch match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            bool result = await _matchService.DeleteMatchAsync(id, byUser); 
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
