using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;

namespace TickettingSystem.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;
        public UnitOfWork()
        {
            context = new AppDbContext();
        }
        public IBaseRepository<Client> ClientRepository { get; set; }
        public IBaseRepository<Trade> TradeRepository { get; set; }

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
