using ECinemaDomain.Domain_Models;
using ECinemaDomain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECinemaServices.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(int id);
        void CreateNewTicket(Ticket t);
        void UpdateExistingTicket(Ticket t);
        ShoppingCartDTO GetShoppingCartInfo(int id);
        void DeleteProduct(int id);
        bool AddToShoppingCart(AddToShoppingCartDTO item, string userId);
    }
}
