using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface iUserAccessor
    {
        public User GetUser();
        public Guid GetId();
    }
}
