using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TickettingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base ("DefaultConnection")
        {

        }

        public static implicit operator Microsoft.EntityFrameworkCore.DbContext(AppDbContext v)
        {
            throw new NotImplementedException();
        }
    }
}
