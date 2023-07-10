using ECinema.Services.Interface;
using ECinemaRepository.Interface;
using ECinemaDomain.Domain_Models;
using ECinemaDomain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using ECinema.Repository.Interface;
using ECinemaDomain.Relationships;
using System.Linq;
using ECinema.Domain.Domain_Models;

namespace ECinema.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {

        public readonly IUserRepository UserRepository;
        public readonly IRepository<TicketInOrder> TicketInOrderRepository;
        public readonly IRepository<ShoppingCart> ShoppingCartRepository;
        public readonly IRepository<EmailMessage> EmailRepository;
        public readonly IRepository<Order> OrderRepository;


        public ShoppingCartService(IUserRepository UserRepository, 
            IRepository<TicketInOrder> TicketInOrderRepository, IRepository<ShoppingCart> ShoppingCartRepository,
            IRepository<Order> OrderRepository, IRepository<EmailMessage> EmailRepository)
        {
            this.UserRepository = UserRepository;
            this.TicketInOrderRepository = TicketInOrderRepository;
            this.ShoppingCartRepository = ShoppingCartRepository;
            this.OrderRepository = OrderRepository;
            this.EmailRepository = EmailRepository;
        }

        public bool deleteTicketFromShoppingCart(string userId, int ticketId)
        {
            if (!string.IsNullOrEmpty(userId) && ticketId != null)
            {
                var loggedInUser = UserRepository.Get(userId);
                var userShoppingCart = loggedInUser.UserShoppingCart;
                var ticketToDelete = userShoppingCart.TicketsInShoppingCart.Where(z => z.TicketId == ticketId).FirstOrDefault();
                userShoppingCart.TicketsInShoppingCart.Remove(ticketToDelete);
                ShoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;

        }

        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {
            var user = UserRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;
            var ticketList = userShoppingCart.TicketsInShoppingCart.Select(z => new
            {
                Quantity = z.Quantity,
                TicketPrice = z.Ticket.Price
            });
            float TotalPrice = 0;
            foreach (var item in ticketList)
            {
                TotalPrice += item.TicketPrice * item.Quantity;
            }

            ShoppingCartDTO model = new ShoppingCartDTO
            {
                TotalPrice = TotalPrice,
                ticketsInShoppingCart = userShoppingCart.TicketsInShoppingCart.ToList()
            };
            return model;
        }

        public bool orderNow(string userId)
        {
            var user = UserRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;

            EmailMessage emailMessage = new EmailMessage();
            emailMessage.MailTo = user.Email;
            emailMessage.Subject = "Sucessfully created order";
            emailMessage.Status = false;

            Order newOrder = new Order
            {
                ECinemaUserId = user.Id,
                OrderedBy = user,
            };
            OrderRepository.Insert(newOrder);
            List<TicketInOrder> ticketInOrder = userShoppingCart.TicketsInShoppingCart.Select(z => new TicketInOrder
            {
                Ticket = z.Ticket,
                TicketId = z.TicketId,
                Order = newOrder,
                OrderId = newOrder.Id,
                Quantity = z.Quantity
            }).ToList();

            StringBuilder sb = new StringBuilder();
            var totalPrice = 0.0;

            sb.AppendLine("Your order is completed. The order conatins: ");

            for (int i = 1; i <= ticketInOrder.Count(); i++)
            {
                var currentItem = ticketInOrder[i - 1];
                totalPrice += currentItem.Quantity * currentItem.Ticket.Price;
                sb.AppendLine(i.ToString() + ". " + currentItem.Ticket.MovieTitle + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Ticket.Price);
            }

            sb.AppendLine("Total price for your order: " + totalPrice.ToString());

            emailMessage.Content = sb.ToString();


            foreach (var item in ticketInOrder)
            { TicketInOrderRepository.Insert(item); }

            user.UserShoppingCart.TicketsInShoppingCart.Clear();
            this.EmailRepository.Insert(emailMessage);
            UserRepository.Update(user);
            return true;
        }
    }
}
