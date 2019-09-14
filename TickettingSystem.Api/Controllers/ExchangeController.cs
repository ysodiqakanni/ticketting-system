using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var allKnownExc = await _exchangeService.GetAllKnownExchanges();
            return Ok(allKnownExc);
        }



    }
}