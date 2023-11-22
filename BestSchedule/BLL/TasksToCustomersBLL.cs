using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class TasksToCustomersBLL
    {
        TasksToCustomersDAL TasksToCustomersDAL = new TasksToCustomersDAL();
        public List<TasksToCustomers> GetTasksToCustomers()
        {
            return TasksToCustomersDAL.GetTasksToCustomers();
        }
        public int Add(TasksToCustomers t)
        {
            new TasksToCustomersDAL().Add(t);
            return new TasksToCustomersDAL().GetTasksToCustomers().Max(x => x.IdTasksToCustomers);
        }

    }
}
