using Application.LabelCQ;
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
    public class LabelController : BaseConttoller
    {
        [HttpGet("libraries")]
        public async Task<ActionResult<List<string>>> GetLabels([FromQuery] GetLabels.Query query)
        {
            return await Mediator.Send(query);
        }
    }
}
