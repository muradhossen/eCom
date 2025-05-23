﻿using Application.DTOs.User;
using Application.Extentions;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities.User;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCom.API.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountsController(ITokenService tokenService
          , UserManager<AuthUser> userManager
          , SignInManager<AuthUser> signInManager
          , IMapper mapper
          , IAccountService accountService)
        {

            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _accountService = accountService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            if (await _accountService.IsUserExist(registerDto.Username)) return BadRequest("User already exist.");

            if (string.IsNullOrEmpty(registerDto.Password) || string.IsNullOrEmpty(registerDto.Username))
                return BadRequest("User name or Password can't be empty");

            var user = _mapper.Map<AuthUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }

            var userDto = _mapper.Map<AuthUserDto>(user);
            userDto.Token = await _tokenService.CreateToken(user);

            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _accountService.GetUserByUsername(loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var userDto = _mapper.Map<AuthUserDto>(user);
            userDto.Token = await _tokenService.CreateToken(user);

            return Ok(userDto);
        }

    }
}
