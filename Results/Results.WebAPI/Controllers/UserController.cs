using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.User;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;


namespace Results.WebAPI.Controllers 
{ 

    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Route("{id}", Name = nameof(GetUserByIdAsync))]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserByIdAsync(Guid id)
        {
            IUser user = await _userService.GetUserByIdAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateUserAsync([FromBody] RegisterUserRest newUser)
        {
            IUser user = await _userService.GetUserByEmailAsync(newUser.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", $"Email { user.Email} in use.");
            }

            user = await _userService.GetUserByUserNameAsync(newUser.UserName);
            if (user != null)
            {
                ModelState.AddModelError("UserName", $"UserName: {user.UserName} in use.");
                return BadRequest(ModelState);
            }

            user = _mapper.Map<IUser>(newUser);

            Guid userId = await _userService.CreateUserAsync(user);
            user = await _userService.GetUserByIdAsync(userId);

            return CreatedAtRoute(nameof(GetUserByIdAsync), new { user.Id }, _mapper.Map<UserViewModel>(user));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateUserAsync(Guid id, [FromBody] UpdateUserRest userForUpdate)
        {
            IUser user = await _userService.GetUserByEmailAsync(userForUpdate.Email);

            if (user != null && user.Email != userForUpdate.Email)
            {
                ModelState.AddModelError("Email", $"Email { user.Email} in use.");
                return BadRequest(ModelState);
            }

            user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user = _mapper.Map(userForUpdate, user);

            bool result = await _userService.UpdateUserAsync(user);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUserAsync(Guid id)
        {
            IUser user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            bool result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}