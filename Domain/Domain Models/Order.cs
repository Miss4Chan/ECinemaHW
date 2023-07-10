using ECinemaDomain.Identity;
using ECinemaDomain.Relationships;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.Domain_Models
{
    public class Order : BaseEntity
    {
/*        [Key]
        public int OrderId { get; set; }*/
        
        public string ECinemaUserId { get; set; }
        public ECinemaUser OrderedBy { get; set; }

        public List<TicketInOrder>  TicketsInOrder{ get; set; }
    }
}
