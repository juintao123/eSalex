using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
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

        public ActionResult UpdateByOrderId(int OrderId)
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

        [HttpGet]
        public ActionResult DeleteByOrderId(int id)
        {
            orderService.DeleteOrderById(id);
            return RedirectToAction("Index");
        }

        public ActionResult InsertOrder()
        {
            List<OrderViewModel> result = orderService.GetOrderList();

            return View(result);
        }

        [HttpPost()]
        public ActionResult InsertOrder(FormCollection order)
        {
            Models.OrderService orderService = new Models.OrderService();
            return View("Index");
        }

    }
}