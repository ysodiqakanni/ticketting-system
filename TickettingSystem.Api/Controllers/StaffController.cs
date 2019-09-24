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

        [HttpGet("searchlast")]
        public async Task<IActionResult> SearchByLastName([FromQuery(Name = "searchStr")] string lastname = "")
        {
            var searchResult = await _staffService.SearchByLastName(lastname);
            if (searchResult != null && searchResult.Any())
            {
                var resp = new List<StaffResponseDTO>();
                foreach (var search in searchResult)
                {
                    resp.Add(StaffMapper.MapStaffDetailsToDto(search, _staffService));
                }
                return Ok(resp);
            }
            return Ok(searchResult);
        }

        [HttpGet("searchprefix")]
        public async Task<IActionResult> SearchByLastNamePrefix([FromQuery(Name = "searchStr")] string prefix = "")
        {
            var searchResult = await _staffService.SearchByLastNamePrefix(prefix);
            if (searchResult != null && searchResult.Any())
            {
                var resp = new List<StaffResponseDTO>();
                foreach (var search in searchResult)
                {
                    resp.Add(StaffMapper.MapStaffDetailsToDto(search, _staffService));
                }
                return Ok(resp);
            }
            return Ok(searchResult);
        }

        [HttpGet("searchsuffix")]
        public async Task<IActionResult> SearchByLastNameSuffix([FromQuery(Name = "searchStr")] string suffix = "")
        {
            var searchResult = await _staffService.SearchByLastNameSuffix(suffix);
            if (searchResult != null && searchResult.Any())
            {
                var resp = new List<StaffResponseDTO>();
                foreach (var search in searchResult)
                {
                    resp.Add(StaffMapper.MapStaffDetailsToDto(search, _staffService));
                }
                return Ok(resp);
            }
            return Ok(searchResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffById(id);
            if (staff != null)
            {
                var resp = StaffMapper.MapStaffDetailsToDto(staff, _staffService);
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

        [HttpGet("{id}/createnote/{note}")]
        public async Task<IActionResult> CreateNewNote(int id, string note)
        {
            var newNotes = await _staffService.CreateNewNote(id, note);
            return Ok(newNotes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] StaffDTO staffModel)
        {
            try
            {
                var staff = StaffMapper.MapDtoToStaffDetails(staffModel, _staffService);
                var staffNew = await _staffService.CreateStaff(staff, staffModel.Languages, staffModel.Teritories);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest("Error saving staff!");
            } 
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaff(int? id, [FromBody] StaffDTO staffModel)
        {

            // first get the staff and ensure it exists

            try
            {
                var theStaff = await _staffService.GetStaffById(staffModel.Id);
                if (theStaff == null)
                    return NotFound();

                theStaff.Dob = staffModel.DateOfBirth;
                theStaff.Firedon = staffModel.FiredOn;
                theStaff.Streetname1 = staffModel.StreetName1;
                theStaff.Streetname2 = staffModel.StreetName2;
                theStaff.Streetname3 = staffModel.StreetName3;
                theStaff.Housenumber = staffModel.StreetNumber;
                theStaff.Firstname = staffModel.Name;
                theStaff.Surname = staffModel.Surname;
                theStaff.DtModified = DateTime.Now;
                theStaff.Countrycode = staffModel.Nationality;
                theStaff.HiredOn = staffModel.HiredOn;
                theStaff.Hiredbyid = staffModel.HiredBy;
                theStaff.Resignedon = staffModel.ResignedOn;
                theStaff.Hiredbyid = staffModel.HiredBy;  

                var staffUpdated = await _staffService.UpdateStaff(0, theStaff, staffModel.Languages, staffModel.Teritories);

                return Ok(staffUpdated);
 
            }
            catch (Exception ex)
            {
                return BadRequest("Error updating staff records");
            }
            
        }

    }
}