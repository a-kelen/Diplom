using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserCQ.Commands;
using Application.UserCQ.Data;
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
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> CurrentUser()
        {
            return await Mediator.Send(new Current.Query());
        }
    }
}
