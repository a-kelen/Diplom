using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AdminCQ.Commands
{
    public class AdmitReport
    {
        public class Command : IRequest<bool>
        {
            public Guid ReportId { get; set; }
            public string ReportType { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.ReportType).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {

                if (request.ReportType == "user")
                {
                    UserReport report = await db.UserReports.FirstOrDefaultAsync(x => x.Id == request.ReportId);
                    report.Status = ReportStatus.Admitted;
                }
                else if (request.ReportType == "library")
                {
                    LibraryReport report = await db.LibraryReports.FirstOrDefaultAsync(x => x.Id == request.ReportId);
                    report.Status = ReportStatus.Admitted;
                }
                else if (request.ReportType == "component")
                {
                    ComponentReport report = await db.ComponentReports.FirstOrDefaultAsync(x => x.Id == request.ReportId);
                    report.Status = ReportStatus.Admitted;
                }
                else
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Type = "Not identified" });

                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}
