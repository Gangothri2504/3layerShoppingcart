using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BAL;
using cart;

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
        static void AddToCart(List<CartItem> cart)
        {
            DataTable products= shoppingCartBLL.GetProducts(); 
            while (true)
            {
                Console.Write("Enter Product ID to add to cart: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Quantity: ");
                int qty = Convert.ToInt32(Console.ReadLine());
                
                List<DataRow> productDetails = shoppingCartBLL.GetProductById(productId);

                if (productDetails != null && productDetails.Count > 0)
                {
                    DataRow product = productDetails[0];
                    int availableQty = Convert.ToInt32(product["Quantity"]);
                    int price = Convert.ToInt32(product["Price"]);

                    if (qty > availableQty)
                    {
                        Console.WriteLine("Requested quantity exceeds available stock.");
                    }
                    else
                    {
                        cart.Add(new CartItem
                        {
                            ProductId = productId,
                            ProductName = product["ProductName"].ToString(),
                            Quantity = qty,
                            Price = price,
                            TotalCost = price * qty
                        });
                        //product["Quantity"] = availableQty - qty;
                        //newQuantity = availableQty - qty;
                        //shoppingCartBLL.UpdateProductQuantity(productId, newQuantity);

                        int newQuantity = availableQty - qty;
                        bool isUpdated = shoppingCartBLL.UpdateProductQuantity(products, productId, newQuantity);
                        if (isUpdated)
                        {
                            Console.WriteLine($"Added {qty} {product["ProductName"]} to your cart.");
                            products = shoppingCartBLL.GetProducts();
                            Console.Write("Do you want to add more products? (Yes/No): ");
                            string additems = Console.ReadLine().ToLower().Trim();

                            if (additems.Equals("no"))
                            {
                                Console.WriteLine("Thank you for shopping with us!");
                                break;
                            }
                            else if (additems.Equals("yes"))
                            {
                                Console.WriteLine("Product List:");
                                Console.WriteLine("ProductId\tProductname\tPrice\tQuantity");
                                foreach (DataRow row in products.Rows)
                                {
                                    Console.WriteLine($"{row["ProductId"]}\t\t{row["ProductName"]}\t\t{row["Price"]}\t\t{row["Quantity"]}");
                                }
                            }
                        }

                        //Console.WriteLine($"{qty} x {product["ProductName"]} added to the cart.");
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
                //Console.WriteLine("Do you want to add more items? (Yes/No): ");
                //string additems = Console.ReadLine().ToLower().Trim();
                //if (additems.Equals("no"))
                //{
                //    break;
                //}
                //else if (additems.Equals("yes"))
                //{
                //    Console.WriteLine("Product List:");
                //    Console.WriteLine("ProductId\tProductname\tPrice\tQuantity");
                //    foreach (DataRow row in products.Rows)
                //    {
                //        Console.WriteLine($"{row["ProductId"]}\t\t{row["ProductName"]}\t\t{row["Price"]}\t\t{row["Quantity"]}");
                //    }
                //}
            }
        }
        static void DisplayCart(List<CartItem> cart)
        {
            Console.WriteLine("\nCart Summary:");
            Console.WriteLine("Product ID\tProduct Name\tQuantity\tPrice\tTotal Cost");

            int Totalprice = 0;
            foreach (var item in cart)
            {
                Console.WriteLine($"{item.ProductId}\t\t{item.ProductName}\t\t{item.Quantity}\t\t{item.Price}\t\t{item.TotalCost}");
                Totalprice += item.TotalCost;
            }
            Console.WriteLine("Thank you for shopping with us!");
            Console.WriteLine($"Total Price: {Totalprice}");
        }


        //public static int Productprice()
        //{
        //    int totalcost = 0;
        //    while (true)
        //    {
        //        Console.Write("Enter ProductID to add to cart: ");
        //        int productId = Convert.ToInt32(Console.ReadLine());
        //        Console.Write("Enter Quantity: ");
        //        int qty = Convert.ToInt32(Console.ReadLine());
        //        DataTable pricecal = shoppingCartBLL.GetProductPrice(productId);
        //        if (pricecal.Rows.Count > 0)
        //        {
        //            int price = Convert.ToInt32(pricecal.Rows[0]["Price"]);
        //            totalcost = price * qty;
        //            int quant=Convert.ToInt32(pricecal.Rows[0]["Quantity"]);
        //            if (qty > quant)
        //            {
        //                Console.WriteLine("Quantity unavailable.");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Product not found.");
        //        }
        //        Console.WriteLine("Do you want to add more items?(Yes/no)");
        //        string additems = Console.ReadLine().ToLower().Trim();
        //        if (additems.Equals("no"))
        //        {
        //            break;
        //        }
        //    }
        //    //totalcost = price * qty;
        //    return totalcost;
        //}


        static void Main(string[] args)
        {
            Console.WriteLine("Do you have credentials to Login? (Yes/No): ");
            Operator:
            string YesNo = Console.ReadLine().ToLower().Trim();
            if (!YesNo.Equals("yes") && !YesNo.Equals("no"))
            {
                Console.WriteLine("Please enter yes/no");
                goto Operator;

            }
            if (YesNo.Equals("no"))
            {
                Register();
            }
            string username = Login();
            if (username != null)
            {
                displayproduct();
                List<CartItem> cart = new List<CartItem>();
                AddToCart(cart);
                DisplayCart(cart);
            }
            else
            {
                Console.WriteLine("Invalid login. Please restart the application.");
            }
            //DataTable products = displayproduct();
            //int totalprice = Productprice();
            //AddToCart();

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
