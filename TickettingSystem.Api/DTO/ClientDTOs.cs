using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Api.DTO
{
    public class ClientResponseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ReferredBy { get; set; }
        public DateTime JoinedDate { get; set; }
        public string KycLevel { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName1 { get; set; }
        public string StreetName2 { get; set; }
        public string StreetName3 { get; set; }
        public string Nationality { get; set; }
        public string Language { get; set; }
        public string RefUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    public class ClientMapper
    {
        public static ClientResponseDTO MapUserDetailsToDto(UserDetails userDetails)
        {
            var result = new ClientResponseDTO
            {
                ID = userDetails.Id,
                HouseNumber = userDetails.Housenumber,
                StreetName1 = userDetails.Streetname1,
                StreetName2 = userDetails.Streetname2,
                StreetName3 = userDetails.Streetname3,
                DateOfBirth = userDetails.Dob,
                Email = userDetails.Emailaddress,
                JoinedDate = userDetails.DtCreated,
                Language = "l",  // get language Id from updated db
                Name = userDetails.Firstname,
                Nationality = userDetails.Country,
                ReferredBy = userDetails.Referralcode,
                Surname = userDetails.Surname, 
            };
            result.KycLevel = GetKycLevel(userDetails.Id);
            result.RefUrl = GetRefUrl(userDetails.Id);
            return result;
        }
        private static string GetKycLevel(int userId)
        {

            // KycLevel = "ss", // the user_verification table tells us if a user has been verified - verification count 
                // is the kyc level (which will be missing, 1 or 2) so no kyc, basic, Advanced)
            return "Not impl";
        }
        private static string GetRefUrl(int userId)
        {
            return "Not impl";
        }
    }
}
