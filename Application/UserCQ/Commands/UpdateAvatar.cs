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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Commands
{
    public class UpdateAvatar
    {
        public class Command : IRequest<bool>
        {
            public IFormFile Image { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
            }
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

                var user = userAccessor.GetUser();
                path = Path.Combine(path, user.Id.ToString() + request.Image.FileName);
                user.Avatar = Path.Combine(user.Id.ToString() + request.Image.FileName);
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
