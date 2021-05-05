using Application.AdminCQ.Queries;
using Application.AdminCQ.Commands;
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
    [Authorize(Roles = "admin")]
    public class AdminController : BaseConttoller
    {
        [HttpGet("users")]
        public async Task<ActionResult<UsersPageDTO>> GetUsers([FromQuery] AllUsers.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("reported-users")]
        public async Task<ActionResult<ReportedUsersPageDTO>> GetReportedUsers([FromQuery] GetReportedUsers.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("reported-libraries")]
        public async Task<ActionResult<ReportedLibrariesPageDTO>> GetReportedLibraries([FromQuery] GetReportedLibraries.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("reported-components")]
        public async Task<ActionResult<ReportedComponentsPageDTO>> GetReportedComponents([FromQuery] GetReportedComponents.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-reports")]
        public async Task<ActionResult<UserReportsPageDTO>> GetUserReports([FromQuery] GetUserReports.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("library-reports")]
        public async Task<ActionResult<LibraryReportsPageDTO>> GetLibraryReports([FromQuery] GetLibraryReports.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("component-reports")]
        public async Task<ActionResult<ComponentReportsPageDTO>> GetComponentReports([FromQuery] GetComponentReports.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("admit-report")]
        public async Task<ActionResult<bool>> AdmitReport(AdmitReport.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("reject-report")]
        public async Task<ActionResult<bool>> RejectReport(RejectReport.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("block-element")]
        public async Task<ActionResult<bool>> BlockElement(BlockElement.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("set-role")]
        public async Task<ActionResult<bool>> SetRoleForUser(ChangeRole.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
