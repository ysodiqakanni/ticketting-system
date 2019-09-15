using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.Models
{
    public class MembershipAvailablePackagesViewModel
    {
        public int ID { get; set; }
        public string MembershipName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
    public class MembershipPackagesPurchasedViewModel
    {
        public int ID { get; set; }
        public string MembershipName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string Description { get; set; }
        public DateTime PurchasedDate { get; set; }
        public DateTime PlayedDate { get; set; }
        public string Currency { get; set; }
    }
    public class Membership
    {
        public List<MembershipPackagesPurchasedViewModel> PackagesPurchased { get; set; }
        public List<MembershipAvailablePackagesViewModel> PackagesAvailable { get; set; }
    }
}
