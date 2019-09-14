using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;

namespace TickettingSystem.Data.Implementations
{
    public class ClientNoteRepository: BaseRepository<ClientNote>, IClientNoteRepository
    {
        public ClientNoteRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}
