using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TickettingSystem.Lib.entities;
using TicketttingSystem.Lib.entities;

namespace TicketttingSystem.Lib.config
{
    public class DBManager:DbContext
    {
        public DBManager(DbContextOptions option):base(option)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
    }
}
