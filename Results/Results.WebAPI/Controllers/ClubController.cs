using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Results.Service.Common;
using AutoMapper;
using Results.WebAPI.Models.RestModels.Club;
using Results.Model.Common;
using System.Threading.Tasks;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/club")]
    public class ClubController : ApiController
    {
        private readonly IClubService _clubService;
        private readonly IMapper _mapper;

        public ClubController(IClubService clubService, IMapper mapper)
        {
            _clubService = clubService;
            _mapper = mapper;
        }

        [Route("CreateClub")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateClubAsync([FromBody] CreateClubRest newClub)
        {
            ClubParameters parameters = new ClubParameters();
            parameters.Name = newClub.Name;

            PagedList<IClub> clubList = await _clubService.GetClubsByQueryAsync(parameters);

            if (clubList.Count != 0)
            { 
                return BadRequest("Club in use!");
            }

            IClub club = await _clubService.GetClubByIdAsync(newClub.StadiumID);
            if(club != null)
            {
                return BadRequest("Club with that stadium already exists.");
            }

            club = _mapper.Map<IClub>(newClub);
            bool result = await _clubService.CreateClubAsync(club);
            if (result)
            {
                return Ok("Club created!");
            }

            return BadRequest("Something went wrong");
        }

        [Route("UpdateClub")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateClubAsync([FromBody] UpdateClubRest editedClub)
        {
            IClub club = await _clubService.GetClubByIdAsync(editedClub.Id);

            if (club == null || club.IsDeleted == true)
            {
                return BadRequest("Club is deleted or does not exist.");
            }

            club = _mapper.Map<IClub>(editedClub);

            bool result = await _clubService.UpdateClubAsync(club);

            if (result)
            {
                return Ok("Club updated!");
            }

            return BadRequest("Something went wrong!");

        }

        [Route("DeleteClub")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteClubAsync([FromBody] DeleteClubRest clubForDelete)
        {
            IClub club = await _clubService.GetClubByIdAsync(clubForDelete.Id);

            if (club == null || club.IsDeleted == true)
            {
                return BadRequest("Stadium is deleted or does not exist.");
            }

            club = _mapper.Map<IClub>(clubForDelete);

            bool result = await _clubService.DeleteClubAsync(club);

            if (result)
            {
                return Ok("Club deleted!");
            }

            return BadRequest("Something went wrong!");
        }


        [Route("FindClubs")]
        [HttpGet]
        public async Task<IHttpActionResult> FindClubsAsync()
        {
            List<IClub> clubs = await _clubService.GetAllClubsAsync();

            if (clubs.Count == 0)
            {
                return NotFound();
            }

            List<GetAllClubsRest> clubsRest = _mapper.Map<List<IClub>, List<GetAllClubsRest>>(clubs);

            return Ok(clubsRest);
        }
    }
}
