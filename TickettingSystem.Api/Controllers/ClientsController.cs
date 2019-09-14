using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts; 
using TickettingSystem.Services.Contracts; 

namespace TickettingSystem.Api.Controllers
{

    /// <summary>
    /// Endpoints to manage Clients
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IClientNoteService _clientNoteService;
        IUnitOfWork uow;

        public ClientsController(IClientService clientService, IClientNoteService clientNoteService )
        {
            _clientService = clientService;
            _clientNoteService = clientNoteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientById(id);
            return Ok(client);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchClient([FromQuery(Name ="searchStr")] string searchStr)
        {
            var searchResult = await _clientService.SearchClient(searchStr);
            return Ok(searchResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(Client client)
        {
            var clientToUpdate = await _clientService.GetClientById(client.ID);
            clientToUpdate.Language = client.Language;
            clientToUpdate.Nationality = client.Nationality;
            clientToUpdate.DateOfBirth = client.DateOfBirth;
            clientToUpdate.Address = client.Address;
            var clientUpdate = await _clientService.UpdateClient(clientToUpdate);
            return Ok(clientUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(string notes)
        {
            var addNote = await _clientNoteService.CreateNote(notes);
            return Ok(addNote);
        }

        [HttpGet("notes")]
        public async Task<IActionResult> GetNotes()
        {
            var notes = await _clientNoteService.GetNotes();
            return Ok(notes);
        }
    }
}