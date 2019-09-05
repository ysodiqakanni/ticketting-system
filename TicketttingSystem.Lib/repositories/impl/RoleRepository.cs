using System;
using Microsoft.Extensions.Logging;
using TicketttingSystem.Lib.config;
using TicketttingSystem.Lib.entities;

namespace TicketttingSystem.Lib.repositories.impl
{
    public class RoleRepository : BaseRepositoryImpl<Role>
    {
        public RoleRepository(DBManager dBManager, ILogger<RoleRepository> logger) : base(dBManager, logger)
        {
            
        }
    }
}
