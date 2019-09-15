using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public static async Task<List<StaffListViewModel>> GetStaffById(int id)
        {
            var result = new List<StaffListViewModel>(); 
            var theStaff = (await GetAllStaff()).Where(s => s.Id == id).FirstOrDefault();
            if (theStaff != null)
            {
                result.Add(theStaff);
            }
          
            return result;
        }
    }
    
}
