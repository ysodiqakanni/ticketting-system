using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Services.Contracts
{
    public interface IClientService
    {
        Task<IList<Clientinterest>> GetAllAsync();
        Task<Clientinterest> GetClientById(int id);
        Task<Clientinterest> UpdateClient(Clientinterest model);
        Task<IList<Clientinterest>> SearchClient(string searchStr);
    }
}
