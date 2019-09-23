using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.DTO
{
    public class StaffResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName1 { get; set; }
        public string StreetName2 { get; set; }
        public string StreetName3 { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ReferredBy { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime FiredOn { get; set; }
        public DateTime ResignedOn { get; set; }
        public string HiredBy { get; set; }
        public List<string> Territory { get; set; }
        public List<string> Languages { get; set; }
        public string StaffUserId { get; set; }
    }

    public class StaffDTO
    {
        public int Id { get; set; }
        public string StaffUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName1 { get; set; }
        public string StreetName2 { get; set; }
        public string StreetName3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ReferredBy { get; set; }
        public DateTime HiredOn { get; set; }
        public DateTime FiredOn { get; set; }
        public DateTime ResignedOn { get; set; }
        public string HiredBy { get; set; }
        public DateTime Created { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Territory { get; set; }

        public StaffDTO()
        {
            Created = DateTime.Now;
        }
    }

    public class StaffNoteResponseDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
    }


    public class StaffMapper
    {
        private static IStaffService _staffService;
        public static StaffResponseDTO MapStaffDetailsToDto(StaffDetails staffDetails, IStaffService staffService)
        {
            _staffService = staffService;
            string count = string.Empty;
            var result = new StaffResponseDTO
            {
                Id = staffDetails.Id,
                DateOfBirth = staffDetails.Dob,
                Nationality = staffDetails.Country,
                FiredOn = staffDetails.Firedon.Value,
                Email = staffDetails.Emailaddress,
                JoinedOn = staffDetails.HiredOn == null ? default(DateTime) : staffDetails.HiredOn.Value,
                ResignedOn = staffDetails.Resignedon == null ? default(DateTime) : staffDetails.Resignedon.Value,
                StreetName1 = staffDetails.Streetname1,
                StreetName2 = staffDetails.Streetname2,
                StreetName3 = staffDetails.Streetname3,
                StreetNumber = staffDetails.Housenumber,
                Name = staffDetails.Firstname + " "+ staffDetails.Surname,
                Surname = staffDetails.Surname,
                StaffUserId = staffDetails.Staffuserid,
            };

            result.HiredBy = "Human Rsource Mgr"; // _staffService.GetManagerById(staffDetails.Departmentid.Value);
            result.Department = _staffService.GetDepartmentById(staffDetails.Departmentid.Value);
            result.Manager = _staffService.GetManagerById(staffDetails.Departmentid.Value);
            var languages = _staffService.GetStaffLanguages(staffDetails.Staffuserid);
            var stfTerritory = _staffService.GetStaffTerritory(staffDetails.Staffuserid);
            var strList = new List<string>();
            var stfTerritoryList = new List<string>();
            foreach (var lang in languages)
            {
                var lan = _staffService.GetLanguageById((int)lang.Languageid);
                strList.Add(lan);
            }

            foreach (var stf in stfTerritory)
            {
                var territry = _staffService.GetTerritoryById((int)stf.Territory);
                stfTerritoryList.Add(territry);
            }
            result.Languages = strList;
            result.Territory = stfTerritoryList;

            return result;
        }

        public static StaffDetails MapDtoToStaffDetails(StaffDTO staffDetails, IStaffService staffService)
        {
            _staffService = staffService;
            var result = new StaffDetails
            {
                Id = staffDetails.Id,
                Staffuserid = staffDetails.StaffUserId,
                City = staffDetails.City,
                State = staffDetails.State,
                Dob = staffDetails.DateOfBirth,
                DtCreated = DateTime.Now,
                Emailaddress = staffDetails.Email,
                Firedon = staffDetails.FiredOn,
                Streetname1 = staffDetails.StreetName1,
                Streetname2 = staffDetails.StreetName2,
                Streetname3 = staffDetails.StreetName3,
                Housenumber = staffDetails.StreetNumber,
                Firstname = staffDetails.Name,
                Surname = staffDetails.Surname, 
                DtModified = DateTime.Now, 
                
            };
            //Phonenumber = "127222624333"
            //result.HiredOn = staffDetails.HiredOn;
            result.Hiredbyid = _staffService.GetHiredByIdFromDepartmentName(staffDetails.Department);
            result.Departmentid = _staffService.GetDepartmentIdFromName(staffDetails.Department);
            return result;
        }
    }

    public class StaffNoteMapper
    {
        private static IStaffService _staffService;
        public static StaffNoteResponseDTO MapStaffDetailsToDto(StaffNotes staffNotesDetail, IStaffService staffService)
        {
            _staffService = staffService;
            string count = string.Empty;
            var result = new StaffNoteResponseDTO
            {
                Id = Convert.ToInt16(staffNotesDetail.Userid),
                Content = staffNotesDetail.Note,
                DateCreated = staffNotesDetail.DtCreated
            };

            return result;
        }
    }
}