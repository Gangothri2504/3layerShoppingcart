using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public class ShoppingBAL
    {
        private ShoppingDAL _shoppingCartDAL;
        public ShoppingBAL()
        {
            _shoppingCartDAL = new ShoppingDAL();
        }

        // Method to Register User
        public int RegisterUser(string name, string username, string password, string confirmpassword, string mobile)
        {
            return _shoppingCartDAL.RegisterUser(name, username, password, confirmpassword, mobile);
        }
        public bool ValidateLogin(string username, string password)
        {
            return _shoppingCartDAL.ValidateLogin(username, password);
        }



    }
}
