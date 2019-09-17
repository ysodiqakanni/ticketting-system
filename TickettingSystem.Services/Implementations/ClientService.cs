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
            if (searchStr == null) searchStr = string.Empty;
            if(!String.IsNullOrEmpty(searchStr))
                searchStr = searchStr.ToLower();

            var clients = await uow.ClientRepository.FindAllAsync(
               x => x.Id.ToString() == searchStr || x.Firstname.ToLower().Contains(searchStr)
                || x.Surname.ToLower().Contains(searchStr.ToLower())
                || x.Emailaddress.ToLower().Contains(searchStr.ToLower()));
            return clients.ToList();
        }
         
        public async Task<UserDetails> UpdateClient(UserDetails model)
        {
            var toUpdate = await uow.ClientRepository.FindAsync(x => x.Id == model.Id);
            if (toUpdate == null)
                throw new Exception("Record not found!");
            toUpdate.Housenumber = model.Housenumber;
            toUpdate.Streetname1 = model.Streetname1;
            toUpdate.Streetname2 = model.Streetname2;
            toUpdate.Streetname3 = model.Streetname3;
            toUpdate.Country = model.Country;
            toUpdate.Dob = model.Dob;

            uow.Save();
            return toUpdate;
        }

    }
}
