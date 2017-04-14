using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSaleDao
{
    public class OrderDao
    {
        /// <summary>
        /// 依照訂單編號取得訂單
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public eSaleModel.Order GetOrderById(string Id)
        {
            return new eSaleModel.Order()
            {
                CustId = "TAO",
                CustName = "濤",
                OrderId = "1"
            };
        }
    }
}
