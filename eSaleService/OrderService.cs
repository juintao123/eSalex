using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSaleService
{
    public class OrderService
    {
        /// <summary>
        /// 依照訂單編號取得訂單
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public eSaleModel.Order GetOrderById(string id)
        { 
            eSaleDao.OrderDao orderdao = new eSaleDao.OrderDao();
            return orderdao.GetOrderById(id);
        }
    }

}
