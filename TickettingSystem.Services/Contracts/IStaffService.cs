using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Services.Contracts
{
    public interface IStaffService
    {
        StaffDetails Authenticate(string username, string password, out string accessToken);
        IEnumerable<StaffDetails> GetAll();
    }
}
