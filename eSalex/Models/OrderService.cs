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
using System.Web.Mvc;


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
                    EmpId = (int)row["EmployeeID"],
                    EmpName = row["EmpName"].ToString(),
                    Freight = (decimal)row["Freight"],
                    Orderdate = row["Orderdate"] == DBNull.Value ? (DateTime?)null:(DateTime)row["Orderdate"],
                    OrderId = (int)row["OrderID"],
                    RequiredDate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
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

        private List<Models.OrderDetailViewModel> MapOrderDetailDataToList(DataTable orderData)
        {
            List<Models.OrderDetailViewModel> result = new List<OrderDetailViewModel>();
            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new OrderDetailViewModel()
                {
                    OrderId = (int)row["OrderId"],
                    ProductId = (int)row["ProductId"],
                    ProductName = row["ProductName"].ToString(),
                    UnitPrice = (double)(row["UnitPrice"]),
                    Qty = (Int16)row["Qty"],
                });
            }
            return result;
        }
    


    /// <summary>
    /// 新增訂單
    /// </summary>
    /// <param name="order"></param>
    public void InsertOrder(Models.OrderViewModel order)
        {
            string sql = @"INSERT INTO [Sales].[Orders]

            ([CustomerID],[EmployeeID],[OrderDate],[RequiredDate],[ShippedDate],[ShipperID]
            ,[Freight],[ShipName],[ShipAddress],[ShipCity],[ShipRegion],[ShipPostalCode],[ShipCountry])

             VALUES
            (
            @custid,@empid,@orderdate,@requireddate,@shippeddate,@shipperid,
            @freight,@shipname,@shipaddress,@shipcity,@shipregion,@shippostalcode,@shipcountry
            )

            select scope_identity()
            ";
            string format1 = Convert.ToDateTime(order.Orderdate).ToString("yyyy-MM-dd");
            string format2 = Convert.ToDateTime(order.RequiredDate).ToString("yyyy-MM-dd");
            string format3 = Convert.ToDateTime(order.ShippedDate).ToString("yyyy-MM-dd");

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@custid", order.CustId));
                cmd.Parameters.Add(new SqlParameter("@empid", order.EmpId));
                cmd.Parameters.Add(new SqlParameter("@orderdate", format1));
                cmd.Parameters.Add(new SqlParameter("@requireddate", format2));
                cmd.Parameters.Add(new SqlParameter("@shippeddate", format3));
                cmd.Parameters.Add(new SqlParameter("@shipperid", order.ShipperId));
                cmd.Parameters.Add(new SqlParameter("@freight", order.Freight));
                cmd.Parameters.Add(new SqlParameter("@shipname", order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@shipaddress", order.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@shipcity", order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@shipregion", order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@shippostalcode", order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@shipcountry", order.ShipCountry));

                //orderId = (int)cmd.ExecuteScalar();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }




        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrderById(int OrderId)
        {
            DataTable dt = new DataTable();
            string sql = @"DELETE FROM Sales.OrderDetails WHERE OrderID = @OrderId

                           DELETE FROM Sales.Orders WHERE OrderID = @OrderId";
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
        public void UpdateByOrderId(Models.OrderViewModel order)
        {
            string sql = @"UPDATE [Sales].[Orders]

                         SET [CustomerID] = @custid
                         ,[EmployeeID] = @empid
                         ,[OrderDate] = @orderdate
                         ,[RequiredDate] = @requireddate
                         ,[ShippedDate] = @shippeddate
                         ,[ShipperID] = @shipperid
                         ,[Freight] = @freight
                         ,[ShipName] = @shipname
                         ,[ShipAddress] = @shipaddress
                         ,[ShipCity] = @shipcity
                         ,[ShipRegion] = @shipregion
                         ,[ShipPostalCode] = @shippostalcode
                         ,[ShipCountry] = @shipcountry

                          WHERE [OrderID] = @orderid
            ";
            string format1 = Convert.ToDateTime(order.Orderdate).ToString("yyyy-MM-dd");
            string format2 = Convert.ToDateTime(order.RequiredDate).ToString("yyyy-MM-dd");
            string format3 = Convert.ToDateTime(order.ShippedDate).ToString("yyyy-MM-dd");

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@orderid", order.OrderId));
                cmd.Parameters.Add(new SqlParameter("@custid", order.CustId));
                cmd.Parameters.Add(new SqlParameter("@empid", order.EmpId));
                cmd.Parameters.Add(new SqlParameter("@orderdate", format1));
                cmd.Parameters.Add(new SqlParameter("@requireddate", format2));
                cmd.Parameters.Add(new SqlParameter("@shippeddate", format3));
                cmd.Parameters.Add(new SqlParameter("@shipperid", order.ShipperId));
                cmd.Parameters.Add(new SqlParameter("@freight", order.Freight.ToString() == "0" ? string.Empty : order.Freight.ToString()));
                cmd.Parameters.Add(new SqlParameter("@shipname", order.ShipName == null ? string.Empty : order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@shipaddress", order.ShipAddress == null ? string.Empty : order.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@shipcity", order.ShipCity == null ? string.Empty : order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@shipregion", order.ShipRegion == null ? string.Empty : order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@shippostalcode", order.ShipPostalCode == null ? string.Empty : order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@shipcountry", order.ShipCountry == null ? string.Empty : order.ShipCountry));

                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }





        /// <summary>
        /// 查詢訂單
        /// </summary>
        /// <param name="order"></param>
        public List<Models.OrderViewModel> SeacrhOrder(Models.OrderViewModel order)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT
                          A.OrderID,A.CustomerID,B.CompanyName AS CustName,A.EmployeeID,C.LastName + C.FirstName AS EmpName,
                          A.OrderDate,A.RequiredDate,A.ShippedDate,A.ShipperID,D.CompanyName AS ShiperName,
	                      A.Freight,A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry
                          
                          FROM Sales.Orders AS A INNER JOIN Sales.Customers AS B ON A.CustomerID = B.CustomerID
                          INNER JOIN HR.Employees AS C ON A.EmployeeID = C.EmployeeID
                          INNER JOIN Sales.Shippers AS D ON A.ShipperID = D.ShipperID

                          WHERE (A.OrderID = @OrderId OR @OrderId = '')
                          AND  (B.CompanyName Like '%'+@CustName+'%')
                          AND  (A.EmployeeID = @EmpId OR @EmpId = '')
                          AND  (D.ShipperID = @ShipperId OR @ShipperId = '')
                          AND  (A.OrderDate = @Orderdate OR @Orderdate = '')
                          AND  (A.ShippedDate = @ShippedDate OR @ShippedDate = '')
                          AND  (A.RequiredDate = @RequiredDate OR @RequiredDate = '')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", order.OrderId.ToString()=="0"?string.Empty:order.OrderId.ToString()));
                cmd.Parameters.Add(new SqlParameter("@CustName", order.CustName==null?string.Empty:order.CustName.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EmpId", order.EmpId.ToString()== "0" ? string.Empty:order.EmpId.ToString()));
                cmd.Parameters.Add(new SqlParameter("@ShipperId", order.ShipperId.ToString()== "0" ? string.Empty:order.ShipperId.ToString()));
                cmd.Parameters.Add(new SqlParameter("@Orderdate", Convert.ToDateTime(order.Orderdate).ToString("yyyy-MM-dd") == "0001-01-01" ? string.Empty: Convert.ToDateTime(order.Orderdate).ToString("yyyy-MM-dd")));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", Convert.ToDateTime(order.ShippedDate).ToString("yyyy-MM-dd") == "0001-01-01" ? string.Empty: Convert.ToDateTime(order.ShippedDate).ToString("yyyy-MM-dd")));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", Convert.ToDateTime(order.RequiredDate).ToString("yyyy-MM-dd") == "0001-01-01" ? string.Empty:Convert.ToDateTime(order.RequiredDate).ToString("yyyy-MM-dd")));

                SqlDataAdapter searchOrder = new SqlDataAdapter(cmd);
                searchOrder.Fill(dt);
                conn.Close();

            }
            return this.MapOrderDataToList(dt);
        }




        public List<Models.OrderViewModel> GetOrderById(string OrderId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT
            A.OrderID,A.CustomerID,B.CompanyName AS CustName,A.EmployeeID,C.LastName + C.FirstName AS EmpName,
            A.OrderDate,A.RequiredDate,A.ShippedDate,A.ShipperID,D.CompanyName AS ShiperName,
	        A.Freight,A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry

            FROM Sales.Orders AS A INNER JOIN Sales.Customers AS B ON A.CustomerID = B.CustomerID
            INNER JOIN HR.Employees AS C ON A.EmployeeID = C.EmployeeID
            INNER JOIN Sales.Shippers AS D ON A.ShipperID = D.ShipperID

            WHERE A.OrderID = @OrderId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", OrderId));
                SqlDataAdapter allOrder = new SqlDataAdapter(cmd);
                allOrder.Fill(dt);
                conn.Close();
            }
            return this.MapOrderDataToList(dt);
        }



        public List<Models.OrderDetailViewModel> GetOrderDetailById(string OrderId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT  OrderId, B.ProductId, ProductName, convert(float, A.UnitPrice, 1) as UnitPrice, Qty
                         FROM [Sales].[OrderDetails] A JOIN [Production].[Products] as B ON A.ProductId = B.ProductId
                         WHERE OrderID = @OrderId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", OrderId));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapOrderDetailDataToList(dt);
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















        public List<Models.OrderViewModel> GetCustName()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT CustomerID,CompanyName AS CustName  FROM Sales.Customers ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter allOrder = new SqlDataAdapter(cmd);
                allOrder.Fill(dt);
                conn.Close();

            }
            List<Models.OrderViewModel> result = new List<OrderViewModel>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new OrderViewModel()
                {
                    CustId = row["CustomerID"].ToString(),
                    CustName = row["CustName"].ToString(),
                });
            }
            return result;
        }
        public List<Models.OrderViewModel> GetEmpName()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT EmployeeID,LastName + FirstName AS EmpName  FROM HR.Employees ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter allOrder = new SqlDataAdapter(cmd);
                allOrder.Fill(dt);
                conn.Close();

            }
            List<Models.OrderViewModel> result = new List<OrderViewModel>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new OrderViewModel()
                {
                    EmpId = (int)row["EmployeeID"],
                    EmpName = row["EmpName"].ToString(),
                });
            }
            return result;
        }
        public List<Models.OrderViewModel> GetShiperName()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ShipperID,CompanyName AS ShiperName FROM Sales.Shippers ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter allOrder = new SqlDataAdapter(cmd);
                allOrder.Fill(dt);
                conn.Close();

            }
            List<Models.OrderViewModel> result = new List<OrderViewModel>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new OrderViewModel()
                {
                    ShipperId = (int)row["ShipperID"],
                    ShiperName = row["ShiperName"].ToString(),
                });
            }
            return result;
        }

    }

   

}