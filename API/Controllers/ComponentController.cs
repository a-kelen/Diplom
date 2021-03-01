﻿using Application.ComponentCQ.Commands;
using Application.ComponentCQ.Queries;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedComponentDTO>> GetComponentById(Guid id)
        {
            return await Mediator.Send(new GetById.Query { Id = id });
        }

        //POST
        [HttpPost]
        public async Task<ActionResult<ComponentDTO>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("switch-status")]
        public async Task<ActionResult<bool>> SwitchStatus(ChangeStatus.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("get-own")]
        public async Task<ActionResult<ComponentDTO>> GetOWn(ToOwnComponent.Command command)
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
