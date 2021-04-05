using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LibraryCQ.Commands
{
    public class UpdateLibraryAvatar
    {
        public class Command : IRequest<bool>
        {
            public Guid LibraryId { get; set; }
            public IFormFile Image { get; set; }
        }
        
        public class Handler : IRequestHandler<Command, bool>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            IWebHostEnvironment environment;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper
                           , IWebHostEnvironment environment)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
                this.environment = environment;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {

                string path = Path.Combine(environment.WebRootPath, "Avatars");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var library = await db.Libraries.FirstOrDefaultAsync(x => x.Id == request.LibraryId);
                var userId = userAccessor.GetId();

                if (library == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                if (library.UserId != userId)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Denied" });

                path = Path.Combine(path, library.Id.ToString() + ".jpg");
                library.Avatar = Path.Combine(library.Id.ToString() + ".jpg");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
