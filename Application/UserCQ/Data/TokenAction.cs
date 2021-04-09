﻿using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Application.UserCQ.Data
{
    public class TokenAction : IMappingAction<User, CurrentUserDTO>
    {
        iJWTGenerator jwtGenerator;

        public TokenAction(iJWTGenerator _jwtGenerator)
        {
            this.jwtGenerator = _jwtGenerator;
        }

        public void Process(User source, CurrentUserDTO destination, ResolutionContext context)
        {
            destination.Token = jwtGenerator.CreateToken(source);
        }
    }
}
