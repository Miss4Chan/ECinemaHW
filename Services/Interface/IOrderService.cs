using ECinemaDomain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECinema.Services.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
