using Application.Common.Result;
using Application.DTOs.User;
using Application.Errors;
using Application.Extentions;
using Application.ServiceInterface;
using AutoMapper;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCom.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IAccountService accountService
            , IMapper mapper
            , IUserService userService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthUserAsync()
        {
            var user = await _accountService.GetUserByUsername(User.GetUserName());

            return Ok(_mapper.Map<AuthUserDto>(user));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] AuthUserUpdateDto userDto)
        {
            var user = _userService
                .Get(c => c.UserName == User.GetUserName())
                .FirstOrDefault();

            if (user is null)
            {
                return BadRequest(Result.Failure(UserError.NotFound));
            }
            _mapper.Map(userDto, user);

            var isUpdate = await _userService.UpdateAsync(user);
            if (isUpdate)
            {
                return Ok(Result.Success(user));
            }
            return BadRequest(Result.Failure(UserError.UpdateFailed(User.GetUserName())));
        }
    }
}
