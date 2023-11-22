using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class CustomerBLL
    {
        CustomerDAL CustomerDAL = new CustomerDAL();
        TasksToCustomersDAL tasksToCustomersDAL = new TasksToCustomersDAL();
        RouteDAL RouteDAL = new RouteDAL();
        public List<CustomersDTO> GetCustomersDTO()
        {
            return Converter.CustomersConverter.ConverToCustomersDTO(CustomerDAL.GetCustomers());
        }
        public bool UpdateAccount(CustomersDTO c)
        {
          return  CustomerDAL.Update(Converter.CustomersConverter.ConverToCustomers(c));
        }

        //לפי הצורך ןירטואלי
        public List<customers> GetCustomers()
        {
            return (CustomerDAL.GetCustomers());
        }
        public CustomersDTO GetCustomer(string id)
        {
            return Converter.CustomersConverter.ConverToCustomersDTO(CustomerDAL.GetCustomer(id));
        }
        public bool Add(CustomersDTO c)
        {
            return CustomerDAL.Add(Converter.CustomersConverter.ConverToCustomers(c));
        }
        //public bool Update(CustomersDTO c)
        //{
        //    return CustomerDAL.Update(Converter.CustomersConverter.ConverToCustomers(c));

        //}
        //public bool Delete(CustomersDTO c)
        //{
        //    return CustomerDAL.Delete(Converter.CustomersConverter.ConverToCustomers(c));

        //}
        //להחזיר למשתמש את משימותיו הקודמות
        public List<TasksCreatedDTO> PreviousTasksPerCustomer(string idCustomer)
        {
            return Converter.TasksCreatedConverter.ConverToTasksCreatedDTO((tasksToCustomersDAL.GetTasksToCustomers().FindAll(a => a.IdCustomers ==idCustomer)).ToList());
        }
        public List<TasksCreatedDTO> PreviousTasksPerThisRoute(string idCustomer, int idRoute)
        {
            return Converter.TasksCreatedConverter.ConverToTasksCreatedDTO((tasksToCustomersDAL.GetTasksToCustomers().FindAll(a => a.IdCustomers == idCustomer&& a.IdRoute==idRoute)).ToList());
        }
        
        //להחזיר למשתמש את כל המסלולים הקודמים שלו
        public List<RoutsPerCustomerDTO> previousRoutesPerCustomer(string id)
        {
            return Converter.RouteConverter.ConverRoutsPerCustomerDTO((RouteDAL.GetRoute().FindAll(a => a.IdCustomers == id)).ToList());
        }
        //public List<RoutsPerCustomerDTO> currentRoute(string id)
        //{
        //    return Converter.RouteConverter.ConverRoutsPerCustomerDTO((RouteDAL.GetRoute().FindAll(a => a.IdCustomers == id)).ToList());
        //}
        public DetailsRouteAndTasksDTO alldataCustomerToConvert(AllDataForRouteAndTaskfromClientDTO all)
        {
            TaskInRouteAlgoritm a = new TaskInRouteAlgoritm(
            Converter.RouteConverter.ConverToRoute(all.route),Converter.TasksConverter.ConverToTasks(all.Tasks), Converter.ConstraintsTaskConverter.ConverToConstraintsTask(all.ConstraintsTask.ToList()), all.goBackHome,all.HoursNotActivityofs);
            return a.result;
        }

    }
}
