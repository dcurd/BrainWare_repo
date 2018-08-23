using System;
using System.Collections.Generic;
namespace Web.DataLayer
{
    using Infrastructure;
    using Web.Data;
    using System.Data;
    public class DataVirtualization
    {
        private static DataVirtualization instance = null;
        private static readonly object dvObjectLock=new object();
        Database database = Database.Instance;
        List<NewOrder> newOrders = new List<NewOrder>();
        public static DataVirtualization Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (dvObjectLock)
                    {
                        if (instance == null)
                        {
                            instance = new DataVirtualization();
                        }
                    }
                }
                return instance;
            }
        }
        private DataVirtualization()
        {
            newOrders = GetOrdersForCompany();
        }

        public List<NewOrder> GetOrders(){
            return newOrders;
        }

        public List<NewOrder> GetOrdersForCompany()
        {

           

            // Get the orders
            var sql1 =
                "SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id";

            var reader1 = database.ExecuteReader(sql1);

            var values = new List<NewOrder>();

            while (reader1.Read())
            {
                var record1 = (IDataRecord)reader1;

                values.Add(new NewOrder()
                {
                    CompanyName = record1.GetString(0),
                    Description = record1.GetString(1),
                    OrderId = record1.GetInt32(2),
                    OrderProducts = new List<NewOrderProduct>()
                });

            }

            reader1.Close();

            //Get the order products
            var sql2 =
                "SELECT op.price, op.order_id, op.product_id, op.quantity, p.name, p.price FROM orderproduct op INNER JOIN product p on op.product_id=p.product_id";

            var reader2 = database.ExecuteReader(sql2);

            var values2 = new List<NewOrderProduct>();

            while (reader2.Read())
            {
                var record2 = (IDataRecord)reader2;

                values2.Add(new NewOrderProduct()
                {
                    OrderId = record2.GetInt32(1),
                    ProductId = record2.GetInt32(2),
                    Price = record2.GetDecimal(0),
                    Quantity = record2.GetInt32(3),
                    Product = new NewProduct()
                    {
                        Name = record2.GetString(4),
                        Price = record2.GetDecimal(5)
                    }
                });
            }

            reader2.Close();

            foreach (var order in values)
            {
                foreach (var orderproduct in values2)
                {
                    if (orderproduct.OrderId != order.OrderId)
                        continue;

                    order.OrderProducts.Add(orderproduct);
                    order.OrderTotal = order.OrderTotal + (orderproduct.Price * orderproduct.Quantity);
                }
            }

            return values;
        }
    }
}
