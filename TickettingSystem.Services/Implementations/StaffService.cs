using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Services.Implementations
{
    public class StaffService: IStaffService
    {
        private readonly IUnitOfWork uow;
        public StaffService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<IList<StaffDetails>> GetAllStaffs()
        {
            var allStaff = await uow.StaffRepository.GetAllAsync();
            return allStaff;
        }

        public async Task<StaffDetails> GetStaffById(int staffId)
        {
            var staff = await uow.StaffRepository.GetAsync(staffId);
            return staff;
        }

        public async Task<StaffDetails> CreateStaff(StaffDetails staff)
        {
            var staffD = await uow.StaffRepository.AddAsync(staff);
            return staffD;
        }

        public async Task<IList<StaffDetails>> SearchByLastName(string lastname)
        {
            if (lastname == null) lastname = string.Empty;
            if (!String.IsNullOrEmpty(lastname))
                lastname = lastname.ToLower();

            var staff = await uow.StaffRepository.FindAllAsync(x => x.Surname.ToLower().Contains(lastname));
            return staff.ToList();
        }

        public async Task<IList<StaffDetails>> SearchByLastNamePrefix(string prefix)
        {
            if (prefix == null) prefix = string.Empty;
            if (!String.IsNullOrEmpty(prefix))
                prefix = prefix.ToLower();

            var staff = await uow.StaffRepository.FindAllAsync(x => x.Surname.ToLower().StartsWith(prefix));
            return staff.ToList();
        }

        public async Task<IList<StaffDetails>> SearchByLastNameSuffix(string suffix)
        {
            if (suffix == null) suffix = string.Empty;
            if (!String.IsNullOrEmpty(suffix))
                suffix = suffix.ToLower();

            var staff = await uow.StaffRepository.FindAllAsync(x => x.Surname.ToLower().EndsWith(suffix));
            return staff.ToList();
        }

        public string GetDepartmentById(int departmentId)
        {
            return uow.DepartmentRepository.Get(departmentId)?.DeptName;
        }

        public string GetManagerById(int departmentId)
        {
            return uow.DepartmentRepository.Get(departmentId)?.DeptMgr;
        }

        public async Task<IList<StaffNotes>> GetNotesByStaffId(int id)
        {
            var notes = await uow.StaffNoteRepository.FindAllAsync(x => x.Userid == id.ToString());
            return notes.ToList();
        }

        public async Task<StaffNotes> CreateNewNote(int staffId, string note)
        {
            var newNote = new StaffNotes
            {
                Userid = staffId.ToString(),
                Note = note, 
                DtCreated = DateTime.Now,
                DtModified = DateTime.Now
            };
            return await uow.StaffNoteRepository.AddAsync(newNote);
            
        }

        public async Task<StaffDetails> UpdateStaff(int value, StaffDetails staff)
        {
            var staffUpdate = await uow.StaffRepository.FindAsync(x => x.Staffuserid == staff.Staffuserid);
            if (staffUpdate == null)
                throw new Exception("Record not found!");
            staffUpdate.Housenumber = staff.Housenumber;
            staffUpdate.Streetname1 = staff.Streetname1;
            staffUpdate.Streetname2 = staff.Streetname2;
            staffUpdate.Streetname3 = staff.Streetname3;
            staffUpdate.Country = staff.Country;
            staffUpdate.Dob = staff.Dob;

            uow.Save();
            return staffUpdate;
        }

        public int GetDepartmentIdFromName(string department)
        {
            var departId = uow.DepartmentRepository
                .Find(x => x.DeptName.ToLower().Equals(department.ToLower())).FirstOrDefault().Id;
            return departId;
        }

        public string GetHiredByIdFromDepartmentName(string department)
        {
            var HiredById = uow.DepartmentRepository
                .Find(x => x.DeptName.ToLower().Equals(department.ToLower())).FirstOrDefault().DeptMgr;
            return HiredById;
        }
    }
}
