using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Services.Contracts;
using static TickettingSystem.Core.Exchange;

namespace TickettingSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTradeById(int id)
        {
            var trade = await _tradeService.GetById(id);
            return Ok(trade);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrades()
        {
            var trades = await _tradeService.GetAllTrades();
            return Ok(trades);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTrades([FromQuery(Name = "searchStr")] string searchStr)
        {
            var searchResult = await _tradeService.SearchTrades(searchStr);
            return Ok(searchResult);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> ComprehensiveSearch([FromBody] int? id, DateTime? startDate, DateTime? endDate,
            string exchange = "", string currencyCode = "")
        {
            if (startDate > endDate)
            {
                throw new Exception("start date cannot be greater than end date");
            }

            
            var search = await _tradeService
                .GetSearchedTradeLines(id, startDate, endDate, exchange, currencyCode);
            return Ok(search);
        }
    }
}