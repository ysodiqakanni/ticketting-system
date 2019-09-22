using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    // NOTE!!! DONOT IMPLEMENT THESE YET!!!!
    public static class MembershipsApi
    {
        public static Task<List<MembershipAvailablePackagesViewModel>> GetAvailableMembershipPackages()
        {
            var memberships = new List<MembershipAvailablePackagesViewModel>
            {
              new MembershipAvailablePackagesViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-1), Price=100},
              new MembershipAvailablePackagesViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-1), Price=100},
              new MembershipAvailablePackagesViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-1), Price=100},
              new MembershipAvailablePackagesViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-1), Price=100},
              new MembershipAvailablePackagesViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-1), Price=100}
            };
            return Task.Run(() => { return memberships; });
        }
        public static Task<List<MembershipPackagesPurchasedViewModel>> GetMembershipPackagesPurchased()
        {
            var memberships = new List<MembershipPackagesPurchasedViewModel>
            {
              new MembershipPackagesPurchasedViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", PurchasedDate = DateTime.Now.AddYears(-3), PlayedDate = DateTime.Now.AddYears(-1), Currency="Dollar"},
              new MembershipPackagesPurchasedViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", PurchasedDate = DateTime.Now.AddYears(-3), PlayedDate = DateTime.Now.AddYears(-1), Currency="Dollar"},
              new MembershipPackagesPurchasedViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", PurchasedDate = DateTime.Now.AddYears(-3), PlayedDate = DateTime.Now.AddYears(-1), Currency="Dollar"},
              new MembershipPackagesPurchasedViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", PurchasedDate = DateTime.Now.AddYears(-3), PlayedDate = DateTime.Now.AddYears(-1), Currency="Dollar"},
              new MembershipPackagesPurchasedViewModel{ID = 1, MembershipName="Mr Adam Smith", DateEnabled = DateTime.Now.AddYears(-5), Description="Lorem ipsum dolor sit amet.", PurchasedDate = DateTime.Now.AddYears(-3), PlayedDate = DateTime.Now.AddYears(-1), Currency="Dollar"}
            };
            return Task.Run(() => { return memberships; });
        }
    }
}