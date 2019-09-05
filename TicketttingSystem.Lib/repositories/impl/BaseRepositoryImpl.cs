using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TickettingSystem.Lib.entities;
using TicketttingSystem.Lib.config;

namespace TicketttingSystem.Lib.repositories.impl
{
    public abstract class BaseRepositoryImpl<entity> : BaseRepository<entity> where entity : BaseEntity
    {
        private readonly DBManager _dbManager;
        protected readonly ILogger _logger;
        private DbSet<entity> dbSet;
        public BaseRepositoryImpl(DBManager dBManager, ILogger logger)
        {
            this._dbManager = dBManager;
            this._logger = logger;
            setDBSet(dBManager);
        }

        private void setDBSet(DBManager dBManager)
        {
            try
            {
                Type[] genericTypes = this.GetType().GetGenericArguments();
                if (genericTypes.Length != 1)
                    throw new Exception("Error while trying to get generic type of repository");
                Type genericTypeArgument = genericTypes[0];
                var property = dBManager.GetType().GetProperty(genericTypeArgument.Name.ToLower() + "s");
                this.dbSet = (DbSet<entity>)property.GetValue(this._dbManager);
            }
            catch (Exception e)
            {
                _logger.LogError("Error while initializing databse" + e.Message);

            }
        }


        public void delete(entity entity)
        {
            this._dbManager.Remove(entity);
        }

        public List<entity> filterBy(Func<entity, bool> predicate)
        {
            return this.dbSet.Where(predicate).ToList();
        }

        public List<entity> findAll()
        {
            return this.dbSet.ToList();
        }

        public entity findById(long id)
        {
            return this.dbSet.Where(entity => entity.Id == id).First();
        }

        public entity save(entity entity)
        {
            var entityEntry = this._dbManager.Add(entity);
            this._dbManager.SaveChanges();
            return entityEntry.Entity;
        }

        public void saveAll(ICollection<entity> entities)
        {
            this.dbSet.AddRange(entities);
            this._dbManager.SaveChanges();
        }

        public entity update(entity entity)
        {
            var entityItem = this._dbManager.Update(entity);
            this._dbManager.SaveChanges();
            return entityItem.Entity;
        }
    }
}
