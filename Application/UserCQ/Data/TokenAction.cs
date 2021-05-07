using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Application.UserCQ.Data
{
    public class TokenAction : IMappingAction<User, CurrentUserDTO>
    {
        iJWTGenerator jwtGenerator;
        UserManager<User> userManager;

        public TokenAction(iJWTGenerator _jwtGenerator, UserManager<User> userManager)
        {
            this.jwtGenerator = _jwtGenerator;
            this.userManager = userManager;
        }

        public void Process(User source, CurrentUserDTO destination, ResolutionContext context)
        {
            destination.Token = jwtGenerator.CreateToken(source);
        }
    }
}
