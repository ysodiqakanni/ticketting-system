using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;

namespace TickettingSystem.Data.Implementations
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}
