using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Services.Implementations
{
    class ClientNoteService : IClientNoteService
    {

        private readonly IUnitOfWork uow;
        public ClientNoteService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<ClientNote> CreateNote(string notes)
        {
            var nt = new ClientNote
            {
                Notes = notes
            };
            return await uow.ClientNoteRepository.AddAsync(nt);
        }

        public async Task<IList<ClientNote>> GetNotes()
        {
            return await uow.ClientNoteRepository.GetAllAsync();
        }
    }
}
