using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;

namespace TickettingSystem.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext context;
        public UnitOfWork(AppDbContext _context)
        {
            context = _context;
            ClientRepository = new ClientRepository(_context);
            TradeRepository = new TradeRepository(_context);
            ClientNoteRepository = new ClientNoteRepository(_context);
        }
        public IBaseRepository<Client> ClientRepository { get; set; }
        public IBaseRepository<Trade> TradeRepository { get; set; }
        public IBaseRepository<ClientNote> ClientNoteRepository { get; set; }

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
