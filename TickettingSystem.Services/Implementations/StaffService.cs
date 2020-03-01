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
using System.Security.Cryptography;
using System.IO;

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

        public StaffDetails Authenticate(string username, string password, out string accessToken)
        {
            accessToken = "";
            var allusers = GetAllStaffs().Result;
            string hashed = HashPassword(password);
            var user = allusers.SingleOrDefault(x => x.Staffuserid == username && x.PasswordHash == hashed);

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
            return GetAllStaffs().Result.Select(x =>
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

        public async Task<StaffDetails> CreateStaff(StaffDetails staff, List<string> langIds, List<string> territoryIds)
        {
            string userId = RandomString(10);
            // the password needs to be hashed
            staff.PasswordHash = HashPassword(staff.PasswordHash);
            staff.Staffuserid = userId;
            var staffD = await uow.StaffRepository.AddAsync(staff);

            // save staff Password

            // save languages
            if (langIds != null && langIds.Any())
            {
                // save new languages
                var data = new List<StaffLanguages>();
                foreach (var id in langIds)
                {
                    data.Add(new StaffLanguages
                    {
                        Languageid = int.Parse(id),
                        Staffuserid = staff.Staffuserid,
                        DtCreated = DateTime.Now,
                        DtModified = DateTime.Now
                    });
                }
                uow.StaffLanguageRepository.AddRange(data);
            }

            // save the territories
            if (territoryIds != null && territoryIds.Any())
            {
                // save new territories
                var data = new List<StaffTerritory>();
                foreach (var id in territoryIds)
                {
                    data.Add(new StaffTerritory
                    {
                        Territory = int.Parse(id),
                        Staffuserid = staff.Staffuserid,
                        DtCreated = DateTime.Now,
                        DtModified = DateTime.Now
                    });
                }
                uow.StaffTerritoryRepository.AddRange(data);
            }

            uow.Save();

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

        public StaffNotes UpdateNote(string note, string id, string modifiedBy)
        {
            int noteId = 0;
            if (!int.TryParse(id, out noteId))
            {
                throw new Exception("Invalid note Id");
            }
            var theNote = uow.StaffNoteRepository.Get(noteId);
            if (theNote == null)
                throw new Exception("Not not found!");
            theNote.Note = note;
            theNote.Modifiedby = modifiedBy;
            theNote.DtModified = DateTime.Now;

            uow.Save();
            return theNote;
        }

        public async Task<StaffDetails> UpdateStaff(int value, StaffDetails staff, List<string> langIds, List<string> territoryIds)
        {
            // check email uniqueness
            if(!(await IsUniqueEmail(staff.Id, staff.Emailaddress)))
            {
                throw new Exception("Staff email address must be unique");
            }


            // save languages
            if (langIds != null && langIds.Any())
            {
                // remove all language entries of this staff from the db
                var staffLangs = uow.StaffLanguageRepository.Find(s => s.Staffuserid == staff.Staffuserid).ToList();
                if (staffLangs != null && staffLangs.Any())
                {
                    uow.StaffLanguageRepository.RemoveRange(staffLangs);
                }

                // save new languages
                var data = new List<StaffLanguages>();
                foreach (var id in langIds)
                {
                    data.Add(new StaffLanguages
                    {
                        Languageid = int.Parse(id),
                        Staffuserid = staff.Staffuserid,
                        DtCreated = DateTime.Now,
                        DtModified = DateTime.Now
                    });
                }
                uow.StaffLanguageRepository.AddRange(data);
            }

            // save the territories
            if (territoryIds != null && territoryIds.Any())
            {
                // remove all language entries of this staff from the db
                var staffTers = uow.StaffTerritoryRepository.Find(s => s.Staffuserid == staff.Staffuserid).ToList();
                if (staffTers != null && staffTers.Any())
                {
                    uow.StaffTerritoryRepository.RemoveRange(staffTers);
                }

                // save new territories
                var data = new List<StaffTerritory>();
                foreach (var id in territoryIds)
                {
                    data.Add(new StaffTerritory
                    {
                        Territory = int.Parse(id),
                        Staffuserid = staff.Staffuserid,
                        DtCreated = DateTime.Now,
                        DtModified = DateTime.Now
                    });
                }
                uow.StaffTerritoryRepository.AddRange(data);
            }


            uow.Save();

            return staff;
        }

        public int? GetDepartmentIdFromName(string department)
        {
            var departId = uow.DepartmentRepository
                .Find(x => x.DeptName.ToLower().Equals(department.ToLower())).FirstOrDefault()?.Id;
            return departId;
        }

        public string GetHiredByIdFromDepartmentName(string department)
        {
            var HiredById = uow.DepartmentRepository
                .Find(x => x.DeptName.ToLower().Equals(department.ToLower())).FirstOrDefault()?.DeptMgr;
            return HiredById;
        }

        private async Task<bool> IsUniqueEmail(int staffId, string newEmail)
        {
            var staff = await uow.StaffRepository.GetAsync(staffId);
            if (staff == null) return false;

            if (string.Compare(staff.Emailaddress, newEmail, true) == 0) return true;

            if (uow.StaffRepository.QueryAll().Any(s => string.Compare(s.Emailaddress, newEmail, true) == 0)) return false;

            return true;
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

        public void PostStaffLanguage(List<StaffLanguages> stLang = null)
        {
            uow.StaffLanguageRepository.AddRange(stLang);
            uow.Save();
        }

        public void PostStaffTerritory(List<StaffTerritory> stTerritory = null)
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
        public List<string> GetStaffLanguageIds(string staffId)
        {
            var ids = uow.StaffLanguageRepository.QueryAll().Where(x => x.Staffuserid.Equals(staffId)).Select(s => s.Languageid.ToString()).ToList();
            return ids;
        }
        public List<string> GetStaffTeritoryIds(string staffId)
        {
            var ids = uow.StaffTerritoryRepository.Find(x => x.Staffuserid.Equals(staffId)).Select(s => s.Territory.ToString()).ToList();
            return ids;
        }

        public List<StaffTerritory> GetStaffTerritory(string staffId)
        {
            var staffTerritory = uow.StaffTerritoryRepository.Find(x => x.Staffuserid.Equals(staffId));
            return staffTerritory.ToList();
        }


        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<IList<Departments>> GetAllDepartments()
        {
            var allDepartments = await uow.DepartmentRepository.GetAllAsync();
            return allDepartments;
        }

        public async Task<bool> UpdatePassword(int id, string oldPassword, string newPassword)
        { 
            var staff = await GetStaffById(id);
            if (staff == null) return false;
            if (string.Compare(staff.PasswordHash, HashPassword(oldPassword)) != 0)
                return false;  // old password not correct
            staff.PasswordHash = HashPassword(newPassword);
            uow.Save();
            return true; 
        }
     
        public string HashPassword(string password)
        {
            string EncryptionKey = "An aweSOm3 Ticketing System";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }
    }
}
