using Dapper;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace POS.DAL
{
    public static class DatabaseHelper
    {
        private static string connectionString = @"Server=localhost\SQLEXPRESS;Database=SalesDB;Trusted_Connection=True";

        public static List<Customer> GetCustomers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Customers";
                return connection.Query<Customer>(query).ToList();
            }
        }

        public static List<Order> GetOrders(int customerId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Orders WHERE CustomerID = @CustomerID";
                return connection.Query<Order>(query, new { CustomerID = customerId }).ToList();
            }
        }

        public static List<Product> GetProducts(int orderId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Products WHERE OrderID = @OrderID";
                return connection.Query<Product>(query, new { OrderID = orderId }).ToList();
            }
        }
    }
}
