using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork uow;
        public ClientService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<IList<UserDetails>> GetAllAsync()
        {
            return await uow.ClientRepository.GetAllAsync();
        }

        public async Task<UserDetails> GetClientById(int id)
        {  
            return await uow.ClientRepository.FindAsync(x => x.Id == id);
        }

        public async Task<IList<UserDetails>> SearchClient(string searchStr)
        {
            throw new NotImplementedException();

            //var clients = await uow.ClientRepository.FindAllAsync(
            //   x => x.ID.ToString() == searchStr || x.Name.ToLower().Contains(searchStr.ToLower())
            //    || x.Surname.ToLower().Contains(searchStr.ToLower())
            //    || x.Email.ToLower().Contains(searchStr.ToLower())
            //    || x.DateOfBirth.ToString("d").Contains(searchStr));
            //return clients.ToList();
        }

        public async Task<UserDetails> UpdateClient(UserDetails model)
        {
            return await uow.ClientRepository.UpdateAsync(model, model.Id);
        }

    }
}
