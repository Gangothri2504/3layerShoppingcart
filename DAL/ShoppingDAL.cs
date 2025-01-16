using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class ShoppingDAL
    {
        private string _connectionString = "server=.;integrated security=true;database=CDB";
        //DataSet ds = new DataSet();
        public int RegisterUser(string name, string username, string password, string confirmpassword, string mobile)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "select * from Registration"; 
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow newRow = dt.NewRow();
                newRow["Name"] = name;
                newRow["Username"] = username;
                newRow["Password"] = password;
                newRow["ConfirmPassword"] = confirmpassword;
                newRow["MobileNumber"] = mobile;
                DataRow[] existingUsers = dt.Select($"Username = '{username}'");
                if (existingUsers.Length > 0)
                {
                    Console.WriteLine("User already exists. Please try a different username.");
                }

                //int result = 0;
                //int a = Convert.ToInt32(dt.Rows["Username"]);
                //if (a.Equals(username))
                //{
                //    result = 1;
                //}
                dt.Rows.Add(newRow);
                return da.Update(dt);
                //return result;
                    
                //if (ds.HasChanges())
                //{
                //    da.Update(ds.Tables["Employee"]);
                //    Console.WriteLine("Record(s) updated on DB");
                //}
            }

        }
        public bool ValidateLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "select * from Registration where Username = @Username and Password = @Password";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Username", username);
                da.SelectCommand.Parameters.AddWithValue("@Password", password);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }
        public DataTable GetProducts()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "select * from Products";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }
        public DataRow GetProductById(int productId) 
        { 
            DataTable dt = new DataTable(); 
            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            { 
                string query = "SELECT * FROM Products WHERE ProductID = @ProductId"; 
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@ProductId", productId); 
                adapter.Fill(dt); 
            }
            if (dt.Rows.Count > 0) { 
                return dt.Rows[0]; 
            } 
            return null; 
        }
        public DataTable GetProductPrice(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * from Products WHERE ProductID = @ProductID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@ProductID", productId);
                DataTable productTable = new DataTable();
                adapter.Fill(productTable);
                return productTable;
            }
        }
        //public int AddToCart()
        
        //{

        //}


    }
}
