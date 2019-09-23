using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Services.Contracts
{
    public interface IClientNoteService
    {
        Task<UserNotes> CreateNote(string notes, string userId, string createdBy, string modifiedBy);
        Task<IList<UserNotes>> GetNotes();
        Task<IList<UserNotes>> GetNotesByClientId(int id);
    }
}
