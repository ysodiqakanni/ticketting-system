using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;

namespace TickettingSystem.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private Microsoft.EntityFrameworkCore.DbContext context;

        public UnitOfWork()
        {
            context = new AppDbContext();
        }
        public IBaseRepository<Client> ClientRepository { get; set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        } 
    }
}
