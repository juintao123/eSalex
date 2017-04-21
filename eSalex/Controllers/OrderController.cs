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

        public ActionResult GetDropDownList()
        {
            var custnamelist = orderService.GetCustName();
            var empnamelist = orderService.GetEmpName();
            var shipernamelist = orderService.GetShiperName();
            List<SelectListItem> custName = new List<SelectListItem>();
            List<SelectListItem> shipperName = new List<SelectListItem>();
            List<SelectListItem> empName = new List<SelectListItem>();
            foreach (var custname in custnamelist)
            {
                custName.Add(new SelectListItem()
                {
                    Text = custname.CustName,
                    Value = custname.CustId.ToString()
                });
            }
            foreach (var shippername in shipernamelist)
            {
                shipperName.Add(new SelectListItem()
                {
                    Text = shippername.ShiperName,
                    Value = shippername.ShipperId.ToString()
                });
            }
            foreach (var empname in empnamelist)
            {
                empName.Add(new SelectListItem()
                {
                    Text = empname.EmpName,
                    Value = empname.EmpId.ToString()
                });
            }
            ViewBag.CustId = custName;
            ViewBag.ShipperId = shipperName;
            ViewBag.EmpId = empName;
            return View();
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
            this.GetDropDownList();
            return View();
        }

        [HttpPost()]
        public ActionResult InsertOrder(Models.OrderViewModel order)
        {
            Models.OrderService orderservice = new OrderService();
            orderService.InsertOrder(order);
            return View("Index");
        }

    }
}