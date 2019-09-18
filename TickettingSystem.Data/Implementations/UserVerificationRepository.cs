using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Data.Implementations
{
    public class UserVerificationRepository : BaseRepository<UserVerification>, IUserVerificationRepository
    {
        public UserVerificationRepository(DbContext ctx) : base(ctx)
        {

        }
    }
}
