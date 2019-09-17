using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public static class ClientsApi
    {
        public static Task<List<ClientDTO>> GetAllClients()
        {
            var clients = new List<ClientDTO>
            {
                new ClientDTO{ID = 1, Name = "John Doe", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Wolex"},
                new ClientDTO{ID = 2, Name = "John kay", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primary", ReferredBy = "Tunde"},
                new ClientDTO{ID = 3, Name = "Bad ROugue", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Joy"}
            };
            return Task.Run(() => { return clients; });
        }
        public static Task<List<ClientListViewModel>> SearchClients(string searchStr)
        {
            var clients = new List<ClientListViewModel>
            {
                new ClientListViewModel{ID = 1, Name = "John Doe", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Wolex"},
                new ClientListViewModel{ID = 3, Name = "Bad ROugue", Email = "jd@gmail.com", JoinedDate = DateTime.Now, KycLevel = "primry", ReferredBy = "Joy"}
            };
            return Task.Run(() => { return clients; });

        }
        public static Task<ClientDTO> GetClientById(int id)
        {
            return Task.Run(() =>
            {
                return new ClientDTO
                {
                    ID = id,
                    Name = "Tester",
                    Surname = "Mayor",
                    Address = "Shomolu Ave gbagura",
                    DateOfBirth = DateTime.Now.AddDays(-8888),
                    Email = "tester@gmail.com",
                    JoinedDate = DateTime.Now.AddYears(-2),
                    KycLevel = "Secondary",
                    Language = "English",
                    Nationality = "AD",
                    ReferredBy = "Refererrrr JJ",
                    RefUrl = "www.urlforref.com"
                };
            });
        }
        public static async Task ResetPassword(int clientId)
        {
            return;
        }
        public static async Task Update(ClientUpdateViewModel model)
        {
            return;
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