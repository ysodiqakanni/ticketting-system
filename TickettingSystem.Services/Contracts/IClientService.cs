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
        Task<IList<UserDetails>> GetAllAsync();
        Task<UserDetails> GetClientById(int id);
        Task<UserDetails> UpdateClient(UserDetails model);
        Task<IList<UserDetails>> SearchClient(string searchStr);


        // should go to the Language Service
        string GetLanguageById(int id);
        string GetKycLevel(int userId);
        List<int> GetIdsOfClientsWithNamesStart(string name);
        List<int> GetIdsOfClientsWithNamesEnd(string name);
        List<int> GetIdsOfClientsWithNamesContain(string name);
        List<Languages> GetAllLanguages();
        List<Territories> GetAllEuropeanCountries();
    }
}
