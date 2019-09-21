﻿using System;
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
        public DateTime? FiredOn { get; set; }
        public DateTime? ResignedOn { get; set; }
        public string HiredBy { get; set; }
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
        public DateTime? FiredOn { get; set; }
        public DateTime? ResignedOn { get; set; }
        public string HiredBy { get; set; }
        public DateTime Created { get; set; }
       
        public StaffDTO()
        {
            Created = DateTime.Now;
            HiredOn = DateTime.Now;
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
                Email = staffDetails.Emailaddress,
                StreetName1 = staffDetails.Streetname1,
                StreetName2 = staffDetails.Streetname2,
                StreetName3 = staffDetails.Streetname3,
                StreetNumber = staffDetails.Housenumber,
                Name = staffDetails.Firstname,
                Surname = staffDetails.Surname,
                
            };

            //FiredOn = staffDetails.Firedon.Value,
            //ResignedOn = staffDetails.Resignedon.Value,
            //result.HiredOn = staffDetails.HiredOn.Value;
            result.HiredBy = _staffService.GetManagerById(staffDetails.Departmentid.Value);
            result.Department = _staffService.GetDepartmentById(staffDetails.Departmentid.Value);
            result.Manager = _staffService.GetManagerById(staffDetails.Departmentid.Value);

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
                DtCreated = staffDetails.Created,
                HiredOn = staffDetails.HiredOn,
                Emailaddress = staffDetails.Email,
                Streetname1 = staffDetails.StreetName1,
                Streetname2 = staffDetails.StreetName2,
                Streetname3 = staffDetails.StreetName3,
                Housenumber = staffDetails.StreetNumber,
                Firstname = staffDetails.Name,
                Surname = staffDetails.Surname,
            };

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