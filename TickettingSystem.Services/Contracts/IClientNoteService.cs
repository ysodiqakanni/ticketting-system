using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;

namespace TickettingSystem.Services.Contracts
{
    public interface IClientNoteService
    {
        Task<ClientNote> CreateNote(string notes);
        Task<IList<ClientNote>> GetNotes();
    }
}
