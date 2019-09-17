using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;

namespace TickettingSystem.Data.Implementations
{
    public class StaffRepository: BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(DbContext ctx): base(ctx)
        {

        }
    }
}
