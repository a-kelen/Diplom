using Application.DTO;
using Application.LikeCQ.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : BaseConttoller
    {
        [HttpGet("liked")]
        public async Task<ActionResult<List<LikeDTO>>> GetLikedLibraries()
        {
            return await Mediator.Send(new Likes.Query());
        }
    }
}
