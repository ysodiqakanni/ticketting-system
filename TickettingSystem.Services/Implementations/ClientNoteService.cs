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

        public UserNotes UpdateNote(string note, string id, string modifiedBy)
        {
            int noteId = 0;
            if(!int.TryParse(id, out noteId))
            {
                throw new Exception("Invalid note Id");
            }
            var theNote = uow.ClientNoteRepository.Get(noteId);
            if (theNote == null)
                throw new Exception("Not not found!");
            theNote.Note = note;
            theNote.Modifiedby = modifiedBy;
            theNote.DtModified = DateTime.Now;

            uow.Save();
            return theNote;
        }
    }
}
