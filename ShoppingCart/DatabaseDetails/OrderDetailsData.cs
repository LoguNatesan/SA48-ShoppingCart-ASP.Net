using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCart.Models;
namespace ShoppingCart.DatabaseDetails
{
    public class OrderDetailsData
    {
        public static List<OrderDetails> AddtoOrderDetails(string sessionId, int UserId)
        {
            List<OrderDetails> list = new List<OrderDetails>();
            GalleryDetails Gallery = null;

            //using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
            //    "Database=ShoppingCartCA; Integrated Security=true"))
            //{
            //    conn.Open();

            //    string sql = @"SELECT UserId,ProductId,Quantity,Price,SessionId from CartDetails where UserId='" + UserId + "' and SessionId='" + sessionId + "' and Quantity>'0'";
            //    SqlCommand com = new SqlCommand(sql, conn);
            //    SqlDataReader re = com.ExecuteReader();
            //    while (re.Read())
            //    {
            //        DateTime today = DateTime.Now;
            //        string date = today.ToString();
            //        sql = @"INSERT INTO OrderDetailsNew VALUES('" + (int)re["UserId"] + "','" + (int)re["ProductId"] + "','" + (int)re["Quantity"] + "','" + (int)re["Price"] + "','" + (string)re["SessionId"] + "','" + date+ "')";
            //        com = new SqlCommand(sql, conn);
            //        com.ExecuteNonQuery();
            //    }
            //}

            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                DateTime today = DateTime.Now;
                string date = today.ToString();
                conn.Open();
                string sql = @"INSERT INTO OrderDetailsNew(UserId,ProductId,Quantity,Price,SessionId,PurchasedOn) select UserId,ProductId,Quantity,Price,SessionId,'" + date + "'" +
                        "FROM CartDetails WHERE SessionId ='" + sessionId + "' and UserId ='" + UserId + "' and Quantity > 0";
                SqlCommand com = new SqlCommand(sql, conn);
                int count = com.ExecuteNonQuery();

            }

            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
               "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"DELETE FROM CartDetails WHERE SessionId ='" + sessionId + "' " +
                    "and UserId ='" + UserId + "'";
                SqlCommand com = new SqlCommand(sql, conn);
                int count = com.ExecuteNonQuery();
            }

            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
               "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"SELECT p.ImageUrl,p.ProductName,p.Description,p.Price,o.PurchasedOn,o.Quantity FROM ProductDetails p,OrderDetailsNew o WHERE o.ProductId=p.ProductId and o.SessionId='" + sessionId + "'and o.UserId='" + UserId+"'";
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader re = com.ExecuteReader();

                while (re.Read())
                {
                    Gallery = new GalleryDetails();
                    OrderDetails orderde = new OrderDetails();
                    Gallery.ImageUrl = (string)re["ImageUrl"];
                    Gallery.ProductName = (string)re["ProductName"];
                    Gallery.Description = (string)re["Description"];
                    Gallery.Price = (int)re["Price"];
                    orderde.gallery = Gallery;
                    orderde.Quantity = (int)re["Quantity"];
                    orderde.PurchasedOn = (string)re["PurchasedOn"];
                    list.Add(orderde);
                }
            }
            return list;
        }

        public static void DeleteFromCart()
        {
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
               "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"DELETE FROM CartDetails";
                SqlCommand com = new SqlCommand(sql, conn);
                int count = com.ExecuteNonQuery();

            }
        }

        public static void RemoveSessionInOrder(int UserId)
        {
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"UPDATE OrderDetailsNew SET SessionId = NULL where UserId= " + UserId;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}