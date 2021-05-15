using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Application.Notifications;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComponentCQ.Queries
{
    public class Download
    {
        public class Query : IRequest<FileStreamResult>
        {
            public Guid Id { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, FileStreamResult>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            IWebHostEnvironment environment;
            IMediator Mediator;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper
                           , IWebHostEnvironment appEnvironment
                           , IMediator mediator)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
                this.environment = appEnvironment;
                this.Mediator = mediator;
            }

            public async Task<FileStreamResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccesor.GetId();
                var component = await db.Components.FirstOrDefaultAsync(x => x.Id == request.Id);
                if(component == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Not found" });
                if (!component.Status && component.UserId != userId)
                    throw new RestException(HttpStatusCode.Forbidden, new { Component = "Denied" });

                string startPath = Path.Combine(environment.WebRootPath, "Files", request.Id.ToString());
                if (Directory.Exists(startPath))
                {

                    var zipName = $"component-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

                    var files = Directory.GetFiles(startPath).ToList();

                    var memoryStream = new MemoryStream();
                    
                        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                        {
                            files.ForEach(file =>
                            {
                                var theFile = archive.CreateEntry(Path.GetFileName(file));
                                using (Stream entryStream = theFile.Open())
                                {
                                    var f = System.IO.File.ReadAllBytes(file);
                                    entryStream.Write(f, 0, f.Length);
                                }
                            });
                        }
                    await Mediator.Publish(new HistoryNotification { ElementId = component.Id , Type = HistoryType.Component, Action = HistoryAction.Installed});
                    memoryStream.Position = 0;
                    return new FileStreamResult(memoryStream, "application/octet-stream")
                    {
                        FileDownloadName = zipName
                    };
                }

                throw new RestException(HttpStatusCode.NotFound, new { File = "Not found" });
            }
        }
    }
}
