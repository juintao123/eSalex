using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using CCC.ORM.Helpers;
using eSalex.Models;


namespace eSalex.Models
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        private List<Models.OrderViewModel>MapOrderDataToList(DataTable orderData)
        {
            List<Models.OrderViewModel> result = new List<OrderViewModel>();
            foreach(DataRow row in orderData.Rows)
            {
                result.Add(new OrderViewModel()
                {
                    CustId = row["CustomerID"].ToString(),
                    CustName = row["CustName"].ToString(),
                    EmpId = row["EmployeeID"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    Freight = (decimal)row["Freight"],
                    Orderdate = row["Orderdate"] == DBNull.Value ? (DateTime?)null:(DateTime)row["Orderdate"],
                    OrderId = (int)row["OrderID"],
                    RequireDdate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
                    ShipAddress = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString(),
                    ShipName = row["ShipName"].ToString(),
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    ShipperId = (int)row["ShipperID"],
                    ShiperName = row["ShiperName"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString(),
                });
            }

            return result;
        } 
               
        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        public void InsertOrder()
        {
            DataTable dt = new DataTable();
            string sql = @"INSERT INTO Sales.Orders VALUES(@OrderID,@CustomeerID)
            A.OrderID,A.CustomerID,B.CompanyName AS CustName,A.EmployeeID,C.LastName + C.FirstName AS EmpName,
            A.OrderDate,A.RequiredDate,A.ShippedDate,A.ShipperID,D.CompanyName AS ShiperName,
	        A.Freight,A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry

            FROM Sales.Orders AS A INNER JOIN Sales.Customers AS B ON A.CustomerID = B.CustomerID
            INNER JOIN HR.Employees AS C ON A.EmployeeID = C.EmployeeID
            INNER JOIN Sales.Shippers AS D ON A.ShipperID = D.ShipperID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter allOrder = new SqlDataAdapter(cmd);
                allOrder.Fill(dt);
                conn.Close();

            }
        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrderById(string OrderId)
        {
            DataTable dt = new DataTable();
            string sql = @"DELETE FROM Sales.Order WHERE OrderId = @OrderId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId",OrderId));
                SqlDataAdapter searchOrder = new SqlDataAdapter(cmd);
                searchOrder.Fill(dt);
                conn.Close();

            }

        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(string OrderId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT
            A.OrderID,A.CustomerID,B.CompanyName AS CustName,A.EmployeeID,C.LastName + C.FirstName AS EmpName,
            A.OrderDate,A.RequiredDate,A.ShippedDate,A.ShipperID,D.CompanyName AS ShiperName,
	        A.Freight,A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry

            FROM Sales.Orders AS A INNER JOIN Sales.Customers AS B ON A.CustomerID = B.CustomerID
            INNER JOIN HR.Employees AS C ON A.EmployeeID = C.EmployeeID
            INNER JOIN Sales.Shippers AS D ON A.ShipperID = D.ShipperID

            WHERE B.CompanyName = @CustName";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustName", CustName));
                SqlDataAdapter searchOrder = new SqlDataAdapter(cmd);
                searchOrder.Fill(dt);
                conn.Close();

            }

        }

        /// <summary>
        /// 查詢訂單
        /// </summary>
        /// <param name="order"></param>
        public List<Models.OrderViewModel> SeacrhByCustName(string CustName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT
            A.OrderID,A.CustomerID,B.CompanyName AS CustName,A.EmployeeID,C.LastName + C.FirstName AS EmpName,
            A.OrderDate,A.RequiredDate,A.ShippedDate,A.ShipperID,D.CompanyName AS ShiperName,
	        A.Freight,A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry

            FROM Sales.Orders AS A INNER JOIN Sales.Customers AS B ON A.CustomerID = B.CustomerID
            INNER JOIN HR.Employees AS C ON A.EmployeeID = C.EmployeeID
            INNER JOIN Sales.Shippers AS D ON A.ShipperID = D.ShipperID

            WHERE B.CompanyName = @CustName";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustName",CustName));
                SqlDataAdapter searchOrder = new SqlDataAdapter(cmd);
                searchOrder.Fill(dt);
                conn.Close();

            }
            return this.MapOrderDataToList(dt);
        }

        /// <summary>
        /// 訂單管理首頁，取得所有訂單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Models.OrderViewModel> GetOrderList()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT
            A.OrderID,A.CustomerID,B.CompanyName AS CustName,A.EmployeeID,C.LastName + C.FirstName AS EmpName,
            A.OrderDate,A.RequiredDate,A.ShippedDate,A.ShipperID,D.CompanyName AS ShiperName,
	        A.Freight,A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry

            FROM Sales.Orders AS A INNER JOIN Sales.Customers AS B ON A.CustomerID = B.CustomerID
            INNER JOIN HR.Employees AS C ON A.EmployeeID = C.EmployeeID
            INNER JOIN Sales.Shippers AS D ON A.ShipperID = D.ShipperID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter allOrder = new SqlDataAdapter(cmd);
                allOrder.Fill(dt);
                conn.Close();

            }
            return this.MapOrderDataToList(dt);
            
        }

    }

   

}