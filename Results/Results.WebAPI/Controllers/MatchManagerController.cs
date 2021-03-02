using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.MatchManager;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/MatchManager")]
    public class MatchManagerController : ApiController
    {
        private readonly IMatchManagerService _matchManagerService;
        private readonly IMapper _mapper;

        public MatchManagerController(IMatchManagerService matchManagerService, IMapper mapper)
        {
            _matchManagerService = matchManagerService;
            _mapper = mapper;
        }

        [Route("CreateCard")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCardAsync([FromBody]CreateCardRest newCard)
        {
            ICard card = _mapper.Map<ICard>(newCard);
            bool result = await _matchManagerService.CreateCardAsync(card);
            if (result)
            {
                return Ok("Card created!");
            }

            return BadRequest("Something went wrong");
        }

        [Route("UpdateCard")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCardAsync([FromBody] UpdateCardRest editedCard)
        {
            CardParameters parameters = new CardParameters();
            parameters.Id = editedCard.Id;
            PagedList<ICard> cards = await _matchManagerService.GetCardsByQueryAsync(parameters);

            if (cards.Count == 0)
            {
                return BadRequest("Stadium is deleted or does not exist.");
            }

            ICard card = _mapper.Map<ICard>(editedCard);

            bool result = await _matchManagerService.UpdateCardAsync(card);

            if (result)
            {
                return Ok("Card updated!");
            }

            return BadRequest("Something went wrong!");

        }

        [Route("FindCards")]
        [HttpGet]
        public async Task<IHttpActionResult> FindStadiumsAsync(CardParameters parameters)
        {
            PagedList<ICard> cards = await _matchManagerService.GetCardsByQueryAsync(parameters);

            if (cards == null)
            {
                return NotFound();
            }

            List<GetAllCardsRest> cardsRest = _mapper.Map<PagedList<ICard>, List<GetAllCardsRest>>(cards);

            return Ok(cardsRest);
        }

        [Route("CreateSubstitution")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateSubstitutionAsync([FromBody] CreateSubstitutionRest newSubstitution)
        {
            ISubstitution substitution = _mapper.Map<ISubstitution>(newSubstitution);
            bool result = await _matchManagerService.CreateSubstitutionAsync(substitution);
            if (result)
            {
                return Ok("Substitution created!");
            }

            return BadRequest("Something went wrong");
        }

        [Route("UpdateSubstitution")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateSubstitutionAsync([FromBody] UpdateSubstitutionRest editedSubstitution)
        {
            SubstitutionParameters parameters = new SubstitutionParameters();
            parameters.Id = editedSubstitution.Id;
            PagedList<ISubstitution> substitutions = await _matchManagerService.GetSubstitutionsByQueryAsync(parameters);

            if (substitutions.Count == 0)
            {
                return BadRequest("Stadium is deleted or does not exist.");
            }

            ISubstitution substitution = _mapper.Map<ISubstitution>(editedSubstitution);

            bool result = await _matchManagerService.UpdateSubstitutionAsync(substitution);

            if (result)
            {
                return Ok("Substitution updated!");
            }

            return BadRequest("Something went wrong!");

        }

        [Route("FindSubstitutions")]
        [HttpGet]
        public async Task<IHttpActionResult> FindSubstitutionsAsync(SubstitutionParameters parameters)
        {
            PagedList<ISubstitution> substitutions = await _matchManagerService.GetSubstitutionsByQueryAsync(parameters);

            if (substitutions == null)
            {
                return NotFound();
            }

            List<GetAllSubstitutionsRest> substitutionsRest = _mapper.Map<PagedList<ISubstitution>, List<GetAllSubstitutionsRest>>(substitutions);

            return Ok(substitutionsRest);
        }

        [Route("CreateScore")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateScoreAsync([FromBody] CreateScoreRest newScore)
        {
            IScore score = _mapper.Map<IScore>(newScore);
            bool result = await _matchManagerService.CreateScoreAsync(score);
            if (result)
            {
                return Ok("Score created!");
            }

            return BadRequest("Something went wrong");
        }

        [Route("UpdateScore")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateScoreAsync([FromBody] UpdateScoreRest editedScore)
        {
            ScoreParameters parameters = new ScoreParameters();
            parameters.Id = editedScore.Id;
            PagedList<IScore> scores = await _matchManagerService.GetScoresByQueryAsync(parameters);

            if (scores.Count == 0)
            {
                return BadRequest("Stadium is deleted or does not exist.");
            }

            IScore score = _mapper.Map<IScore>(editedScore);

            bool result = await _matchManagerService.UpdateScoreAsync(score);

            if (result)
            {
                return Ok("Card updated!");
            }

            return BadRequest("Something went wrong!");

        }

        [Route("FindScores")]
        [HttpGet]
        public async Task<IHttpActionResult> FindScoresAsync(ScoreParameters parameters)
        {
            PagedList<IScore> scores = await _matchManagerService.GetScoresByQueryAsync(parameters);

            if (scores == null)
            {
                return NotFound();
            }

            List<GetAllScoresRest> scoresRest = _mapper.Map<PagedList<IScore>, List<GetAllScoresRest>>(scores);

            return Ok(scoresRest);
        }
    }
}
