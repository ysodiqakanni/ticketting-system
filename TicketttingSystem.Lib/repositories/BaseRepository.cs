using System;
using System.Collections.Generic;
using TickettingSystem.Lib.entities;

namespace TicketttingSystem.Lib.repositories
{
    public interface BaseRepository<entity> where entity: BaseEntity
    {
        entity save(entity entity);
        void saveAll(ICollection<entity> entities);
        entity update(entity entity);
        void delete(entity entity);
        entity findById(long id);
        List<entity> findAll();

        List<entity> filterBy(Func<entity, Boolean> predicate);

    }
}
