using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class StaffLanguageRepository:BaseRepository<StaffLanguages>, IStaffLanguageRepository
    {
        public StaffLanguageRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}
