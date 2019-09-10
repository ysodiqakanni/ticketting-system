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

        
        //[HttpPost]
        //[Consumes("application/json")]
        //public async Task<IActionResult> Search([FromBody]int? id,  DateTime? startDate, 
        //    [FromBody]DateTime? endDate,
        //    [FromBody]ExchangeEnum? exchange, [FromBody]string currencyCode = "")
        //{
        //    if (startDate > endDate)
        //    {
        //        throw new Exception("start date cannot be greater than end date");
        //    }

        //    if ((startDate != null && endDate != null) || id.ToString() != null
        //        || exchange != null || currencyCode.Length > 0)
        //    {
        //        var search = await _tradeService
        //            .GetSearchedTradeLines(id, startDate, endDate, exchange, currencyCode);
        //        return Ok(search);
        //    }

        //    return Ok();
        //}


    }
}