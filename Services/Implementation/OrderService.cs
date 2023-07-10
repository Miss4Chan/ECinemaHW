using ECinema.Repository.Interface;
using ECinema.Services.Interface;
using ECinemaDomain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECinema.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository OrderRepository;

        public OrderService (IOrderRepository OrderRepository)
        {
            this.OrderRepository = OrderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return OrderRepository.getAllOrders();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return OrderRepository.getOrderDetails(model);
        }
    }
}
