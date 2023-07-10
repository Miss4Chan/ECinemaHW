using ECinema.Repository.Interface;
using ECinemaDomain.Domain_Models;
using ECinemaDomain.DTO;
using ECinemaDomain.Relationships;
using ECinemaRepository.Interface;
using ECinemaServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECinemaServices.Implementation
{
    public class TicketService : ITicketService
    {
        public readonly IRepository<Ticket> TicketRepository;
        public readonly IRepository<TicketInShoppingCart> TicketInShoppingCartRepository;
        public readonly IUserRepository UserRepository;
        
        public TicketService(IRepository<Ticket> TicketRepository, IUserRepository UserRepository,
            IRepository<TicketInShoppingCart> TicketInShoppingCartRepository)
        {
            this.TicketRepository = TicketRepository;   
            this.UserRepository = UserRepository;
            this.TicketInShoppingCartRepository = TicketInShoppingCartRepository;
        }
        public bool AddToShoppingCart(AddToShoppingCartDTO item, string userId)
        {
            var user = this.UserRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;

            if(userShoppingCart!=null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);
                if (ticket != null)
                {
                    //Composite keyto stopnuva dodavanje pak ist film hahahahha -- solved samo zgolemi quantity na existing ez :))                   
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                     };
                    var doesItExist = TicketInShoppingCartRepository.GetByCompositeKey(itemToAdd.ShoppingCartId, itemToAdd.TicketId);
                    if (doesItExist != null)
                    {
                        doesItExist.Quantity += itemToAdd.Quantity;
                        this.TicketInShoppingCartRepository.Update(doesItExist);
                    }
                    else
                        this.TicketInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
            }
            return false;

        }

        public void CreateNewTicket(Ticket t)
        {
            this.TicketRepository.Insert(t);
        }

        public void DeleteProduct(int id)
        {
            var t = TicketRepository.Get(id);
            this.TicketRepository.Delete(t);
        }

        public List<Ticket> GetAllTickets()
        {
            return TicketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(int id)
        {
            return TicketRepository.Get(id);
        }

        public ShoppingCartDTO GetShoppingCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateExistingTicket(Ticket t)
        {
            TicketRepository.Update(t);
        }
    }
}
