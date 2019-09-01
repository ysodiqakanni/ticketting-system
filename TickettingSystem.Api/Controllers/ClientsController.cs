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
    /// Endpoints to manage Staff
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        public async Task<IActionResult> GetAllStaffs()
        {

            return Ok();
        }
        public async Task<IActionResult> GetStaffById()
        {

            return Ok();
        }
        public async Task<IActionResult> CreateStaff()
        { 

            return Ok();
        }
        public async Task<IActionResult> AssignRightsToStaff()
        {

            return Ok();
        }
        public async Task<IActionResult> ResetStaffPassword()
        {

            return Ok();
        }
        public async Task<IActionResult> BlockAccess()
        {

            return Ok();
        }
    }
}