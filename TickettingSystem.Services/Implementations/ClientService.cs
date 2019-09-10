﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork uow;
        public ClientService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public async Task<IList<Client>> GetAllAsync()
        {
            return await uow.ClientRepository.GetAllAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            var x1 = uow.ClientRepository.GetAll();
            var x2 = uow.ClientRepository.Get(id);
            

            return await uow.ClientRepository.FindAsync(x => x.ID == id);
        }
    }
}
