﻿using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.AdminCQ.Data
{
    class TableUserAction : IMappingAction<User, TableUserDTO>
    {
        DataContext db;
        UserManager<User> userManager;

        public TableUserAction(DataContext context, UserManager<User> userManager)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager;
        }

        public async void Process(User source, TableUserDTO destination, ResolutionContext context)
        {
            if(source.UserReports != null) {
                destination.TotalReports = source.UserReports.Count;
                destination.AdmittedReports = source.UserReports.Where(x => x.Status == ReportStatus.Admitted).Count();
            }
        }

    }
}
