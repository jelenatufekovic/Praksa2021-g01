using AutoMapper;
using Results.Common.Utils;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.Person;
using Results.WebAPI.Models.ViewModels.Person;
using System;
using System.Collections.Generic;
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


        [HttpGet]
        public async Task<IHttpActionResult> GetPlayersByQueryAsync([FromUri] PlayerParameters parameters)
        {
            PagedList<IPlayer> playerList = await _playerService.GetPlayersByQueryAsync(parameters);

            if (playerList == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PagedList<IPlayer>, List<PlayerViewModel>>(playerList));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreatePlayerAsync([FromBody] PlayerRest playerRest)
        {
            IPlayer player = _mapper.Map<IPlayer>(playerRest);

            Guid playerId = await _playerService.CreatePlayerAsync(player);

            player = await _playerService.GetPlayerByIdAsync(playerId);

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

            if (!(await _playerService.DeletePlayerAsync(id, player.ByUser)))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
