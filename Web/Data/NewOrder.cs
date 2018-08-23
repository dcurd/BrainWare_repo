using System;
using System.Collections.Generic;
namespace Web.Data
{
    public class NewOrder
    {
        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<NewOrderProduct> OrderProducts { get; set; }

    }


    public class NewOrderProduct
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public NewProduct Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }

    public class NewProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
