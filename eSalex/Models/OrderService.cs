using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSalex.Models
{

    public class OrderService
    {

        public void InsertOrder(Models.Order order)
        {

        }

        public void DeleteOrderById(string id)
        {

        }

        public void UpdateOrder(Models.Order order)
        {

        }

        public Models.Order GetOrderById(string id)
        {
            Models.Order result = new Order();
            result.CustId = "123";
            result.CustName = "TAO";
            return result;
        } 

        public List<Models.Order> GetOrders()
        {
            return new List<Order>();
        }
    }
}