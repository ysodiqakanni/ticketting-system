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
        public List<int> GetIdsOfClientsWithNamesContain(string name)
        {
            var result = new List<int>();
            name = name.ToLower();
            var clients = uow.ClientRepository.QueryAll().Where(c => c.Firstname.ToLower().Contains(name) || c.Surname.ToLower().Contains(name));
            if (clients != null && clients.Any())
            {
                result.AddRange(clients.Select(c => c.Id));
            }
            return result;
        }
        public List<int> GetIdsOfClientsWithNamesStart(string name)
        {
            var result = new List<int>();
            name = name.ToLower();
            var clients = uow.ClientRepository.QueryAll().Where(c => c.Firstname.ToLower().StartsWith(name) || c.Surname.ToLower().StartsWith(name));
            if(clients != null && clients.Any())
            {
                result.AddRange(clients.Select(c => c.Id));
            }
            return result;
        }
        public List<int> GetIdsOfClientsWithNamesEnd(string name)
        {
            var result = new List<int>();
            name = name.ToLower();
            var clients = uow.ClientRepository.QueryAll().Where(c => c.Firstname.ToLower().EndsWith(name) || c.Surname.ToLower().EndsWith(name));
            if (clients != null && clients.Any())
            {
                result.AddRange(clients.Select(c => c.Id));
            }
            return result;
        }

        public string GetLanguageById(int id)
        {
            return uow.LanguageRepository.Get(id).Language;
        }
        public string GetKycLevel(int userId)
        {

            // KycLevel = "ss", // the user_verification table tells us if a user has been verified - verification count 
            // is the kyc level (which will be missing, 1 or 2) so no kyc, basic, Advanced)
            var userVerification = uow.UserVerificationRepository.Find(u => u.Userid == userId).FirstOrDefault();
            int verCount = 0;
            if (userVerification != null)
                verCount = userVerification.VerificationCount;
            
            switch (verCount)
            {
                case 0:
                    return "Missing";
                case 1: return "Basic";
                case 2: return "Advanced";
                default:
                    return "Invalid data!";
            }
            
        }
        private string GetRefUrl(int userId)
        {
            return "Not impl";
        }
       

    }
}
