using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
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

        [HttpGet("top-list")]
        public async Task<ActionResult<List<UserDTO>>> Top()
        {
            return await Mediator.Send(new TopList.Query());
        }

        [HttpGet("activities")]
        public async Task<ActionResult<ActivitiesPageDTO>> GetLastActivies([FromQuery] GetActivities.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<HistoryItemDTO>>> GetHistory()
        {
            return await Mediator.Send(new GetHistory.Query());
        }

        [HttpGet("followed-users")]
        public async Task<ActionResult<List<FollowDTO>>> GetFollowedUsers()
        {
            return await Mediator.Send(new GetFollowedUsers.Query());
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<CurrentUserDTO>> Login(Login.Command query)
        {
            var res = await Mediator.Send(query);
            CookieOptions options= new CookieOptions
            {
                Expires = DateTime.Now.AddDays(5),
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true
            };
            HttpContext.Response.Cookies.Append("token", res.Token, options);
            HttpContext.Response.Cookies.Append("refreshToken", res.RefreshToken, options);
            return res;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<CurrentUserDTO>> Register(Register.Command command)
        {
            var res = await Mediator.Send(command);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(5),
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true
            };
            
            HttpContext.Response.Cookies.Append("token", res.Token, options);
            HttpContext.Response.Cookies.Append("refreshToken", res.RefreshToken, options);
            return res;
        }

        [HttpPost("follow")]
        public async Task<ActionResult<bool>> SwitchFollow(SwitchFollowUser.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            CookieOptions options = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true
            };
            HttpContext.Response.Cookies.Delete("token", options);
            HttpContext.Response.Cookies.Delete("refreshToken", options);
            return Ok();
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

        [HttpPost("avatar")]
        public async Task<ActionResult<bool>> UpdateAvatar([FromForm]UpdateAvatar.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> EditProfile(EditProfile.Command command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<CurrentUserDTO>> RefreshToken()
        {
            string refreshToken = HttpContext.Request.Cookies["refreshToken"];
            string accessToken = HttpContext.Request.Cookies["token"];
            var res = await Mediator.Send(new RefreshToken.Command { RefreshToken = refreshToken, AccessToken = accessToken });

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(5),
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true
            };

            HttpContext.Response.Cookies.Append("token", res.Token, options);
            HttpContext.Response.Cookies.Append("refreshToken", res.RefreshToken, options);
            return res;
        }

        [HttpPost("revoke-token")]
        public async Task<ActionResult<bool>> RevokeToken()
        {
            return await Mediator.Send(new RevokeToken.Command());
        }




    }
}
