using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public class StaffApi
    {
        static string apiBaseUrl = "https://localhost:5001/api/v1/";
        public static async Task<List<StaffListViewModel>> GetAllStaff()
        {
            var staff = new List<StaffListViewModel>
            {
                new StaffListViewModel{ Id = 1, Name="Jhon Doe Wills 1", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 2, Name="Jhon Doe Wills 2", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 3, Name="Jhon Doe Wills 3", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 4, Name="Jhon Doe Wills 4", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr"},
                new StaffListViewModel{ Id = 5, Name="Jhon Doe Wills 5", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
            };
            //return Task.Run(() => { return staff; });
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            }
        }
        public async static Task<List<StaffListViewModel>> SearchByLastName(string lastname)
        {
            //var staff = (await GetAllStaff()).Where(s => s.Name.ToLower().Contains(lastname.ToLower())).ToList();

            //return staff
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/searchlast?searchStr=" + lastname);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            }
        }

        public async static Task<List<StaffListViewModel>> SearchByLastNamePrefix(string prefix)
        {
            //var staffs = new List<StaffListViewModel>
            //{
            //    new StaffListViewModel{ Id = 1, Name="Jhon Doe Wills 1", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
            //    new StaffListViewModel{ Id = 2, Name="Jhon Doe Wills 2", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr" }
            //};
            //return Task.Run(() => { return staff; });
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/searchprefix?searchStr=" + prefix);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            }
        }
        public static async Task<List<StaffListViewModel>> SearchByLastNameSuffix(string suffix)
        {
            //var staff = new List<StaffListViewModel>
            //{
            //    new StaffListViewModel{ Id = 3, Name="Jhon Doe Wills 3", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
            //    new StaffListViewModel{ Id = 4, Name="Jhon Doe Wills 4", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Secondary", ReferredBy= "Mr Will Smith jr"},
            //    new StaffListViewModel{ Id = 5, Name="Jhon Doe Wills 5", Email = "exampleemail@gmail.com", JoinedOn = DateTime.Now, KycLevel = "Primary", ReferredBy= "Mr Will Smith jr"},
            //};

            //return Task.Run(() => { return staff; });
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/searchsuffix?searchStr="+suffix);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            }
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
            //return new StaffDTO
            //{
            //    DateOfBirth = DateTime.Now.AddYears(-56),
            //    Department = "Test Department",
            //    Email = "test@gmail.com",
            //    FiredOn = DateTime.Now,
            //    HiredBy = "Jhon Doe Wills 2",
            //    HiredOn = DateTime.Now,
            //    Id = 3,
            //    Manager = "Mr Manager",
            //    Name = "Mike",
            //    Nationality = "Cyprus",
            //    ReferredBy = "test referer",
            //    ResignedOn = DateTime.Now,
            //    StreetNumber = "390",
            //    StreetName1 = "Lakewood Ave",
            //    StreetName2 = "Off Becksite Plamer road",
            //    StreetName3 = "West wing",
            //    Surname = "Doe"
            //};

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync($"staff/{id}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<StaffDTO>();
                return responseBody;
            }
        }
        public static async Task<List<StaffNoteViewModel>> GetNotesByStaffId(int id)
        {
            // Note: This list should be ordered by Data desc
            //var notes = new List<StaffNoteViewModel>
            //{
            //    new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "},
            //    new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "},
            //    new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "},
            //    new StaffNoteViewModel{ Id = 1, DateCreated = DateTime.Now, Content = $"This not is for staff with id: {id}. It is an important note that should not be overlooked. Pay attention to it! "}
            //};
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync($"staff/{id}/notes");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffNoteViewModel>>();
                return responseBody;
            }
        }

        public static async Task<StaffNoteViewModel> CreateNewNote(int id, string note)
        {
            // return the note after successful insertion to db
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var httpContent = new StringContent(note, Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("staff/notes", httpContent);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<StaffNoteViewModel>();
                return responseBody;
            }
        }

        public static async Task<StaffDTO> CreateNewStaff(StaffDTO staff)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(staff), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("staff/createStaff", httpContent);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<StaffDTO>();
                return responseBody;
            }
        }

        public static async Task<StaffDTO> UpdateStaff(int value, StaffDTO staff)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(staff), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("staff", httpContent);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<StaffDTO>();
                return responseBody;
            }
        }
    }

}