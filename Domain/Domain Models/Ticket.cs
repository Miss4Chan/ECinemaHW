using ECinemaDomain.Relationships;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.Domain_Models
{
    public class Ticket : BaseEntity
    {
        public enum Hall
        {
            Big,
            Medium,
            Small,
        }
        public enum MovieCategory
        {
            Comedy,
            Horror,
        }
        [Required]
/*        [Key]
        public int TicketId { get; set; }*/
        public string MovieTitle { get; set; }
        public MovieCategory Category {get;set;}
        public DateTime DateTime { get; set; }
        //Maybe expand with class about Movie for other data needed :()
        //Treba da ima vreme na prikazhuvanje 
        //Broj/tip na sala? -- could be od enum
        public Hall HallType { get; set; }
        public ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public float Price { get; set; }
        public int Duration { get; set; }
    }
}
