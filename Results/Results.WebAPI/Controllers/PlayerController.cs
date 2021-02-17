using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.Player;
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

        [HttpPost]
        public async Task<IHttpActionResult> CreatePlayerAsync([FromBody] PlayerRest playerRest)
        {
            IPlayer player = _mapper.Map<Player>(playerRest);

            Guid playerId = await _playerService.CreatePlayerAsync(player);

            player = await _playerService.GetPlayerByIdAsync(playerId);

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

            if (!(await _playerService.DeletePlayerAsync(id, player.UserId)))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
