﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;


namespace TickettingSystem.Data.Implementations
{
    public class TradeRepository: BaseRepository<Trade>, ITradeRepository
    {
        private readonly DbContext _ctx;
        public TradeRepository(DbContext ctx): base(ctx)
        {
            _ctx = ctx;
        }



    }
}