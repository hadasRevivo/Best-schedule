using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class TasksToCustomersDTO
    {
        public int IdTasksToCustomers { get; set; }
        public int IdTasks { get; set; }
        public string IdCustomers { get; set; }
        public int IdRoute { get; set; }
        public System.DateTime Date { get; set; }
    }
}
