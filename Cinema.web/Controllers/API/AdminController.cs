using ECinema.Services.Interface;
using ECinemaDomain.Domain_Models;
using ECinemaDomain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECinema.Domain.DTO;

namespace ECinema.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService OrderService;
        private readonly UserManager<ECinemaUser> userManager;
        public AdminController(IOrderService OrderService, UserManager<ECinemaUser> userManager)
        {
            this.OrderService = OrderService;
            this.userManager = userManager;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return this.OrderService.GetAllOrders();
        }


        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this.OrderService.GetOrderDetails(model);
        }
        
        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDTO> model)
        {
            bool status = true;
            foreach (var user in model)
            {
                var userCheck = userManager.FindByEmailAsync(user.Email).Result;
                if(userCheck == null)
                {
                    var newUser = new ECinemaUser
                    {
                        UserName = user.Email,
                        Email = user.Email,
                        Address = user.Email,
                        DateOfBirth = user.DateOfBirth,
                        EmailConfirmed = true,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserShoppingCart = new ShoppingCart()
                    };
                    var result = userManager.CreateAsync(newUser, user.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }
    }
}
