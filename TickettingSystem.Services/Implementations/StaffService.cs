using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TickettingSystem.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork uow;
        private readonly AppSettings _appSettings;
        public StaffService(IUnitOfWork _uow, IOptions<AppSettings> appSettings)
        {
            uow = _uow;
            _appSettings = appSettings.Value;
        }
        private List<StaffDetails> _users = new List<StaffDetails>
        {
            new StaffDetails { Id = 1, Firstname = "Test", Surname = "User", Staffuserid = "test", PasswordHash = "test" }
        };


        public StaffDetails Authenticate(string username, string password, out string accessToken)
        {
            accessToken = "";
            var user = _users.SingleOrDefault(x => x.Staffuserid == username && x.PasswordHash == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, user.Id.ToString())
                //}),
                Subject = new ClaimsIdentity(GetUserClaims(user)),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            accessToken = tokenHandler.WriteToken(token);

            // remove password before returning
            // user.PasswordHash = null;

            return user;
        }
        private IEnumerable<Claim> GetUserClaims(StaffDetails user)
        {
            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim("USERID", user.Staffuserid) ,
            //new Claim("ACCESS_LEVEL", user.ACCESS_LEVEL?.ToUpper()),
            //new Claim("READ_ONLY", user.READ_ONLY?.ToUpper())
            };
            return claims;
        }

        public IEnumerable<StaffDetails> GetAll()
        {
            // return users without passwords
            return _users.Select(x =>
            {
                x.PasswordHash = null;
                return x;
            });
        }
        public async Task<IList<StaffDetails>> GetAllStaffs()
        {
            var allStaff = await uow.StaffRepository.GetAllAsync();
            return allStaff;
        }

        public async Task<StaffDetails> GetStaffById(int staffId)
        {
            var staff = await uow.StaffRepository.GetAsync(staffId);
            return staff;
        }

        public async Task<StaffDetails> CreateStaff(StaffDetails staff)
        {
            var staffD = await uow.StaffRepository.AddAsync(staff);
            return staffD;
        }

        public async Task<IList<StaffDetails>> SearchByLastName(string lastname)
        {
            if (lastname == null) lastname = string.Empty;
            if (!String.IsNullOrEmpty(lastname))
                lastname = lastname.ToLower();

            var staff = await uow.StaffRepository.FindAllAsync(x => x.Surname.ToLower().Contains(lastname));
            return staff.ToList();
        }

        public async Task<IList<StaffDetails>> SearchByLastNamePrefix(string prefix)
        {
            if (prefix == null) prefix = string.Empty;
            if (!String.IsNullOrEmpty(prefix))
                prefix = prefix.ToLower();

            var staff = await uow.StaffRepository.FindAllAsync(x => x.Surname.ToLower().StartsWith(prefix));
            return staff.ToList();
        }

        public async Task<IList<StaffDetails>> SearchByLastNameSuffix(string suffix)
        {
            if (suffix == null) suffix = string.Empty;
            if (!String.IsNullOrEmpty(suffix))
                suffix = suffix.ToLower();

            var staff = await uow.StaffRepository.FindAllAsync(x => x.Surname.ToLower().EndsWith(suffix));
            return staff.ToList();
        }

        public string GetDepartmentById(int departmentId)
        {
            return uow.DepartmentRepository.Get(departmentId)?.DeptName;
        }

        public string GetManagerById(int departmentId)
        {
            return uow.DepartmentRepository.Get(departmentId)?.DeptMgr;
        }

        public async Task<IList<StaffNotes>> GetNotesByStaffId(int id)
        {
            var notes = await uow.StaffNoteRepository.FindAllAsync(x => x.Userid == id.ToString());
            return notes.ToList();
        }

        public async Task<StaffNotes> CreateNewNote(int staffId, string note)
        {
            var newNote = new StaffNotes
            {
                Userid = staffId.ToString(),
                Note = note,
                DtCreated = DateTime.Now,
                DtModified = DateTime.Now
            };
            return await uow.StaffNoteRepository.AddAsync(newNote);

        }

        public async Task<StaffDetails> UpdateStaff(int value, StaffDetails staff)
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
            //var staffModify = 
            var staffUpdate = await uow.StaffRepository.FindAsync(x => x.Id == value);
            if (staffUpdate == null)
                throw new Exception("Record not found!");
            staffUpdate.Firstname = staff.Firstname;
            staffUpdate.Surname = staff.Surname;

            staffUpdate.Housenumber = staff.Housenumber;
            staffUpdate.Streetname1 = staff.Streetname1;
            //staffUpdate.Streetname2 = staff.Streetname2;
            //staffUpdate.Streetname3 = staff.Streetname3;
            staffUpdate.Country = staff.Country;
            staffUpdate.Dob = staff.Dob;
            staffUpdate.Departmentid = staff.Departmentid;
            staffUpdate.Hiredbyid = staff.Hiredbyid;
            staffUpdate.Country = staff.Country;

            uow.Save();
            return staffUpdate;
        }

        public int GetDepartmentIdFromName(string department)
        {
            var departId = uow.DepartmentRepository
                .Find(x => x.DeptName.ToLower().Equals(department.ToLower())).FirstOrDefault().Id;
            return departId;
        }

        public string GetHiredByIdFromDepartmentName(string department)
        {
            var HiredById = uow.DepartmentRepository
                .Find(x => x.DeptName.ToLower().Equals(department.ToLower())).FirstOrDefault().DeptMgr;
            return HiredById;
        }

        public int GetLanguageIdByName(string language)
        {
            var langId = uow.LanguageRepository.Find(x => x.Language.ToLower().Equals(language.ToLower())).FirstOrDefault().Id;
            return langId;
        }

        public int GetTerritoryByName(string territory)
        {
            var territoryId = uow.TerritoriesRepository.Find(x => x.TerritoryName.ToLower().Equals(territory.ToLower())).FirstOrDefault().Id;
            return territoryId;
        }

        public void PostStaffLanguage(List<StaffLanguages> stLang=null)
        {
            uow.StaffLanguageRepository.AddRange(stLang);
            uow.Save();
        }

        public void PostStaffTerritory(List<StaffTerritory> stTerritory=null)
        {
            uow.StaffTerritoryRepository.AddRange(stTerritory);
            uow.Save();
        }

        public string GetLanguageById(int id)
        {
            return uow.LanguageRepository.Get(id).Language;
        }

        public string GetTerritoryById(int territoryId)
        {
            return uow.TerritoriesRepository.Find(x => x.Id == territoryId).FirstOrDefault().TerritoryName;
        }

        public List<StaffLanguages> GetStaffLanguages(string staffId)
        {
            var staffLanguages = uow.StaffLanguageRepository.Find(x => x.Staffuserid.Equals(staffId));
            return staffLanguages.ToList();
        }

        public List<StaffTerritory> GetStaffTerritory(string staffId)
        {
            var staffTerritory = uow.StaffTerritoryRepository.Find(x => x.Staffuserid.Equals(staffId));
            return staffTerritory.ToList();
        }
    }
}
 