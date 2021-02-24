using AutoMapper;
using Results.Common.Utils;
using Results.Model.Common;
using Results.Service.Common;
using Results.WebAPI.Models.RestModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Results.WebAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IUserManager _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserManager userManager, IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUserAsync([FromBody] RegisterUserRest newUser)
        {
            if (!newUser.ValidatePassword())
            {
                return BadRequest("Password and ConfirmPassword do not match.");
            }

            IUser user = _mapper.Map<IUser>(newUser);

            if (!await _userManager.RegisterUserAsync(user))
            {
                return BadRequest();
            }
            
            return Ok();
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> LoginUserAsync([FromBody] LoginUserRest userLogin)
        {
            IUser user = await _userService.GetUserByUserNameAsync(userLogin.UserName);

            if (user == null)
            {
                return BadRequest("Invalid username or possward.");
            }

            if (!await _userManager.CheckPassword(user, userLogin.Password))
            {
                return BadRequest("Invalid username or possward.");
            }

            return Ok(await _userManager.GenerateIdTokenAsync(user, userLogin.RememberMe));
        }
    }
}
