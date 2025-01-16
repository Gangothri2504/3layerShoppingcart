using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BAL;

namespace UI
{
    class Program
    {
        static ShoppingBAL shoppingCartBLL = new ShoppingBAL();
        static void Register()
        {
            Console.WriteLine("Register");
            Console.WriteLine("Enter Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter UserName");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();
            confirmpassword:
            Console.WriteLine("Enter ConfirmPassword");
            string confirmpassword = Console.ReadLine();
            if (password != confirmpassword)
            {
                Console.WriteLine("Password and Confirm Password do not match.");
                goto confirmpassword;

            }
            Console.WriteLine("Enter Mobile Number");
            string mobile = Console.ReadLine();
            try
            {
                shoppingCartBLL.RegisterUser(name, username, password, confirmpassword, mobile);
                //if()
                Console.WriteLine("Registration Success.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static string Login()
        {
            Console.WriteLine("Enter Username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();
            if (shoppingCartBLL.ValidateLogin(username, password))
            {
                Console.WriteLine("Login Successful");
                return username;
            }
            else
            {
                Console.WriteLine("Login Failed,Please try again.");
                Login();
                return null;               
            }
        }
        static DataTable displayproduct()
        {
            Console.WriteLine("Products List:");
            DataTable products = shoppingCartBLL.GetProducts();
            Console.WriteLine("ProductId\tProductName\tPrice\tQuantity");

            foreach (DataRow row in products.Rows)
            {
                Console.WriteLine($"{row["ProductID"]}\t\t{row["ProductName"]}\t\t{row["Price"]}\t\t{row["Quantity"]}");
            }
            return products;
        }
        
        public void AddToCart()
        {
            DataTable cartTable = new DataTable();
            cartTable.Columns.Add("ProductID", typeof(int));
            cartTable.Columns.Add("ProductName", typeof(string));
            Console.WriteLine("Enter Product ID:");
            int productId = int.Parse(Console.ReadLine());
            DataRow productRow = shoppingCartBLL.GetProductById(productId);
            if (productRow != null)
            {
                cartTable.ImportRow(productRow);
            }
            foreach (DataRow row in cartTable.Rows)
            {
                Console.WriteLine($"Product ID: {row["ProductID"]}, Product Name: {row["ProductName"]}");
            }
        }
        public static int Productprice()
        {
            int totalcost = 0;
            while (true)
            {
                Console.Write("Enter ProductID to add to cart: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Quantity: ");
                int qty = Convert.ToInt32(Console.ReadLine());
                DataTable pricecal = shoppingCartBLL.GetProductPrice(productId);
                if (pricecal.Rows.Count > 0)
                {
                    int price = Convert.ToInt32(pricecal.Rows[0]["Price"]);
                    totalcost = price * qty;
                    int quant=Convert.ToInt32(pricecal.Rows[0]["Quantity"]);
                    if (qty > quant)
                    {
                        Console.WriteLine("Quantity unavailable.");
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
                Console.WriteLine("Do you want to add more items?(Yes/no)");
                string additems = Console.ReadLine().ToLower().Trim();
                if (additems.Equals("no"))
                {
                    break;
                }
            }
            //totalcost = price * qty;
            return totalcost;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Do you have credentials to Login? (Yes/No): ");
            Operator:
            string YesNo = Console.ReadLine().ToLower().Trim();
            if(!YesNo.Equals("yes") && !YesNo.Equals("no"))
            {
                Console.WriteLine("Please enter yes/no");
                goto Operator;

            }
            if (YesNo.Equals("no"))
            {
                Register();
            }
            string username = Login();
            DataTable products = displayproduct();
            int totalprice = Productprice();

            //if (username != null)
            //{
            //    ShoppingCart(username);
            //}
            //else
            //{
            //    Console.WriteLine("Invalid login.\nPlease Enter Valid Credentials.");
            //}

            Console.ReadLine();

        }
    }
}
