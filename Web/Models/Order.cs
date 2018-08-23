using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    using System.Security.AccessControl;

    public class Order
    {
        
        public string Description { get; set; }

        public string OrderFinalTotal { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

    }


    public class OrderProduct
    {
        

        public Product Product { get; set; }
    
        public int Quantity { get; set; }

        public string ActualPrice { get; set; }

    }

    public class Product
    {
        public string Name { get; set; }

       
    }
}