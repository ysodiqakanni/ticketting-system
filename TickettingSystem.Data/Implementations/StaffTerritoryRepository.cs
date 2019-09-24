using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class StaffTerritoryRepository : BaseRepository<StaffTerritory>, IStaffTerritoryRepository
    {
        public StaffTerritoryRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}
