using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ShoppingDAL
    {
        private string _connectionString = "server=.;integrated security=true;database=CDB";
        DataSet ds = new DataSet();
        public int RegisterUser(string name, string username, string password, string confirmpassword, string mobile)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Registration WHERE Username=@Username"; 
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                //SqlCommandBuilder builder = new SqlCommandBuilder(da);
                //DataSet ds = new DataSet();
                da.Fill(ds, "Registration");
                DataRow newRow = ds.Tables["Registration"].NewRow();
                newRow["Name"] = name;
                newRow["Username"] = username;
                newRow["Password"] = password;
                newRow["ConfirmPassword"] = confirmpassword;
                newRow["MobileNumber"] = mobile;

                ds.Tables["Registration"].Rows.Add(newRow);

                return da.Update(ds, "Registration");
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
                string query = "SELECT * FROM Registration WHERE Username = @Username AND Password = @Password";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Username", username);
                da.SelectCommand.Parameters.AddWithValue("@Password", password);

                //DataSet ds = new DataSet();
                da.Fill(ds, "Registration");

                return ds.Tables["Registration"].Rows.Count > 0;
            }
        }

    }
}
