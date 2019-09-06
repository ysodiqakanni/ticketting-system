using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace TickettingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base ("DefaultConnection")
        {

        }   
    }
}
