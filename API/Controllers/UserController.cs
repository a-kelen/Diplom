using System;
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
    [Authorize]
    public class UserController : BaseConttoller
    {
        [HttpGet]
        public async Task<ActionResult<CurrentUserDTO>> CurrentUser()
        {
            return await Mediator.Send(new Current.Query());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<DetailedUserDTO>> GetUser(string username)
        {
            return await Mediator.Send(new Get.Query { Username = username});
        }

        [HttpGet("search/{searchQuery}")]
       
        public async Task<ActionResult<List<UserDTO>>> SearchUser(string searchQuery)
        {
            return await Mediator.Send(new SearchUsers.Query { SearchQuery = searchQuery });
        }

        [HttpGet("avatar/{username}")]
        public async Task<FileContentResult> GetUserAvatar(string username)
        {
            var result = await Mediator.Send(new GetAvatar.Query { Username = username });
            return File(result, "image/jpeg");
        }

        [HttpGet("topList")]
        public async Task<ActionResult<List<UserDTO>>> Top()
        {
            return await Mediator.Send(new TopList.Query());
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<CurrentUserDTO>> Login(Login.Command query)
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

        [HttpPost("report")]
        public async Task<ActionResult<bool>> Report(ReportToUser.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<bool>> ChangePassword(ChangePassword.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("avatar")]
        public async Task<ActionResult<bool>> UpdateAvatar([FromForm]UpdateAvatar.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> EditProfile(EditProfile.Command command)
        {
            return await Mediator.Send(command);
        }




    }
}
