using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    using Web.Data;

    using Models;
    using DataLayer;
    public class OrderService : IOrderService
    {
        DataVirtualization dataVirtualization = DataVirtualization.Instance;

        public List<Order> GetAllOrdersForCompany(){
            return CreateAllOrdersForCompany(dataVirtualization.GetOrders());
        }
        public List<Order> CreateAllOrdersForCompany(List<NewOrder> orders)
        {
            var values = new List<Order>();
            foreach(var order in orders){
                Order _order = new Order();
                _order.Description = order.Description;
                _order.OrderFinalTotal = order.OrderTotal.ToString("F2");
                _order.OrderProducts = new List<OrderProduct>();
                foreach(var orderproduct in order.OrderProducts){
                    OrderProduct _orderproduct = new OrderProduct();
                    _orderproduct.ActualPrice = orderproduct.Price.ToString("F2");
                    _orderproduct.Quantity = orderproduct.Quantity;
                    _orderproduct.Product = new Product()
                    {
                        Name = orderproduct.Product.Name
                    };
                    _order.OrderProducts.Add(_orderproduct);

                }
                values.Add(_order);
            }
            return values;

        }
    }
}