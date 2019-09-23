using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.Api.DTO;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.Helper
{
    public static class StaffHelperMethod
    {
        private static IStaffService _staffService;
        public static async Task<List<StaffLanguages>> ProcessStaffLanguage(StaffDTO staffModel, IStaffService staffService)
        {
            _staffService = staffService;
            var staffLangList = new List<StaffLanguages>();
            if (staffModel.Languages == null) return null;
            foreach (var lang in staffModel.Languages)
            {
                var langId = _staffService.GetLanguageIdByName(lang);
                var staffLangModel = new StaffLanguages
                {
                    Languageid = langId,
                    Staffuserid = staffModel.StaffUserId,
                    DtCreated = DateTime.Now,
                    DtModified = DateTime.Now,
                };
                staffLangList.Add(staffLangModel);
            }
            return staffLangList;
        }

        public static async Task<List<StaffTerritory>> ProcessStaffTerritory(StaffDTO staffModel, IStaffService staffService)
        {
            _staffService = staffService;
            var staffTerritory = new List<StaffTerritory>();
            if (staffModel.Territory == null) return null;
            foreach (var territory in staffModel.Territory)
            {
                var territoryId = _staffService.GetTerritoryByName(territory);
                var staffTerritoryModel = new StaffTerritory
                {
                    Territory = territoryId,
                    Staffuserid = staffModel.StaffUserId,
                    DtCreated = DateTime.Now,
                    DtModified = DateTime.Now,
                };
                staffTerritory.Add(staffTerritoryModel);
            }
            return staffTerritory;
        }
    }
}
