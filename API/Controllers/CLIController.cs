using Application.CliCQ.Queries;
using Application.DTO;
using Application.UserCQ.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CLIController : BaseConttoller
    {

        [HttpGet("libraries")]
        public async Task<ActionResult<List<LibraryDTO>>> GetLibraries([FromQuery] GetLibraries.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("components")]
        public async Task<ActionResult<List<ComponentDTO>>> GetComponents([FromQuery] GetComponents.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("dependencies")]
        public async Task<ActionResult<List<string>>> GetDependencies([FromQuery] GetDependencies.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("install")]
        public async Task<ActionResult<FolderDTO>> Install([FromQuery] Install.Query query)
        {
            return await Mediator.Send(query);
        }
    }
}
