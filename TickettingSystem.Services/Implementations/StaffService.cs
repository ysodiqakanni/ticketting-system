using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Services.Implementations
{
    public class StaffService: IStaffService
    {
        private readonly IUnitOfWork uow;
        public StaffService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
    }
}
