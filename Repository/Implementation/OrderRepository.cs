using ECinema.Repository.Interface;
using ECinemaDomain.Domain_Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECinema.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> getAllOrders()
        {
            return entities.Include(z => z.OrderedBy).Include(z => z.TicketsInOrder).Include("TicketsInOrder.Ticket").ToListAsync().Result;
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return entities.Include(z => z.OrderedBy).Include(z => z.TicketsInOrder).Include("TicketsInOrder.Ticket")
                .SingleOrDefaultAsync(z=>z.Id==model.Id).Result;
        }
    }
}
