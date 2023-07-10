using ECinemaDomain.Domain_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.Relationships
{
    public class TicketInOrder : BaseEntity
    {
        [ForeignKey("TicketId")]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
