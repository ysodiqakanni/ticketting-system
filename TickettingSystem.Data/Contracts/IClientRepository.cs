using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Contracts
{
    public interface IClientRepository : IBaseRepository<UserDetails>
    {
    }
}
