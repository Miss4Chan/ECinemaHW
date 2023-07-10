
using ECinemaDomain.Domain_Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECinemaDomain.Identity
{
    public class ECinemaUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ShoppingCart UserShoppingCart { get; set; } 
    }
}
