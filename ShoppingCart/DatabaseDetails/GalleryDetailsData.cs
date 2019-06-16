using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart.Models;
using System.Data.SqlClient;

namespace ShoppingCart.DatabaseDetails
{
    public class GalleryDetailsData
    {
        public static List<GalleryDetails> GetGalleryProductDetails(string SessionId)
        {
            List<GalleryDetails> products = new List<GalleryDetails>();
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"select ProductId,ProductName,Description,Price,ImageUrl from ProductDetails";
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader re = com.ExecuteReader();
                 while(re.Read())
                { 
                   GalleryDetails product = new GalleryDetails();
                product.ProductId = (int)re["ProductId"];
                product.ProductName = (string)re["ProductName"];
                product.Description = (string)re["Description"];
                product.Price = (int)re["Price"];
                product.ImageUrl = (string)re["ImageUrl"];

                products.Add(product);
                }

            }
            return products;
        }

        public static List<GalleryDetails> GetSearchProductDetails(string Productname)
        {
            List<GalleryDetails> products = new List<GalleryDetails>();
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"select ProductId,ProductName,Description,Price,ImageUrl from ProductDetails where ProductName like '%"+ Productname +"%' or Description like '%"+ Productname + "%'";
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader re = com.ExecuteReader();
                while (re.Read())
                {
                    GalleryDetails product = new GalleryDetails();
                    product.ProductId = (int)re["ProductId"]; 
                    product.ProductName = (string)re["ProductName"];
                    product.Description = (string)re["Description"];
                    product.Price = (int)re["Price"];
                    product.ImageUrl = (string)re["ImageUrl"];

                    products.Add(product);
                }

            }
            return products;
        }

        public static List<OrderDetails> AddtoMyPurchases(string sessionId, int UserId)
        {
            List<OrderDetails> list = new List<OrderDetails>();
            GalleryDetails Gallery = null;

            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
               "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"SELECT p.ImageUrl,p.ProductName,p.Description,p.Price,o.PurchasedOn,o.Quantity FROM ProductDetails p,OrderDetailsNew o WHERE o.ProductId=p.ProductId and o.UserId='" + UserId + "'";
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

    }
}