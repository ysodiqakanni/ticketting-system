using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Api.DTO;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
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
            if(clients != null && clients.Any())
            {
                var resp = new List<ClientResponseDTO>();
                foreach (var client in clients)
                {
                    resp.Add(ClientMapper.MapUserDetailsToDto(client, _clientService));
                }
                return Ok(resp);
            }
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientById(id);
            if(client != null)
            {
                var resp = ClientMapper.MapUserDetailsToDto(client, _clientService);
                return Ok(resp);
            } 
            return Ok(client);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchClient([FromQuery(Name ="searchStr")] string searchStr = "")
        {
            var searchResult = await _clientService.SearchClient(searchStr);
            if (searchResult != null && searchResult.Any())
            {
                var resp = new List<ClientResponseDTO>();
                foreach (var client in searchResult)
                {
                    resp.Add(ClientMapper.MapUserDetailsToDto(client, _clientService));
                }
                return Ok(resp);
            }
            return Ok(searchResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientUpdateDTO dto)
        {
            var client = new UserDetails
            {
                Id = dto.ID,
                Countrycode = dto.Nationality,
                Languageid = int.Parse(dto.Language),
                Dob = dto.Dob,
                Housenumber = dto.HouseNumber,
                Streetname1 = dto.StreetName1,
                Streetname2 = dto.StreetName2,
                Streetname3 = dto.StreetName3
            };
            var result = await _clientService.UpdateClient(client);
            return Ok(result);
        }

        [HttpPost("notes")]
        public async Task<IActionResult> CreateNote([FromBody] IDictionary<string, string> noteModel)
        {
            var addNote = await _clientNoteService.CreateNote(noteModel["Note"], noteModel["Userid"], noteModel["Createdby"], noteModel["Modifiedby"]);
            return Ok(addNote);
        }

        [HttpGet("notes")]
        public async Task<IActionResult> GetNotes()
        {
            var notes = await _clientNoteService.GetNotes();
            return Ok(notes);
        }

        [HttpGet("notes/{id}")]
        public async Task<IActionResult> GetNotesByClientId(int id)
        {
            var notes = await _clientNoteService.GetNotesByClientId(id); 
            return Ok(notes);
        }

        [HttpGet("languages")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var languages =  _clientService.GetAllLanguages();
            return Ok(languages);
        }
    }
}