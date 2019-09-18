using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class LanguageRepository : BaseRepository<Languages>, ILanguageRepository
    {
        db_a3d3ad_pricingContext _ctx;
        public LanguageRepository(DbContext ctx) : base(ctx)
        {
            _ctx = ctx as db_a3d3ad_pricingContext;
        }
    }
}
