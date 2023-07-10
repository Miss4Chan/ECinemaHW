using ECinemaDomain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.DTO
{
    //Called only when needed to add to the cart 
    public class AddToShoppingCartDTO
    {
        public Ticket SelectedTicket { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
