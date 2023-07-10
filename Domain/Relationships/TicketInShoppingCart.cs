using ECinemaDomain.Domain_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.Relationships
{
    public class TicketInShoppingCart : BaseEntity
    {
        public int TicketId { get; set; }
        public int ShoppingCartId { get; set; }
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        public int Quantity { get; set; }

    }
}
