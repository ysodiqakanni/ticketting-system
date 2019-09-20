using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Services.Contracts
{
    public interface IStaffService
    {
        Task<IList<StaffDetails>> GetAllStaffs();
        Task<StaffDetails> GetStaffById(int id);
        Task<IList<StaffDetails>> SearchByLastName(string lastname);
        Task<StaffDetails> CreateStaff(StaffDetails staff);
        Task<StaffDetails> UpdateStaff(int value, StaffDetails staff);
        Task<IList<StaffDetails>> SearchByLastNamePrefix(string prefix);
        Task<IList<StaffDetails>> SearchByLastNameSuffix(string suffix);
        string GetDepartmentById(int departmentId);
        string GetManagerById(int departmentId);
        Task<IList<StaffNotes>> GetNotesByStaffId(int id);
        Task<StaffNotes> CreateNewNote(int staffId, string note);

    }
}
