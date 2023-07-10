using ECinemaDomain.Domain_Models;
using ECinemaRepository.Interface;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECinemaRepository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity"); entities.Remove(entity); context.SaveChanges();
        }

        public T Get(int id)
        {
            return entities.SingleOrDefault(z => z.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetByCompositeKey(params object[] keyValues)
        {
            return context.Set<T>().Find(keyValues);
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity"); entities.Add(entity); context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity"); entities.Update(entity); context.SaveChanges();
        }
    }
}
