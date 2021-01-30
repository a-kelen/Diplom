using Application.ComponentCQ.Commands;
using Application.DTO;
using Application.UserCQ.Queries;
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
    public class ComponentController : BaseConttoller
    {
        [HttpGet("ownComponentList")]
        public async Task<ActionResult<List<ComponentDTO>>> GetComponents()
        {
            return await Mediator.Send(new OwnList.Query());
        }

        [HttpPost]
        public async Task<ActionResult<ComponentDTO>> Create([FromForm]Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("switch-status")]
        public async Task<ActionResult<bool>> SwitchStatus(ChangeStatus.Command command)
        {
            return await Mediator.Send(command);
        }

       
        [HttpPost("like")]
        public async Task<ActionResult<bool>> LikeLibrary(ComponentLike.Command command)
        {
            return await Mediator.Send(command);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<ComponentDTO>> UpdateComponent(Edit.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> SoftDelete(SoftDelete.Command command)
        {   
            return await Mediator.Send(command);
        }
    }
}
