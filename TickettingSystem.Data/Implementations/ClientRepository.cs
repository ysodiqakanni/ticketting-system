using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
//using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class ClientRepository : BaseRepository<UserDetails>, IClientRepository
    {
        public ClientRepository(DbContext ctx) : base(ctx)
        {
        }

    }
}
