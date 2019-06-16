using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ShoppingCart.DatabaseDetails
{
    public class UserData
    {
        public static User GetUserDetails(string UserName)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"select UserId,UserName,Password from UserDetailsNew where UserName = '"+ UserName +"'";
                SqlCommand com = new SqlCommand(sql,conn);
                SqlDataReader re = com.ExecuteReader();
                if (re.Read())
                    user = new User();
                user.UserId = (int)re["UserId"];
                user.Password = (string)re["Password"];
                
            }
            return user;
        }

        public static User GetUserDetailsBySessionId(string sessionId)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"select UserId,FullName,SessionId from UserDetailsNew where SessionId = '" + sessionId + "'";
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader re = com.ExecuteReader();
                if (re.Read())
                    user = new User();
                
                user.FullName = (string)re["FullName"];
                user.SessionId = (string)re["SessionId"];
                user.UserId = (int)re["UserId"];

            }
            return user;
        }

        public static string CreateSession(int UserId)
        {
            string sessionId = Guid.NewGuid().ToString();

            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"UPDATE UserDetailsNew SET SessionId = '" + sessionId + "'" +
                        " WHERE UserId =" + UserId ;
                SqlCommand com = new SqlCommand(sql,conn);
                com.ExecuteNonQuery();
            }

            return sessionId;
        }

        public static void RemoveSession(int UserId)
        {
            using (SqlConnection conn = new SqlConnection("Server = LOGU;" +
                "Database=ShoppingCartCA; Integrated Security=true"))
            {
                conn.Open();
                string sql = @"UPDATE UserDetailsNew SET SessionId = NULL where UserId= " + UserId;
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}