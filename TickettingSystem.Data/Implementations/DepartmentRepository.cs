using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class DepartmentRepository : BaseRepository<Departments>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}