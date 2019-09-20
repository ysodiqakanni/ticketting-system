using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Api.DTO;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.Controllers
{
    /// <summary>
    /// Endpoints to manage Staff
    /// </summary>
    ///     //[Authorize]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        
        IUnitOfWork uow;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaffs()
        {
            var staff = await _staffService.GetAllStaffs();
            if (staff != null && staff.Any())
            {
                var resp = new List<StaffResponseDTO>();
                foreach (var stf in staff)
                {
                    resp.Add(StaffMapper.MapStaffDetailsToDto(stf, _staffService));
                }
                return Ok(resp);
            }
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffById(id);
            if (staff != null)
            {
                var resp = new List<StaffResponseDTO>();
                resp.Add(StaffMapper.MapStaffDetailsToDto(staff, _staffService));
                return Ok(resp);
            }
            return Ok(staff);
        }



        [HttpGet("notes/{id}")]
        public async Task<IActionResult> GetNotesByStaffId(int id)
        {
            var notes = await _staffService.GetNotesByStaffId(id);
            if (notes != null && notes.Any())
            {
                var resp = new List<StaffNoteResponseDTO>();
                foreach (var nts in notes)
                {
                    resp.Add(StaffNoteMapper.MapStaffDetailsToDto(nts, _staffService));
                }
                return Ok(resp);
            }
            return Ok(notes);
        }

        [HttpPost("{id}/notes")]
        public async Task<IActionResult> CreateNewNote([FromBody] int staffId, string NoteModel)
        {
            var newNotes = await _staffService.CreateNewNote(staffId, NoteModel);
            return Ok(newNotes);
        }

        [HttpPost("createStaff")]
        public async Task<IActionResult> CreateStaff([FromBody] StaffDetails staffModel)
        {
            var staff = await _staffService.CreateStaff(staffModel);
            if (staff != null)
            {
                var resp = new List<StaffResponseDTO>();
                resp.Add(StaffMapper.MapStaffDetailsToDto(staff, _staffService));
                return Ok(resp);
            }
            return Ok(staff);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaff(int? value, [FromBody] StaffDetails staffModel)
        {
            var staff = await _staffService.UpdateStaff(value.Value, staffModel);
            if (staff != null)
            {
                var resp = new List<StaffResponseDTO>();
                resp.Add(StaffMapper.MapStaffDetailsToDto(staff, _staffService));
                return Ok(resp);
            }
            return Ok(staff);
        }

    }
}