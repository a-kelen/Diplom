using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserCQ.Data
{
 
    public class HistoryAction : IMappingAction<HistoryItem, HistoryItemDTO>
    {
        DataContext db;

        public HistoryAction(DataContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async void Process(HistoryItem source, HistoryItemDTO destination, ResolutionContext context)
        {
            if(source.Type == HistoryType.Component)
            {
                Component component = await db.Components.Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == source.ElementId);
                destination.ElementName = $"@{component.Owner.UserName}/{component.Name}";
            } else
            {
                Library library = await db.Libraries.Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == source.ElementId);
                destination.ElementName = $"@{library.Owner.UserName}/{library.Name}";
            }
            
        }
    }
}
