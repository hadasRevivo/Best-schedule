using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
  public  class RouteBLL
    {
        RouteDAL RouteDAL = new RouteDAL();
        TasksBLL TasksBLL = new TasksBLL();
        TasksToCustomersBLL TasksToCustomersBLL = new TasksToCustomersBLL();
        ConstraintsTaskBLL ConstraintsTask = new ConstraintsTaskBLL();
        public DetailsRouteAndTasksDTO currentRoute(string idCustomer)
        {
            Converter.TasksCreatedConverter TasksCreatedConverter = new Converter.TasksCreatedConverter();
            DetailsRouteAndTasksDTO d = new DetailsRouteAndTasksDTO();
            d.route = Converter.RouteConverter.ConverToRouteDTO(RouteDAL.GetRoute().FindAll(a => a.IdCustomers.Trim() == idCustomer && a.StartDateToRoute <= DateTime.Now && a.EndDate > DateTime.Now).OrderBy(x=>x.EndDate).ElementAt(0));
            //TasksToCustomersBLL.GetTasksToCustomers().FindAll(a => a.IdRoute == d.route.IdRoute&&a.tas.).Select(a=>a.IdTasks);
            d.createdTasks= TasksCreatedConverter.ConverToTasksCreatedDTO(TasksBLL.GetTasks().FindAll(a => a.TasksToCustomers.Select(x => x.IdRoute).Contains(d.route.IdRoute)).ToList(), d.route.IdRoute).ToArray();
            return d;

            }
        public bool AddNewRoute(DetailsRouteAndTasksDTO details)
        {
            //מוסיה מסלול שומרת מספר מסלול
            //עוברת על משימות
            //כול משימה בודקת אם קיימת-מוסיפה רק משימה ללקוח
            //אין משימה-אז גם משימה וגם משימה ללקוח

            //לשמור מספר משימה ללקוח שהתווסף
            //בודקת אם יש אילוץ ומוסיפה
            int idroute = AddRout(details.route);
            int idt = 0;
            int idTC;
            details.createdTasks = details.createdTasks.Where(a => a.Task.IdTasks != 0).ToArray();
            try
            {
                foreach (TasksCreatedDTO item in details.createdTasks)
                {
                    idt = LookingTask(item.Task);
                    if (idt == -1)
                    {
                        idt = TasksBLL.Add(item.Task);
                    }
                    //הוספת משימה ללקוח
                    TasksToCustomers t = new TasksToCustomers();
                    t.IdTasks = idt;
                    t.IdCustomers = details.route.IdCustomers;
                    t.IdRoute = idroute;
                    t.Date = item.Constraits.DateToConstraint;
                    idTC = TasksToCustomersBLL.Add(t);
                    //הוספת אילוץ משימה
                    item.Constraits.IdTasksToCustomers = idTC;
                    ConstraintsTask.Add(item.Constraits);
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
         public int AddRout(RouteDTO r)
        {
            
                new RouteDAL().Add(Converter.RouteConverter.ConverToRoute(r));
                return new RouteDAL().GetRoute().Max(x => x.IdRoute);
            //}
            //catch (Exception)
            //{
            //    return -1;
            //}
          
        }
        public int LookingTask(TasksDTO t)
        {
            return TasksBLL.GetTasks().FindAll(a => a.NameTasks.Trim() == t.NameTasks.Trim() && a.AddressTasks.Trim() == t.AddressTasks.Trim() && a.TaskLength == t.TaskLength).Count == 0 ? -1 : TasksBLL.GetTasks().FindAll(a => a.NameTasks.Trim() == t.NameTasks.Trim() && a.AddressTasks.Trim() == t.AddressTasks.Trim() && a.TaskLength == t.TaskLength)[0].IdTasks;
        }
    }
}
