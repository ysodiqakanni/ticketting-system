using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;
using TickettingSystem.Utilities;

namespace TickettingSystem.ApiHelper
{
    public class StaffApi
    {
        private readonly AppSettings _appSettings;
       
        private static string baseUrl;
        public StaffApi(AppSettings appSettings)
        {
            _appSettings = appSettings;
            baseUrl = _appSettings.BaseUrl;
        } 

        // static string baseUrl = "https://localhost:44355/api/v1/";
        public async Task<List<StaffListViewModel>> GetAllStaff()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            }
             
        }
        public async static Task<List<StaffListViewModel>> SearchByLastName(string lastname)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/searchlast?searchStr="+lastname);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            } 
        }
        public static async Task<List<StaffListViewModel>> SearchByLastNamePrefix(string prefix)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/searchprefix?searchStr=" + prefix);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            } 
        }
        public static async Task<List<StaffListViewModel>> SearchByLastNameSuffix(string suffix)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/searchsuffix?searchStr=" + suffix);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffListViewModel>>();
                return responseBody;
            }
        }
        public static async Task<List<StaffListViewModel>> SearchStaffById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/" + id);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<StaffListViewModel>();

                var result = new List<StaffListViewModel>();
                if (responseBody != null)
                {
                    result.Add(responseBody);
                }

                return result;
            } 
        }
        public static async Task<StaffDTO> GetStaffById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/" + id);
                msg.EnsureSuccessStatusCode();

                var responseBody = await msg.Content.ReadAsAsync<StaffDTO>();
 
                return responseBody;
            } 
        }
        public static async Task<List<StaffNoteViewModel>> GetNotesByStaffId(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("staff/notes/" + id);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<StaffNoteViewModel>>();

                return responseBody;
            } 
        }

        public static async Task<string> CreateNewNote(int staffId, string note)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync($"staff/{staffId}/createnote/{note}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsStringAsync();

                return responseBody;
            } 
        }

        public static Task CreateNewStaff(StaffDTO staff)
        {
            return Task.Run(() => { });
        }

        public static async Task UpdateStaff(int value, StaffDTO staff)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(staff), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("staff", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public static async Task<LoginViewModel> Authenticate(LoginViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync($"account/authenticate", content); //    var httpContent = new StringContent(noteData, Encoding.UTF8, "application/json");

                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<LoginViewModel>();
                return responseBody;
            }
        }

    }

}