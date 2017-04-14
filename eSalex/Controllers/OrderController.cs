using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using eSalex.Models;

namespace eSalex.Controllers
{
    public class OrderController : Controller
    {
        private Models.OrderService orderService = new Models.OrderService();
        // GET: Order
        // 訂單管理系統首頁
        public ActionResult Index()
        {
            List<OrderViewModel> result = orderService.GetOrderList();                                           
            return View(result);
        }

        public ActionResult InsertOrder()
        {
            Models.OrderViewModel order = new Models.OrderViewModel();
            return View(order);
        }

        [HttpPost]
        public ActionResult SearchByCustName(string CustName)
        {
            List<OrderViewModel> result = orderService.SeacrhByCustName(CustName);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeleteOrder(string OrderId)
        {
            orderService.DeleteOrderById(OrderId);
            return View();
        }


        [HttpPost()]
        public ActionResult InsertOrder(Models.OrderViewModel order)
        {
            Models.OrderService orderService = new Models.OrderService();
            //orderService.InsertOrder(order);
            return View("Index");
        }

    }
}