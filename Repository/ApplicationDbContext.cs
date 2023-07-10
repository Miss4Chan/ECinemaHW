
using ECinema.Domain.Domain_Models;
using ECinemaDomain.Domain_Models;
using ECinemaDomain.Identity;
using ECinemaDomain.Relationships;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ApplicationDbContext : IdentityDbContext<ECinemaUser>
    {
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ECinemaUser> ECinemaUsers { get; set; }
        public virtual DbSet<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        
        public virtual DbSet<TicketInOrder> TicketsInOrder { get;set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //za da napraime composite key od dveve
            builder.Entity<TicketInShoppingCart>().HasKey(c => new { c.ShoppingCartId, c.TicketId });
            builder.Entity<TicketInOrder>().HasKey(c => new { c.OrderId, c.TicketId });
            base.OnModelCreating(builder);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
