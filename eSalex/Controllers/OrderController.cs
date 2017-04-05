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
        public ActionResult Index()
        {
            //Models.OrderService orderService = new Models.OrderService();
            //var order = orderService.GetOrderById("123");
            //ViewBag.CustId = order.CustId;

            
            return View();
        }

        public ActionResult InsertOrder()
        {
            Models.Order order = new Models.Order();
            order.CustName = "TAO";
            return RedirectToAction("Index");
        }

        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.InsertOrder(order);
            return View("Index");
        }

        [HttpGet()]
        public JsonResult TestJson()
        {
            var result = new Models.Order();
            result.CustId = "123";
            result.CustName = "TAO"; //實際上是透過資料庫抓取，該處僅為練習

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}