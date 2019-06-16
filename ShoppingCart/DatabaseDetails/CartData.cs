using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.DatabaseDetails
{
    public class CartData
    {
        public static void AddtoCartDetails(CartDetails cart)
        {
            bool x= false;
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"SELECT Quantity from CartDetails WHERE UserId =" + cart.UserId +"and ProductId="+cart.ProductId;
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader re = com.ExecuteReader();

                if (!re.Read())
                {
                    x = true;
                }
                Debug.WriteLine(re.Read());
            }
            if(x==true)
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
                {
                    conn.Open();
                    string sql = @"INSERT INTO CartDetails VALUES('" + cart.UserId + "','" + cart.ProductId + "',1,'" + cart.Price + "','" + cart.SessionId+"')";
                    SqlCommand com = new SqlCommand(sql, conn);
                    int row = com.ExecuteNonQuery();
                }
        }

        public static void UpdateCartDetails(CartDetails cart)
        {
            //List<CartDetails> list = new List<CartDetails>();
            //CartDetails cart = null;
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"UPDATE CartDetails SET Quantity= '" + cart.Quantity + "',Price= '" + cart.Quantity * cart.Price + "' WHERE SessionId ='" + cart.SessionId + "' and ProductId ='" + cart.ProductId + "'";
                SqlCommand com = new SqlCommand(sql, conn);
                int count = com.ExecuteNonQuery();
            }

            //using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
            //    "Database=ShoppingCartCA; Integrated Security=true"))
            //{
            //    conn.Open();
            //    string sql = @"Select Price,Quantity from CartDetails where ProductId='" + cart.ProductId + "' and SessionId ='" + cart.SessionId + "'";
            //    SqlCommand com = new SqlCommand(sql, conn);
            //}

        }
        public static List<CartDetails> AddtoViewCart(string sessionId,int UserId)
        {
            List<CartDetails> list = new List<CartDetails>();
            GalleryDetails gallery = null;
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"SELECT p.ImageUrl,p.ProductName,p.Description,p.Price,p.ProductId,c.Price as CartPrice,c.Quantity from ProductDetails p,CartDetails c WHERE p.ProductId=c.ProductId and c.SessionId ='"+ sessionId +"' and c.UserId='" + UserId + "'";
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader re = com.ExecuteReader();

                while (re.Read())
                {
                    gallery = new GalleryDetails();
                    gallery.ImageUrl = (string)re["ImageUrl"];
                    gallery.ProductName = (string)re["ProductName"];
                    gallery.Description = (string)re["Description"];
                    gallery.ProductId = (int)re["ProductId"];
                    gallery.Price = (int)re["Price"];
                    CartDetails cart = new CartDetails();
                    cart.Gallery = gallery;
                    cart.Price = (int)re["CartPrice"];
                    cart.Quantity = (int)re["Quantity"];
                    list.Add(cart);
                }
                return list;
            }

        }

        public static int GetCartCount(string SessionId, int UserId)
        {
            int count=0;

            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"SELECT COUNT(Quantity) from CartDetails WHERE SessionID='" + SessionId + "'";
                SqlCommand com = new SqlCommand(sql, conn);
                count = (int)com.ExecuteScalar();
                //SqlDataReader re = com.ExecuteReader();
                //if (!re.Read())
                //    count = 0;
                //else
                //    count = (int)re["quantity"];
            }
           
            return count;
        }

    //    public static int GetProductQuantity(string SessionId)
    //    {
    //        int count = 0;

    //        using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
    //            "Database=ShoppingCartCA; Integrated Security=true"))
    //        {
    //            conn.Open();
    //            string sql = @"SELECT COUNT(SessionId) from CartDetails WHERE SessionID='" + SessionId + "'";
    //            SqlCommand com = new SqlCommand(sql, conn);
    //            count = (int)com.ExecuteScalar();
    //        }
    //        Debug.WriteLine(count);
    //        return count;
    //    }
    }

    
}