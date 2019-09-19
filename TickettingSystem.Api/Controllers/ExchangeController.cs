using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Api.DTO;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        
        IUnitOfWork uow;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKnownExchanges()
        {
            var allKnownExchange = await _exchangeService.GetAllKnownExchanges();
            if (allKnownExchange != null && allKnownExchange.Any())
            {
                var resp = new List<ExchangeResponseDTO>();
                foreach (var exchange in allKnownExchange)
                {
                    resp.Add(ExchangeMapper.MapExchangeDetailsToDto(exchange, _exchangeService));
                }
                return Ok(resp);
            }
            return Ok(allKnownExchange);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchExchangesByUserId([FromQuery(Name = "userId")] string userId)
        {
            var searchResult = await _exchangeService.SearchExchangesByUserId(userId);
            if (searchResult != null && searchResult.Any())
            {
                var resp = new List<ExchangeResponseDTO>();
                foreach (var exchange in searchResult)
                {
                    resp.Add(ExchangeMapper.MapExchangeDetailsToDto(exchange, _exchangeService));
                }
                return Ok(resp);
            }
            return Ok(searchResult);
        }



    }
}