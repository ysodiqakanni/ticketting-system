using Microsoft.Extensions.Configuration;
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
    public static class ClientsApi
    {
        static string apiBaseUrl = "https://localhost:44355/api/v1/";
        public async static Task<List<ClientDTO>> GetAllClients()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("clients");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ClientDTO>>();
                return responseBody;
            } 
        }

        public async static Task<List<ClientDTO>> SearchClients(string searchStr)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("clients/search?searchStr="+searchStr);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ClientDTO>>();
                return responseBody;
            } 

        }
        public async static Task<ClientDTO> GetClientById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync($"clients/{id}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<ClientDTO>();
                return responseBody;
            }
             
        }
        public static async Task ResetPassword(int clientId)
        {
            return;
        }
        public static async Task Update(ClientUpdateViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("clients", httpContent);
                msg.EnsureSuccessStatusCode(); 
            } 
        }
        public static async Task CreateNewNote(string noteData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                var httpContent = new StringContent(noteData, Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("clients/notes", httpContent);
                msg.EnsureSuccessStatusCode();
            }
             
            return;
        }
        public static async Task<List<NoteListViewModel>> GetAllNotes()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl); 
                HttpResponseMessage msg = await client.GetAsync("clients/notes");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<NoteListViewModel>>();
                return responseBody;
            } 
        }
    }

}