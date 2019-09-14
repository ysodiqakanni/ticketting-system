using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Core;

namespace TickettingSystem.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Client> ClientRepository { get; set; }
        IBaseRepository<Trade> TradeRepository { get; set; }
        IBaseRepository<ClientNote> ClientNoteRepository { get; set; }
        IBaseRepository<Exchange> ExchangeRepository { get; set; }
        int Save();
    }
}
