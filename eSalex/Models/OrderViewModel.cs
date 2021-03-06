﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using eSalex.Models;

namespace eSalex.Models
{
    public class OrderViewModel
    {
        public IEnumerable<SelectListItem> MyList { get; set; }

        /// <summary>
        /// 訂單編號
        /// </summary>
        [Display(Name ="訂單編號")]
        public int OrderId { get; set; }

        /// <summary>
        /// 客戶代號
        /// </summary>
        [Display(Name = "客戶代號")]
        public string CustId { get; set; }

        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Display(Name = "客戶名稱")]
        public string CustName { get; set; }

        /// <summary>
        /// 業務(員工)代號
        /// </summary>
        [Display(Name = "業務(員工)代號")]
        public int EmpId { get; set; }

        /// <summary>
        /// 業務(員工姓名)
        /// </summary>
        [Display(Name = "業務(員工)姓名")]
        public string EmpName { get; set; }

        /// <summary>
        /// 訂單日期
        /// </summary>
        [Display(Name = "訂單日期")]
        public DateTime? Orderdate { get; set; }

        /// <summary>
        /// 需要日期
        /// </summary>
        [Display(Name = "需要日期")]
        public DateTime? RequiredDate { get; set; }

        /// <summary>
        /// 出貨日期
        /// </summary>
        [Display(Name = "出貨日期")]
        public DateTime? ShippedDate { get; set; }

        /// <summary>
        /// 出貨公司代號
        /// </summary>
        [Display(Name = "出貨公司代號")]
        public int ShipperId { get; set; }

        /// <summary>
        /// 出貨公司名稱
        /// </summary>
        [Display(Name = "出貨公司名稱")]
        public string ShiperName { get; set; }

        /// <summary>
        /// 運費
        /// </summary>
        [Display(Name = "運費")]
        public decimal Freight { get; set; }

        /// <summary>
        /// 出貨說明
        /// </summary>
        [Display(Name = "出貨說明")]
        public string ShipName { get; set; }

        /// <summary>
        /// 出貨地址
        /// </summary>
        [Display(Name = "出貨地址")]
        public string ShipAddress { get; set; }

        /// <summary>
        /// 出貨城市
        /// </summary>
        [Display(Name = "出貨城市")]
        public string ShipCity { get; set; }

        /// <summary>
        /// 出貨地區
        /// </summary>
        [Display(Name = "出貨地區")]
        public string ShipRegion { get; set; }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        [Display(Name = "郵遞區號")]
        public string ShipPostalCode { get; set; }

        /// <summary>
        /// 出貨國家
        /// </summary>
        [Display(Name = "出貨國家")]
        public string ShipCountry { get; set; }
    }
}