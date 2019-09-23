using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Api.DTO;
using TickettingSystem.Api.Helper;
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
        private readonly ITicketService _ticketService;

        IUnitOfWork uow;

        public StaffController(IStaffService staffService, ITicketService ticketService)
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

        [HttpPost("createStaff")]
        public async Task<IActionResult> CreateStaff([FromBody] StaffDTO staffModel)
        {
            var staff = StaffMapper.MapDtoToStaffDetails(staffModel, _staffService);
            
            var staffNew = await _staffService.CreateStaff(staff);
            if (staffNew != null)
            {
                if (staffModel.Languages?.Count() > 0 || staffModel.Territory?.Count() > 0)
                {
                    var staffLan = await StaffHelperMethod.ProcessStaffLanguage(staffModel, _staffService);
                    var staffTerritry = await StaffHelperMethod.ProcessStaffTerritory(staffModel, _staffService);
                    try
                    {
                        _staffService.PostStaffTerritory(staffTerritry);
                        _staffService.PostStaffLanguage(staffLan);
                    }
                    catch (Exception)
                    {
                        throw new Exception("an error occured while persisting your data to the database");
                    }
                }
                var resp = StaffMapper.MapStaffDetailsToDto(staffNew, _staffService);
               
                return Ok(resp);
            }
            return Ok(staffNew);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int value, [FromBody] StaffDTO staffModel)
        {
            /*
             * Update ONLY::: Name: $("#txtStaffName").val(),
            //Surname: $("#txtStaffSurname").val(),
            //Department: $("#txtStaffDepartment").val(),
            //Manager: $("#txtStaffManager").val(),
            //StreetNumber: $("#txtStaffStreetNumber").val(),
            //HiredBy: $("#txtStaffHiredBy").val(),
            //Nationality: $("#txtStaffNationality").val()

             */
           
            var staff = StaffMapper.MapDtoToStaffDetails(staffModel, _staffService);
            var staffUpdated = await _staffService.UpdateStaff(value, staff);
            if (staff != null)
            {
                if (staffModel.Languages?.Count() > 0 || staffModel.Territory?.Count() > 0)
                {
                    var staffLan = await StaffHelperMethod.ProcessStaffLanguage(staffModel, _staffService);
                    var staffTerritry = await StaffHelperMethod.ProcessStaffTerritory(staffModel, _staffService);
                    try
                    {
                        foreach (var item in staffLan.ToList())
                        {
                            if (_staffService.GetStaffLanguages(staffModel.StaffUserId).Any(x => x.Languageid == item.Languageid))
                            {
                                staffLan.Remove(item);
                            }
                        }
                        foreach (var item in staffTerritry.ToList())
                        {
                            if (_staffService.GetStaffTerritory(staffModel.StaffUserId).Any(x => x.Territory == item.Territory))
                            {
                                staffTerritry.Remove(item);
                            }
                        }
                        _staffService.PostStaffTerritory(staffTerritry);
                        _staffService.PostStaffLanguage(staffLan);
                    }
                    catch (Exception)
                    {
                        throw new Exception("an error occured while persisting your data to the database");
                    }
                }
                var resp = StaffMapper.MapStaffDetailsToDto(staffUpdated, _staffService);
                return Ok(resp);
            }
            return Ok(staffUpdated);
        }
    }
}