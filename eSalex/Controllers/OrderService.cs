using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSalex.Models
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {
        private 
        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        public void InsertOrder(Models.Order order)
        {

        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrderById(string id)
        {

        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Models.Order order)
        {

        }

        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Order GetOrderById(string id)
        {
            Models.Order result = new Order();
            result.CustId = "123";
            result.CustName = "TAO";
            return result;
        } 

        public List<Models.Order> GetOrders()
        {
            List<Models.Order> result = new List<Order>();
            result.Add(new Order() { CustId = "TAO", CustName = "濤", EmpId = 1, EmpName = "MAX", OrderId = 1 });
            result.Add(new Order() { CustId = "MIN", CustName = "利", EmpId = 2, EmpName = "MAX", OrderId = 2 });
            return result;
        }
    }
}