using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Results.Service.Common;
using Results.Model.Common;
using Results.Model;
using System.Threading.Tasks;
using Results.WebAPI.Models.RestModels.Stadium;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/stadium")]
    public class StadiumController : ApiController
    {
        private readonly IStadiumService _stadiumService;
        private readonly IMapper _mapper;

        public StadiumController(IStadiumService stadiumService, IMapper mapper)
        {
            _stadiumService = stadiumService;
            _mapper = mapper;
        }
        
        [Route("CreateStadium")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateStadiumAsync([FromBody]CreateStadiumRest newStadium)
        {
            IStadium stadium = await _stadiumService.GetStadiumByNameAsync(newStadium.Name);

            if(stadium != null && stadium.IsDeleted == false)
            {
                ModelState.AddModelError("Name", $"Name {stadium.Name} in use.");
                return BadRequest(ModelState);
            }

            stadium = _mapper.Map<IStadium>(newStadium);
            bool result = await _stadiumService.CreateStadiumAsync(stadium);
            if(result)
            {
                return Ok("Stadium created!");
            }
             
            return BadRequest("Something went wrong");
        }

        [Route("UpdateStadium")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateStadiumAsync([FromBody]UpdateStadiumRest editedStadium)
        {
            IStadium stadium = await _stadiumService.GetStadiumByIdAsync(editedStadium.Id);

            if(stadium == null || stadium.IsDeleted == true)
            {
                return BadRequest("Stadium is deleted or does not exist.");
            }

            stadium = _mapper.Map<IStadium>(editedStadium);

            bool result = await _stadiumService.UpdateStadiumAsync(stadium);

            if(result)
            {
                return Ok("Stadium updated!");
            }

            return BadRequest("Something went wrong!");

        }

        [Route("DeleteStadium")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteStadiumAsync([FromBody]DeleteStadiumRest stadiumForDelete)
        {
            IStadium stadium = await _stadiumService.GetStadiumByIdAsync(stadiumForDelete.Id);

            if (stadium == null || stadium.IsDeleted == true)
            {
                return BadRequest("Stadium is deleted or does not exist.");
            }

            stadium = _mapper.Map<IStadium>(stadiumForDelete);

            bool result = await _stadiumService.DeleteStadiumAsync(stadium);

            if (result)
            {
                return Ok("Stadium deleted!");
            }

            return BadRequest("Something went wrong!");
        }


        [Route("GetAllStadiums")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllStadiumsAsync()
        {
            List<IStadium> stadiums = await _stadiumService.GetAllStadiumsAsync();
            
            if(stadiums.Count == 0)
            {
                return NotFound();
            }

            List<GetAllStadiumsRest> stadiumsRest = _mapper.Map<List<IStadium>, List<GetAllStadiumsRest>>(stadiums);

            return Ok(stadiumsRest);
        }
    }
}
