using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TickettingSystem.Core;

namespace TickettingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
