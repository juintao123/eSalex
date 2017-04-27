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





        [HttpGet]
        public ActionResult UpdateByOrderId(string OrderId)
        {
            Models.OrderViewModel order = new Models.OrderViewModel();
            List<OrderDetailViewModel> detail = new List<OrderDetailViewModel>();
            Models.OrderService service = new OrderService();
            this.GetDropDownList();
            foreach (var data in service.GetOrderById(OrderId))
            {
                order.OrderId = data.OrderId;
                order.Orderdate = data.Orderdate;
                order.RequiredDate = data.RequiredDate;
                order.ShippedDate = data.ShippedDate;
                order.Freight = data.Freight;
                order.ShipName = data.ShipName;
                order.ShipAddress = data.ShipAddress;
                order.ShipCity = data.ShipCity;
                order.ShipRegion = data.ShipRegion;
                order.ShipPostalCode = data.ShipPostalCode;
                order.ShipCountry = data.ShipCountry;
                order.CustId = data.CustId;
                order.EmpId = data.EmpId;
                order.ShipperId = data.ShipperId;
            }
            foreach (var data in service.GetOrderDetailById(OrderId))
            {
                detail.Add(new OrderDetailViewModel()
                {
                    OrderId = data.OrderId,
                    ProductId = data.ProductId,
                    ProductName = data.ProductName,
                    UnitPrice = data.UnitPrice,
                    Qty = data.Qty
                });
            }
            ViewBag.OrderDetail = detail;
            return View(order);
        }
        [HttpPost]
        public ActionResult UpdateByOrderId(Models.OrderViewModel order)
        {
            Models.OrderService service = new OrderService();
            service.UpdateByOrderId(order);
            return RedirectToAction("SearchOrder");
        }







        public ActionResult SearchOrder()
        {
            this.GetDropDownList();
            return View();
        }
        
        [HttpPost]
        public ActionResult SearchOrderDone(Models.OrderViewModel order)
        {
            this.GetDropDownList();
            List<OrderViewModel> result = orderService.SeacrhOrder(order);
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
            return View();
        }

    }
}