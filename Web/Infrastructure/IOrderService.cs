using System;
using System.Collections.Generic;
namespace Web.Infrastructure
{
    using Models;
    public interface IOrderService
    {
        List<Order> GetAllOrdersForCompany();
    }
}
