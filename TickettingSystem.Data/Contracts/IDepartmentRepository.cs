using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Contracts
{
    public interface IDepartmentRepository : IBaseRepository<Departments>
    {
    }
}