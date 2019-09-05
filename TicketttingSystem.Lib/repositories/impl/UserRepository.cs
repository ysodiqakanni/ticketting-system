using System;
using Microsoft.Extensions.Logging;
using TickettingSystem.Lib.entities;
using TicketttingSystem.Lib.config;

namespace TicketttingSystem.Lib.repositories.impl
{
    public class UserRepository : BaseRepositoryImpl<User>
    {
        public UserRepository(DBManager dBManager, ILogger<UserRepository> logger) : base(dBManager, logger)
        {
        }
    }
}
