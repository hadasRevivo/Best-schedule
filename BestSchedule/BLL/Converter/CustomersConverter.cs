using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL.Converter
{
    public class CustomersConverter
    {
        //מקבל אוביקט מסוג מחלקה רגילה ומחזיר את האוביקט מסוג מחלקה למשתמש  DTO
        public static CustomersDTO ConverToCustomersDTO(customers customers)
        {
            CustomersDTO c = new CustomersDTO();
            c.IdCustomers = customers.IdCustomers;
            c.NameCustomers = customers.NameCustomers;
            c.EmailCustomers = customers.EmailCustomers;
            c.AddressCustomers = customers.AddressCustomers;
            c.Pass = customers.Pass;
            return c;
        }
        //מקבל אוביקט מסוג מחלקה של המשתמש ומחזיר את האוביקט מסוג מחלקה רגילה
        public static customers ConverToCustomers(CustomersDTO customersDTO)
        {
            customers c = new customers();
            c.IdCustomers = customersDTO.IdCustomers;
            c.NameCustomers = customersDTO.NameCustomers;
            c.EmailCustomers = customersDTO.EmailCustomers;
            c.AddressCustomers = customersDTO.AddressCustomers;
            c.Pass = customersDTO.Pass;
            return c;
        }
        //מקבל רשימה מסוג מחלקה רגילה ומחזיר רשימה מסוג מחלקה למשתמש
        public static List<CustomersDTO> ConverToCustomersDTO(List<customers> lCustomers)
        {
            return lCustomers.Select(a => ConverToCustomersDTO(a)).ToList();
        }
        //מקבל רשימה מסוג מחלקה של משתמש ומחזיר רשימה מסוג מחלקה רגילה
        public static List<customers> ConverToCustomers(List<CustomersDTO> lCustomersDTO)
        {
            return lCustomersDTO.Select(a => ConverToCustomers(a)).ToList();
        }
    }
}
