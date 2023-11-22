using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  
    public class CustomerDAL
    {
        BestScheduleEntities DB = new BestScheduleEntities();
        //הוספה עידכון מחיקה , GET ALL ,GET BY PRIMARY KEY
        public List<customers> GetCustomers()
        {
            return DB.customers.ToList();
        }
        public customers GetCustomer(string id)
        {
            return DB.customers.Find(id);
        }
        public bool Add(customers c)
        {
            try
            {
                c.IdCustomers = DB.customers.Count() + 1.ToString();             
                DB.customers.Add(c);
                DB.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Update(customers c)
        {
            try
            {
                DB.customers.Find(c.IdCustomers).AddressCustomers = c.AddressCustomers.Trim();
                DB.customers.Find(c.IdCustomers).NameCustomers = c.NameCustomers.Trim();
                DB.customers.Find(c.IdCustomers).EmailCustomers = c.EmailCustomers.Trim();
                DB.customers.Find(c.IdCustomers).Pass = c.Pass.Trim();

                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(customers c)
        {
            try
            {
                DB.customers.Remove(c);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
