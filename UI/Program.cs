using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("Enter ConfirmPassword");
            string confirmpassword = Console.ReadLine();
            if (password != confirmpassword)
            {
                Console.WriteLine("Password and Confirm Password do not match.");

            }
            Console.WriteLine("Enter Mobile Number");
            string mobile = Console.ReadLine();
            try
            {
                shoppingCartBLL.RegisterUser(name, username, password, confirmpassword, mobile);
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
