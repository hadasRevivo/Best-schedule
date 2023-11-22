using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BLL
{
  public  class LoginBLL
    {
        CustomerBLL customerBLL = new CustomerBLL();
        public CustomersDTO Login(string email,string pass)
        {
           return  customerBLL.GetCustomersDTO().Find(a => a.EmailCustomers.Trim() == email && a.Pass.Trim() == pass);
        }
    }
}
