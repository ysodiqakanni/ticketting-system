using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;

namespace TickettingSystem.Data.Implementations
{
    public class ExchangeRepository: BaseRepository<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}
