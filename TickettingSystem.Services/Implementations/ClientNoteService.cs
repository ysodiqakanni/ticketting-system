using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;
using System.Linq;

namespace TickettingSystem.Services.Implementations
{
    public class ClientNoteService : IClientNoteService
    {

        private readonly IUnitOfWork uow;
        public ClientNoteService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<UserNotes> CreateNote(string notes, string userId, string createdBy, string modifiedBy)
        {
            var nt = new UserNotes
            {
                Modifiedby = modifiedBy,
                Createdby = createdBy,
                DtCreated = DateTime.Now,
                DtModified = DateTime.Now,
                Note = notes,
                Userid = userId
            };
            return await uow.ClientNoteRepository.AddAsync(nt);
        }

        public async Task<IList<UserNotes>> GetNotes()
        {
            return await uow.ClientNoteRepository.GetAllAsync();
        }

        public async Task<IList<UserNotes>> GetNotesByClientId(int id)
        {
            return uow.ClientNoteRepository.QueryAll().Where(n => n.Userid == id.ToString()).ToList(); 
        }
    }
}
