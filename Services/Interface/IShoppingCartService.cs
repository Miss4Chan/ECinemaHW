using ECinemaDomain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECinema.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string userId);
        bool deleteTicketFromShoppingCart(string userId,int ticketId);
        bool orderNow(string userId);
    }
}
