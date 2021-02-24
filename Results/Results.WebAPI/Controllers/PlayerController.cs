using AutoMapper;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.Person;
using Results.WebAPI.Models.ViewModels.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/player")]
    public class PlayerController : ApiController
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        [Route("{id}", Name = nameof(GetPlayerByIdAsync))]
        [HttpGet]
        public async Task<IHttpActionResult> GetPlayerByIdAsync(Guid id)
        {
            IPlayer player = await _playerService.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlayerViewModel>(player));
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindPlayersAsync([FromUri] PlayerParameters parameters)
        {
            if (!parameters.IsValid())
            {
                return BadRequest();
            }

            PagedList<IPlayer> playerList = await _playerService.FindPlayersAsync(parameters);

            if (playerList == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PagedList<IPlayer>, IEnumerable<PlayerViewModel>>(playerList));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreatePlayerAsync([FromBody] PlayerRest playerRest)
        {
            IPlayer player = _mapper.Map<IPlayer>(playerRest);

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            player.ByUser = Guid.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            player = await _playerService.CreatePlayerAsync(player);

            if (player == null)
            {
                return BadRequest();
            }
            
            return CreatedAtRoute(nameof(GetPlayerByIdAsync), new { player.Id }, _mapper.Map<PlayerViewModel>(player));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePlayerAsync(Guid id, [FromBody] PlayerRest playerRest)
        {
            IPlayer player = await _playerService.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            player = _mapper.Map(playerRest, player);

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            player.ByUser = Guid.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (!(await _playerService.UpdatePlayerAsync(player)))
            {
                return BadRequest();
            };

            return Ok(_mapper.Map<PlayerViewModel>(player));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePlayerAsync(Guid id)
        {
            IPlayer player = await _playerService.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            player.ByUser = Guid.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (!(await _playerService.DeletePlayerAsync(id, player.ByUser)))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
