using Application.ComponentCQ.Commands;
using Application.DTO;
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
    public class ComponentController : BaseConttoller
    {
        [HttpPost]
        public async Task<ActionResult<ComponentDTO>> Create(Create.Command command)
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
