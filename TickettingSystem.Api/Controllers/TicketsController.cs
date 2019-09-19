using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Api.DTO;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        IUnitOfWork uow;
        ITicketService ticketSvc;
        IClientService clientSvc;
        public TicketsController(IUnitOfWork _uow, ITicketService ticketService, IClientService _clientSvc)
        {
            uow = _uow;
            ticketSvc = ticketService;
            clientSvc = _clientSvc;
        }
        [HttpGet]
        public async Task<IActionResult> GetTop10()
        {
            var tickets = ticketSvc.GetTop10();
            if (tickets != null && tickets.Any())
            {
                var resp = new List<TicketDTO>();
                foreach (var client in tickets)
                {
                    resp.Add(await MapTicketToDto(client));
                }
                return Ok(resp);
            }
            return Ok(tickets);
        }

        //[HttpGet]
        //[Route("search/{name}")]
        //public async Task<IActionResult> GetParentTicketsByClientName(string name)
        //{
        //    var tickets = ticketSvc.GetParentTicketsByClientName(name);
        //    if (tickets != null && tickets.Any())
        //    {
        //        var resp = new List<TicketDTO>();
        //        foreach (var client in tickets)
        //        {
        //            resp.Add(await MapTicketToDto(client));
        //        }
        //        return Ok(resp);
        //    }
        //    return Ok(tickets);
        //}

        [HttpGet]
        [Route("search/{clientId?}/{name?}")]
        public async Task<IActionResult> GetParentTicketsByClientId(int clientId, string name = null)
        {
            List<SupportTicket> tickets = null;
            if(name == null)
            {
                tickets = ticketSvc.GetParentTicketsByClientId(clientId);
            }
            else
            {
                tickets = ticketSvc.GetParentTicketsByClientName(name);
            }
            if (tickets != null && tickets.Any())
            {
                var resp = new List<TicketDTO>();
                foreach (var client in tickets)
                {
                    resp.Add(await MapTicketToDto(client));
                }
                return Ok(resp);
            }
            return Ok(tickets);
        }


        [HttpGet]
        [Route("wildcardsearch/prefix/{name}")]
        public async Task<IActionResult> GetParentTicketsByClientNamePrefix(string name)
        {
            var tickets = ticketSvc.GetParentTicketsByClientNamePrefix(name);
            if (tickets != null && tickets.Any())
            {
                var resp = new List<TicketDTO>();
                foreach (var client in tickets)
                {
                    resp.Add(await MapTicketToDto(client));
                }
                return Ok(resp);
            }
            return Ok(tickets);
        }

        [HttpGet]
        [Route("wildcardsearch/suffix/{name}")]
        public async Task<IActionResult> GetParentTicketsByClientNameSuffix(string name)
        {
            var tickets = ticketSvc.GetParentTicketsByClientNameSuffix(name);
            if (tickets != null && tickets.Any())
            {
                var resp = new List<TicketDTO>();
                foreach (var client in tickets)
                {
                    resp.Add(await MapTicketToDto(client));
                }
                return Ok(resp);
            }
            return Ok(tickets);
        }

        [HttpGet]
        [Route("search/{id}/wildcardsuffix")]
        public async Task<IActionResult> GetParentTicketsByClientIdSuffix(string id)
        {
            var tickets = ticketSvc.GetParentTicketsByClientIdSuffix(id);
            if (tickets != null && tickets.Any())
            {
                var resp = new List<TicketDTO>();
                foreach (var client in tickets)
                {
                    resp.Add(await MapTicketToDto(client));
                }
                return Ok(resp);
            }
            return Ok(tickets);
        }

        [HttpGet]
        [Route("search/{id}/wildcardprefix")]
        public async Task<IActionResult> GetParentTicketsByClientIdPrefix(string id)
        {
            var tickets = ticketSvc.GetParentTicketsByClientIdPrefix(id);
            if (tickets != null && tickets.Any())
            {
                var resp = new List<TicketDTO>();
                foreach (var client in tickets)
                {
                    resp.Add(await MapTicketToDto(client));
                }
                return Ok(resp);
            }
            return Ok(tickets);
        }

        [HttpGet]
        [Route("conversations/{id}")]
        public async Task<IActionResult> GetTickectConversationForParentTicket(int id)
        {
            var tickets = ticketSvc.GetConversationsForParentTicket(id);
            if (tickets != null && tickets.Any())
            {
                var resp = new List<TicketConversationDTO>();
                foreach (var client in tickets)
                {
                    resp.Add(await MapTicketToConversationDTO(client));
                }
                return Ok(resp);
            }
            return Ok(tickets);
        }

        [HttpGet]
        [Route("close/{id}")]
        public async Task<IActionResult> CloseTicket(int id)
        {
            try
            {
                var ticket = ticketSvc.CloseTicket(id);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest("Error closing ticket!");
            } 
        }

        [HttpGet]
        [Route("update/{id}/{staffId}/{note}")]
        public async Task<IActionResult> UpdateTicket(int id, int staffId, string note)
        {
            // reassign and add a new note as ticket
            try
            {
                var ticket = ticketSvc.Update(id, staffId, note);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest("Error assigning ticket!");
            }
        }

        private async Task<TicketDTO> MapTicketToDto(SupportTicket ticket)
        {
            var result = new TicketDTO
            {
                Id = ticket.Id,
                AssignedToStaffId = ticket.AssignedTo == null ? 0 : ticket.AssignedTo.Value,
                DateEnabled = ticket.DtCreated.Value,
                Description = ticket.Message,
                Price = 0
            };
            var client = await clientSvc.GetClientById(ticket.UserId);
            result.ClientName = client != null ? client.Firstname + " " + client.Surname : "";

            return result;
        }
        private async Task<TicketConversationDTO> MapTicketToConversationDTO(SupportTicket ticket)
        {
            // Todo: remove the line below
            int x = new Random().Next(0, 2);
            var result = new TicketConversationDTO
            { 
                Content = ticket.Message,
                DateCreated = ticket.DtCreated == null ? default(DateTime) : ticket.DtCreated.Value,
                CreatedByClient = x == 1   // MUST be removed and substituted by a real value from a column
            };
            return result;
        }
    }
}