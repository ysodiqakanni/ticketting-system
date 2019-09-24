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
using TickettingSystem.Utilities;

namespace TickettingSystem.ApiHelper
{
    public class ClientsApi
    {
        private readonly AppSettings _appSettings;

        private string baseUrl;
        public ClientsApi(AppSettings appSettings)
        {
            _appSettings = appSettings;
            baseUrl = _appSettings.BaseUrl;
        }
         
        public async Task<List<ClientDTO>> GetAllClients()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("clients");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ClientDTO>>();
                return responseBody;
            }
        }

        public async Task<List<ClientDTO>> SearchClients(string searchStr)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("clients/search?searchStr=" + searchStr);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ClientDTO>>();
                return responseBody;
            }

        }
        public async Task<ClientDTO> GetClientById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync($"clients/{id}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<ClientDTO>();
                return responseBody;
            }

        }
        public async Task ResetPassword(int clientId)
        {
            return;
        }
        public async Task Update(ClientUpdateViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("clients", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task CreateNewNote(string noteData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(noteData, Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("clients/notes", httpContent);
                msg.EnsureSuccessStatusCode();
            }

            return;
        }
        public async Task<List<NoteListViewModel>> GetAllNotes()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("clients/notes");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<NoteListViewModel>>();
                return responseBody;
            }
        }
        public async Task<List<NoteListViewModel>> GetNotesByClientId(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("clients/notes/" + id);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<NoteListViewModel>>();

                return responseBody;
            }
        }

        public async Task<List<LanguageViewModel>> GetAllLanguages()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("clients/languages");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<LanguageViewModel>>();

                return responseBody;
            }
        }

        public async Task<List<TeritoryViewModel>> GetEuropeanCountries()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("clients/teritories");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TeritoryViewModel>>();

                return responseBody;
            }
        }

    }

}