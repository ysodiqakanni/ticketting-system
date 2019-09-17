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
        public static async Task<string> CreateNewNote(string note)
        {
            // return the note after successful insertion to db
            return note;
        }
        public static async Task<List<NoteListViewModel>> GetAllNotes()
        {
            // Return the MOST RECENT 5 notes
            return new List<NoteListViewModel>()
            {
                new NoteListViewModel{Content = "So Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"}
            };
        }
    }

}