using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CliCQ.Queries
{
    public class Install
    {
        public class Query : IRequest<FolderDTO>
        {
            public string Type { get; set; }
            public string Author { get; set; }
            public string Name { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Author).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Query, FolderDTO>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            IWebHostEnvironment environment;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper
                           , IWebHostEnvironment environment)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
                this.environment = environment;
            }

            public async Task<FolderDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = new FolderDTO();
                res.Files = new List<string>();
                if(request.Type == "component")
                {
                    Component component = await db.Components
                        .Include(x => x.Owner)
                        .FirstOrDefaultAsync(x => x.Owner.UserName == request.Author && x.Name == request.Name);
                    if (component == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Component = "Not found" });
                    res.Name = component.Name;
                    res.Id = component.Id;
                    string startPath = Path.Combine(environment.WebRootPath, "Files", component.Id.ToString());
                    var files = Directory.GetFiles(startPath).ToList();
                    string[] seps = new string[] { component.Id.ToString() + "\\" };
                    foreach (var f in files)
                        res.Files.Add(f.Split(seps, StringSplitOptions.RemoveEmptyEntries)[1]);
                } 
                else if(request.Type == "library")
                {
                    Library library = await db.Libraries
                        .Include(x => x.Owner)
                        .FirstOrDefaultAsync(x => x.Owner.UserName == request.Author && x.Name == request.Name);
                    if (library == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                    res.Name = library.Name;
                    res.Id = library.Id;
                    string startPath = Path.Combine(environment.WebRootPath, "Files", library.Id.ToString());
                    var files = Directory.GetFiles(startPath).ToList();
                    string[] seps = new string[] { library.Id.ToString() + "\\" };
                    foreach (var f in files)
                        res.Files.Add(f.Split(seps, StringSplitOptions.RemoveEmptyEntries)[1]);
                }
                return res;
            }
        }
    }
}
