using AutoMapper;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.Season;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/season")]
    public class SeasonController : ApiController
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly ISeasonService _seasonService;

        #endregion Fields

        #region Constructors

        public SeasonController(ISeasonService seasonController, IMapper autoMapper)
        {
            _seasonService = seasonController;
            _mapper = autoMapper;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        public async Task<IHttpActionResult> CreateSeasonAsync([FromBody]CreateSeasonRest newSeason)
        {
            SeasonParameters parameters = new SeasonParameters();
            parameters.Name = newSeason.Name;
            ISeason season = await _seasonService.GetSeasonByQueryAsync(parameters);

            if (season != null)
            {
                ModelState.AddModelError("Season Name", "Season with this name already exists!");
                return BadRequest(ModelState);
            }

            season = _mapper.Map(newSeason, season);

            bool result = await _seasonService.CreateSeasonAsync(season);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteSeasonAsync(Guid Id)
        {
            ISeason season = await _seasonService.GetSeasonByIdAsync(Id);
            if (season == null)
            {
                return NotFound();
            }

            bool result = await _seasonService.DeleteSeasonAsync(Id);

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSeasonByIdAsync(Guid Id)
        {
            ISeason season = await _seasonService.GetSeasonByIdAsync(Id);
            if (season == null)
            {
                return BadRequest();
            }

            Models.RestModels.Season.SeasonViewModel viewModel = _mapper.Map<Models.RestModels.Season.SeasonViewModel>(season);

            return Ok(viewModel);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateSeasonAsync(UpdateSeasonRest updateSeason)
        {
            ISeason season = await _seasonService.GetSeasonByIdAsync(updateSeason.Id);
            if (season == null)
            {
                return NotFound();
            }

            season = _mapper.Map(updateSeason, season);

            bool result = await _seasonService.UpdateSeasonAsync(season);

            if (!result)
            {
                BadRequest();
            }
            return Ok();
        }

        #endregion Methods
    }
}