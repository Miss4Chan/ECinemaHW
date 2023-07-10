using ECinema.Repository.Interface;
using ECinemaDomain.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECinema.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<ECinemaUser> entities;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ECinemaUser>();
        }

        public void Delete(ECinemaUser entity)
        {
            if (entity == null) throw new ArgumentNullException(); entities.Remove(entity); context.SaveChanges();
        }

        public ECinemaUser Get(string id)
        {
            return entities.Include(z => z.UserShoppingCart).Include("UserShoppingCart.TicketsInShoppingCart")
                .Include("UserShoppingCart.TicketsInShoppingCart.Ticket")
                .SingleOrDefault(x => x.Id == id) ?? throw new ArgumentNullException();
        }

        public IEnumerable<ECinemaUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(ECinemaUser entity)
        {
            if (entity == null) throw new ArgumentNullException(); entities.Add(entity); context.SaveChanges();
        }

        public void Update(ECinemaUser entity)
        {
            if (entity == null) throw new ArgumentNullException(); entities.Update(entity); context.SaveChanges();
        }
    }
}
