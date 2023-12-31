using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SQLFunction
{
    public static class GetProduct
    {
        [FunctionName("GetProducts")]
        public static IActionResult RunProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("Get data from the database");
                List<Product> _product_lst = new List<Product>();
                string _statement = "SELECT ProductId,ProductName,Quantity from Product";
                SqlConnection _connection = GetConnection();

                _connection.Open();

                SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

                using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
                {
                    while (_reader.Read())
                    {
                        Product _product = new Product()
                        {
                            ProductId = _reader.GetInt32(0),
                            ProductName = _reader.GetString(1),
                            Quantity = _reader.GetInt32(2)
                        };

                        _product_lst.Add(_product);
                    }
                }
                _connection.Close();

                return new OkObjectResult(_product_lst);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        private static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=AZ204;Integrated Security=True";
            return new SqlConnection(connectionString);
        }

        [FunctionName("GetProduct")]
        public static IActionResult RunProduct(
           [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
           ILogger log)
        {


            int ProductID = int.Parse(req.Query["Id"]);

            string _statement = String.Format("SELECT ProductID,ProductName,Quantity from Products WHERE ProductID={0}", ProductID);
            SqlConnection _connection = GetConnection();

            _connection.Open();

            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            Product _product = new Product();

            try
            {
                using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
                {
                    _reader.Read();
                    _product.ProductId = _reader.GetInt32(0);
                    _product.ProductName = _reader.GetString(1);
                    _product.Quantity = _reader.GetInt32(2);
                    var response = _product;

                    _connection.Close();

                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                var response = "No Records found";
                return new OkObjectResult(response);
            }
            


        }
    }


}
