using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;

namespace TickettingSystem.Services.Contracts
{
    public interface IClientService
    {
        Task<IList<Client>> GetAllAsync();
        Task<Client> GetClientById(int id);
        Task<Client> UpdateClient(Client model);
        Task<IList<Client>> SearchClient(string searchStr);
    }
}
