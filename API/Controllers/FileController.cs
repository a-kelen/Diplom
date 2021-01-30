using Application.FileCQ.Commands;
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
    public class FileController : BaseConttoller
    {
        [HttpPost("upload-files")]
        public async Task<ActionResult<bool>> UploadFiles([FromForm]UploadFile.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
