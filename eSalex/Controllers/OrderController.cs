using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSalex.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        // 訂單管理系統首頁
        public ActionResult Index()
        {
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.Data = orderService.GetOrders();
            //ViewBag.CustId = order.CustId;

            
            return View();
        }

        public ActionResult InsertOrder()
        {
            Models.Order order = new Models.Order();
            return View(order);
        }

        //[HttpPost]
        //public ActionResult CreateOrder(eSalex.Models.Order data)
        //{
        //    return 0;
        //}

        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.InsertOrder(order);
            return View("Index");
        }

    }
}