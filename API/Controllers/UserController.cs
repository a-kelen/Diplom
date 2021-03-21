﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.UserCQ.Commands;
using Application.UserCQ.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseConttoller
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> CurrentUser()
        {
            return await Mediator.Send(new Current.Query());
        }

        [HttpGet("{username}")]
        [Authorize]
        public async Task<ActionResult<DetailedUserDTO>> GetUser(string username)
        {
            return await Mediator.Send(new Get.Query { Username = username});
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(Login.Command query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("follow")]
        public async Task<ActionResult<bool>> SwitchFollow(SwitchFollowUser.Command command)
        {
            return await Mediator.Send(command);
        }




    }
}
