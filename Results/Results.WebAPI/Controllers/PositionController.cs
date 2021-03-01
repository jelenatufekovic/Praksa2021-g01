using AutoMapper;
using Results.Service.Common;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Position;
using Results.Common.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Results.Common.Utils;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/position")]
    public class PositionController : ApiController
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;

        public PositionController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPositionByQueryAsync([FromUri] PositionParameters parameters)
        {
            if (!parameters.IsValid())
            {
                return BadRequest();
            }

            PagedList<IPosition> positionList = await _positionService.GetPositionsAsync(parameters);

            if (positionList.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PagedList<IPosition>, IEnumerable<PositionRest>>(positionList));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreatePositionAsync([FromBody] PositionRest positionRest) {

            IPosition position = await _positionService.GetPositionByQueryAsync(new PositionParameters { Name = positionRest.Name });
            if (position != null)
            {
                ModelState.AddModelError("Name", $"Name { position.Name} in use.");
                return BadRequest(ModelState);
            }

            position = await _positionService.GetPositionByQueryAsync(new PositionParameters { ShortName = positionRest.ShortName });
            if (position != null)
            {
                ModelState.AddModelError("ShortName", $"ShortName { position.ShortName} in use.");
                return BadRequest(ModelState);
            }

            position = _mapper.Map<IPosition>(positionRest);
            Guid id = await _positionService.CreatePositionAsync(position);
            return Ok(id);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePositionAsync(Guid id,[FromBody] PositionRest positionRest)
        {
            IPosition position = await _positionService.GetPositionByIdAsync(id);

            if (position != null) {
                IPosition duplicateCheck = await _positionService.GetPositionByQueryAsync(new PositionParameters { Name = positionRest.Name });
                if (duplicateCheck != null && duplicateCheck.Id != position.Id)
                {
                    ModelState.AddModelError("Name", $"Name { duplicateCheck.Name} in use.");
                    return BadRequest(ModelState);
                }

                duplicateCheck = await _positionService.GetPositionByQueryAsync(new PositionParameters { ShortName = positionRest.ShortName });
                if (duplicateCheck != null && duplicateCheck.Id != position.Id)
                {
                    ModelState.AddModelError("ShortName", $"ShortName { duplicateCheck.ShortName} in use.");
                    return BadRequest(ModelState);
                }

                IPosition positionForUpdate = _mapper.Map<PositionRest, IPosition>(positionRest);
                positionForUpdate.Id = id;
                bool result = await _positionService.UpdatePositionAsync(positionForUpdate);

                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }

            return NotFound();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePositionAsync(Guid id)
        {
            IPosition position = await _positionService.GetPositionByIdAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            bool result = await _positionService.DeletePositionAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok("Success");
        }


    }
}
