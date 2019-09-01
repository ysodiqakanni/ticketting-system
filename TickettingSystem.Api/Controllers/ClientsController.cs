using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TickettingSystem.Api.Controllers
{
    
    /// <summary>
    /// Endpoints to manage Clients
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        public async Task<IActionResult> Search()
        { 
            return Ok();
        }
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
        public async Task<IActionResult> ResetVerificationStatus()
        {
            return Ok();
        }
        public async Task<IActionResult> GetImages()
        {
            return Ok();
        }
        public async Task<IActionResult> BlockAccess()
        {
            return Ok();
        }
        public async Task<IActionResult> GetTradingActivities()
        {
            return Ok();
        }
    }
}