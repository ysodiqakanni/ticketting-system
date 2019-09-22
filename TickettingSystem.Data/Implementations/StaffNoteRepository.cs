using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class StaffNoteRepository : BaseRepository<StaffNotes>, IStaffNoteRepository
    {
        public StaffNoteRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}