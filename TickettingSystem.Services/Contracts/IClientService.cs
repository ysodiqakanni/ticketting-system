﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;

namespace TickettingSystem.Services.Contracts
{
    public interface IClientService
    {
        Task<IList<Client>> GetAllAsync();
    }
}
