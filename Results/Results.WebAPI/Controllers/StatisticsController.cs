﻿using AutoMapper;
using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/statistics")]
    public class StatisticsController : ApiController
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IStatisticsService _statisticsService;

        #endregion Fields

        #region Constructors

        public StatisticsController(IStatisticsService statisticsService, IMapper autoMapper)
        {
            _statisticsService = statisticsService;
            _mapper = autoMapper;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        public async Task<IHttpActionResult> CreateStatisticsAsync([FromBody]CreateStatisticsRest newStatistics)
        {
            IStatistics statistics = new Statistics();
            statistics = _mapper.Map(newStatistics, statistics);

            bool result = await _statisticsService.CreateStatisticsAsync(statistics);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteStatisticsAsync(Guid MatchId)
        {
            IStatistics statistics = await _statisticsService.GetStatisticsAsync(MatchId);
            if (statistics == null)
            {
                return NotFound();
            }

            bool result = await _statisticsService.DeleteStatisticsAsync(MatchId);

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetAllStatisticsAsync(StatisticsParameters parameters)
        {
            List<IStatistics> statisticsList = await _statisticsService.GetAllStatisticsAsync(parameters);
            if (statisticsList == null)
            {
                return BadRequest();
            }

            List<StatisticsViewModel> viewModels = new List<StatisticsViewModel>();

            foreach(IStatistics statistics in statisticsList)
            {
                viewModels.Add(_mapper.Map<StatisticsViewModel>(statistics));
            }
                       
            return Ok(viewModels);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStatisticsAsync(Guid Id)
        {
            IStatistics statistics = await _statisticsService.GetStatisticsAsync(Id);
            if (statistics == null)
            {
                return BadRequest();
            }

            StatisticsViewModel viewModel = new StatisticsViewModel();

            viewModel = _mapper.Map<StatisticsViewModel>(statistics);

            return Ok(viewModel);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateStatisticsAsync(UpdateStatisticsRest updateStatistics)
        {
            IStatistics statistics = await _statisticsService.GetStatisticsAsync(updateStatistics.MatchId);
            if (statistics == null)
            {
                return NotFound();
            }

            statistics = _mapper.Map(updateStatistics, statistics);

            bool result = await _statisticsService.UpdateStatisticsAsync(statistics);

            if (!result)
            {
                BadRequest();
            }
            return Ok();
        }

        #endregion Methods
    }
}

