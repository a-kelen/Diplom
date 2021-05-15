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
using Application.Notifications;
using Domain.Entities;

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

        [HttpGet("search/{searchQuery}")]
        public async Task<ActionResult<List<LibraryDTO>>> SearchLibrary(string searchQuery)
        {
            return await Mediator.Send(new SearchLibraries.Query { SearchQuery = searchQuery });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedLibraryDTO>> GetLibrary(Guid id)
        {
            return await Mediator.Send(new GetById.Query { Id = id });
        }

        [HttpGet("{author}/{name}")]
        public async Task<ActionResult<DetailedLibraryDTO>> GetLibraryByAuthorAndName(string author, string name)
        {
            return await Mediator.Send(new GetByAuthorAndName.Query { Author = author, Name = name});
        }

        [HttpGet("liked")]
        public async Task<ActionResult<List<LibraryDTO>>> GetLikedLibraries()
        {
            return await Mediator.Send(new LikedList.Query());
        }

        [HttpGet("topList")]
        public async Task<ActionResult<List<LibraryDTO>>> Top()
        {
            return await Mediator.Send(new TopList.Query());
        }

        [HttpGet("avatar/{id}")]
        public async Task<FileContentResult> GetLibraryAvatar(Guid id)
        {

            var result = await Mediator.Send(new GetLibraryAvatar.Query { LibraryId = id });
            return File(result, "image/jpeg");
        }

        [HttpGet("download/{id}")]

        public async Task<ActionResult<List<ComponentDTO>>> DownLoad(Guid id)
        {

            var res = await Mediator.Send(new Download.Query { Id = id });
            await Mediator.Publish(new HistoryNotification { Type = HistoryType.Library, ElementId = id, Action = HistoryAction.Installed });
            return res;
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

        [HttpPost("report")]
        public async Task<ActionResult<bool>> Report(ReportToLibrary.Command command)
        {
            return await Mediator.Send(command);
        }


        // PUT
        [HttpPut]
        public async Task<ActionResult<LibraryDTO>> UpdateLibrary(Edit.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("avatar")]
        public async Task<ActionResult<bool>> UpdateAvatar([FromForm]UpdateLibraryAvatar.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
