using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public class StaffApi
    {
        public static Task<List<StaffListViewModel>> GetAllStaff()
        {
            var staff = new List<StaffListViewModel>
            {
                new StaffListViewModel{ Id = 1, Name="Jhon Doe Wills 1", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 2, Name="Jhon Doe Wills 2", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 3, Name="Jhon Doe Wills 3", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 4, Name="Jhon Doe Wills 4", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 5, Name="Jhon Doe Wills 5", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
            };
            return Task.Run(() => { return staff; });
        }
        public async static Task<List<StaffListViewModel>> SearchByLastName(string lastname)
        {
            var staff = (await GetAllStaff()).Where(s => s.Name.ToLower().Contains(lastname.ToLower())).ToList();

            return staff;
        }
        public static Task<List<StaffListViewModel>> SearchByLastNamePrefix(string prefix)
        {
            var staff = new List<StaffListViewModel>
            {
                new StaffListViewModel{ Id = 1, Name="Jhon Doe Wills 1", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 2, Name="Jhon Doe Wills 2", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr" }
            };
            return Task.Run(() => { return staff; });
        }
        public static Task<List<StaffListViewModel>> SearchByLastNameSuffix(string suffix)
        {
            var staff = new List<StaffListViewModel>
            {
                new StaffListViewModel{ Id = 3, Name="Jhon Doe Wills 3", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 4, Name="Jhon Doe Wills 4", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 5, Name="Jhon Doe Wills 5", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
            };

            return Task.Run(() => { return staff; });
        }
        public static async Task<List<StaffListViewModel>> SearchStaffById(int id)
        {
            var result = new List<StaffListViewModel>();
            var theStaff = (await GetAllStaff()).Where(s => s.Id == id).FirstOrDefault();
            if (theStaff != null)
            {
                result.Add(theStaff);
            }

            return result;
        }
        public static async Task<StaffDTO> GetStaffById(int id)
        {
            // var theStaff = (await GetAllStaff()).Where(s => s.Id == id).FirstOrDefault();

            return new StaffDTO
            {
                DateOfBirth = DateTime.Now.AddYears(-56),
                Department = "Test Department",
                Email = "test@gmail.com",
                FiredOn = DateTime.Now,
                HiredBy = "Jhon Doe Wills 2",
                HiredOn = DateTime.Now,
                Id = 3,
                Manager = "Mr Manager",
                Name = "Mike",
                Nationality = "Cyprus",
                ReferredBy = "test referer",
                ResignedOn = DateTime.Now,
                StreetNumber = "390",
                StreetName1 = "Lakewood Ave",
                StreetName2 = "Off Becksite Plamer road",
                StreetName3 = "West wing",
                Surname = "Doe"
            };
        }
        public static Task<List<StaffNoteViewModel>> GetNotesByStaffId(int id)
        {
            // Note: This list should be ordered by Data desc
            var notes = new List<StaffNoteViewModel>
            {
                new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "},
                new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "},
                new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "},
                new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "}
            };
            return Task.Run(() => { return notes; });
        }

        public static async Task<string> CreateNewNote(int staffId, string note)
        {
            // return the note after successful insertion to db
            return note;
        }

        public static Task CreateNewStaff(StaffDTO staff)
        {
            return Task.Run(() => { });
        }

        public static Task UpdateStaff(int value, StaffDTO staff)
        {
            return Task.Run(() => { });
        }
    }

}
