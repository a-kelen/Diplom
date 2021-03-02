using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.LibraryCQ.Commands;
using Application.DTO;
using Application.LibraryCQ.Queries;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibraryController : BaseConttoller
    {
        // GET
        [HttpGet("ownLibraryList")]
        public async Task<ActionResult<List<LibraryDTO>>> GetLibraries()
        {
            return await Mediator.Send(new OwnLibraryList.Query());
        }

        [HttpGet("ownedLibraryList")]
        public async Task<ActionResult<List<LibraryDTO>>> GetOwnedLibraries()
        {
            return await Mediator.Send(new OwnedList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedLibraryDTO>> GetLibrary(Guid id)
        {
            return await Mediator.Send(new GetById.Query { Id = id });
        }

        [HttpGet("liked")]
        public async Task<ActionResult<List<LibraryDTO>>> GetLikedLibraries()
        {
            return await Mediator.Send(new LikedList.Query());
        }


        //POST
        [HttpPost]
        public async Task<ActionResult<LibraryDTO>> CreateLibrary(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("like")]
        public async Task<ActionResult<bool>> LikeLibrary(LibraryLike.Command command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("get-own")]
        public async Task<ActionResult<LibraryDTO>> GetOWn(ToOwnLibrary.Command command)
        {
            return await Mediator.Send(command);
        }


        // PUT
        [HttpPut]
        public async Task<ActionResult<LibraryDTO>> UpdateLibrary(Edit.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
