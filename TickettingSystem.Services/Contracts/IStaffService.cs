using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Services.Contracts
{
    public interface IStaffService
    {
        StaffDetails Authenticate(string username, string password, out string accessToken);
        IEnumerable<StaffDetails> GetAll();

        Task<IList<StaffDetails>> GetAllStaffs();
        Task<StaffDetails> GetStaffById(int id);
        Task<IList<StaffDetails>> SearchByLastName(string lastname);
        Task<StaffDetails> CreateStaff(StaffDetails staff, List<string> langIds, List<string> territoryIds);
        Task<StaffDetails> UpdateStaff(int value, StaffDetails staff, List<string> langIds, List<string> territoryIds);
        Task<IList<StaffDetails>> SearchByLastNamePrefix(string prefix);
        Task<IList<StaffDetails>> SearchByLastNameSuffix(string suffix);
        string GetDepartmentById(int departmentId);
        int? GetDepartmentIdFromName(string department);
        string GetHiredByIdFromDepartmentName(string department);
        string GetManagerById(int departmentId);
        Task<IList<StaffNotes>> GetNotesByStaffId(int id);
        Task<StaffNotes> CreateNewNote(int staffId, string note);
        StaffNotes UpdateNote(string note, string id, string modifiedBy);

        int GetLanguageIdByName(string language);
        void PostStaffLanguage(List<StaffLanguages> stLang);
        void PostStaffTerritory(List<StaffTerritory> stTerritory);
        int GetTerritoryByName(string territory);
        List<StaffLanguages> GetStaffLanguages(string staffId);
        List<StaffTerritory> GetStaffTerritory(string staffId);
        string GetLanguageById(int id);
        string GetTerritoryById(int territoryId);

        List<string> GetStaffTeritoryIds(string staffId);
        List<string> GetStaffLanguageIds(string staffId);
    }
}
 