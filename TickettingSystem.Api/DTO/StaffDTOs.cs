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
        public DateTime HiredOn { get; set; }
        public DateTime FiredOn { get; set; }
        public DateTime ResignedOn { get; set; }
        public string HiredBy { get; set; }
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
                HiredOn = staffDetails.HiredOn.Value,
                ResignedOn = staffDetails.Resignedon.Value,
                StreetName1 = staffDetails.Streetname1,
                StreetName2 = staffDetails.Streetname2,
                StreetName3 = staffDetails.Streetname3,
                StreetNumber = staffDetails.Housenumber,
                Name = staffDetails.Firstname,
                Surname = staffDetails.Surname,
            };

            result.HiredBy = _staffService.GetManagerById(staffDetails.Departmentid.Value);
            result.Department = _staffService.GetDepartmentById(staffDetails.Departmentid.Value);
            result.Manager = _staffService.GetManagerById(staffDetails.Departmentid.Value);

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
