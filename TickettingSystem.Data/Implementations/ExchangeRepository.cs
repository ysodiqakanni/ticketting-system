﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class ExchangeRepository : BaseRepository<Exchangesusers>, IExchangeRepository
    {
        public ExchangeRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}