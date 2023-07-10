using ECinemaDomain.Relationships;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.Domain_Models
{
    public class ShoppingCart : BaseEntity
    {
/*        [Key]
        public int ShoppingCartId { get; set; }*/
        public string ECinemaUserId { get; set; }

        public ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
