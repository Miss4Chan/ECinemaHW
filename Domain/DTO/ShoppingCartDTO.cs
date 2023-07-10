
using ECinemaDomain.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.DTO
{
    //Called to display the shopping cart
    public class ShoppingCartDTO
    {
        public List<TicketInShoppingCart> ticketsInShoppingCart { get; set; }
        public float TotalPrice { get; set; }
    }
}
