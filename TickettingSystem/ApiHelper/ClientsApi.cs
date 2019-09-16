using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public static class ClientsApi
    {
        public static async Task<List<ClientDTO>> GetAllClients()
        {
            //var clients = new List<ClientDTO>
            //{
            //    new ClientDTO{ID = 1, Name = "John Doe", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Wolex"},
            //    new ClientDTO{ID = 2, Name = "John kay", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primary", ReferredBy = "Tunde"},
            //    new ClientDTO{ID = 3, Name = "Bad ROugue", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Joy"}
            //};
            //return Task.Run(() => { return clients; });



            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1662/api/v1/");

                HttpResponseMessage msg = await client.GetAsync("clients");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ClientDTO>>();
                return responseBody;
            }
        }

        public static async Task<List<ClientListViewModel>> SearchClients(string searchStr)
        {
            //var clients = new List<ClientListViewModel>
            //{
            //    new ClientListViewModel{ID = 1, Name = "John Doe", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Wolex"},
            //    new ClientListViewModel{ID = 3, Name = "Bad ROugue", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Joy"}
            //};
            //return Task.Run(() => { return clients; });

            using (HttpClient client = new HttpClient())
            {

                var builder = new UriBuilder("http://localhost:5000/api/v1/clients/search");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["searchStr"] = searchStr;
                builder.Query = query.ToString();
                string url = builder.ToString();
                HttpResponseMessage msg = await client.GetAsync(url);
                msg.EnsureSuccessStatusCode();
                var responsebody = await msg.Content.ReadAsAsync<List<ClientListViewModel>>();
                return responsebody;
            }
        }
        public static async Task<ClientDTO> GetClientById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/v1/");

                HttpResponseMessage msg = await client.GetAsync($"clients/{id}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<ClientDTO>();
                return responseBody;
            }
            

            //return Task.Run(() =>
            //{
            //    return new ClientDTO
            //    {
            //        ID = id,
            //        Name = "Tester",
            //        Surname = "Mayor",
            //        Address = "Shomolu Ave gbagura",
            //        DateOfBirth = DateTime.Now.AddDays(-8888),
            //        Email = "tester@gmail.com",
            //        JoinedDate = DateTime.Now.AddYears(-2),
            //        KycLevel = "Secondary",
            //        Language = "English",
            //        Nationality = "Brazilian",
            //        ReferredBy = "Refererrrr JJ",
            //        RefUrl = "www.urlforref.com"
            //    };
            //});
        }
        public static async Task ResetPassword(int clientId)
        {
            return;
        }
        public static async Task<ClientDTO> Update(ClientUpdateViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/v1/");
                string jsonStr = JsonConvert.SerializeObject(model);

                HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
               
                HttpResponseMessage msg = await client.PutAsync("clients", httpContent);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<ClientDTO>();
                return responseBody;
            }

        }

        public static async Task<ClientNoteDTO> CreateNewNote(string note)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/v1/");
                string jsonStr = note;

                HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                HttpResponseMessage msg = await client.PostAsync("clients/notes", httpContent);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<ClientNoteDTO>();
                return responseBody;
            }
        }
    }
    
}
