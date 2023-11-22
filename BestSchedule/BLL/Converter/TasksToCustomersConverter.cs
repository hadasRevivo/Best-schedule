using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL.Converter
{
    class TasksToCustomersConverter
    {
        //מקבל אוביקט מסוג מחלקה רגילה ומחזיר את האוביקט מסוג מחלקה למשתמש  DTO
        public static TasksToCustomersDTO ConverToTasksToCustomersDTO(TasksToCustomers tsksToCustomers)
        {
            TasksToCustomersDTO t = new TasksToCustomersDTO();
            t.IdTasksToCustomers = tsksToCustomers.IdTasksToCustomers;
            t.IdTasks = tsksToCustomers.IdTasks;
            t.IdCustomers = tsksToCustomers.IdCustomers;
            t.IdRoute = tsksToCustomers.IdRoute;
            t.Date = tsksToCustomers.Date;
            return t;
        }
        //מקבל אוביקט מסוג מחלקה של המשתמש ומחזיר את האוביקט מסוג מחלקה רגילה
        public static TasksToCustomers ConverToTasksToCustomers(TasksToCustomersDTO tasksToCustomersDTO)
        {
            TasksToCustomers t = new TasksToCustomers();
            t.IdTasksToCustomers = tasksToCustomersDTO.IdTasksToCustomers;
            t.IdTasks = tasksToCustomersDTO.IdTasks;
            t.IdCustomers = tasksToCustomersDTO.IdCustomers;
            t.IdRoute = tasksToCustomersDTO.IdRoute;
            t.Date = tasksToCustomersDTO.Date;

            return t;
        }
        //מקבל רשימה מסוג מחלקה רגילה ומחזיר רשימה מסוג מחלקה למשתמש
        public static List<TasksToCustomersDTO> ConverToTasksToCustomersDTO(List<TasksToCustomers> lTasksToCustomers)
        {
            return lTasksToCustomers.Select(a => ConverToTasksToCustomersDTO(a)).ToList();
        }
        //מקבל רשימה מסוג מחלקה של משתמש ומחזיר רשימה מסוג מחלקה רגילה
        public static List<TasksToCustomers> ConverToTasksToCustomers(List<TasksToCustomersDTO> lTasksToCustomersDTO)
        {
            return lTasksToCustomersDTO.Select(a => ConverToTasksToCustomers(a)).ToList();
        }


    }
}
