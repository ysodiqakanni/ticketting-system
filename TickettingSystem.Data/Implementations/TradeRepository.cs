using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class TradeRepository: BaseRepository<TradeLog>, ITradeRepository
    {
        private readonly DbContext _ctx;
        public TradeRepository(DbContext ctx): base(ctx)
        {
            _ctx = ctx;
        }



    }
}
