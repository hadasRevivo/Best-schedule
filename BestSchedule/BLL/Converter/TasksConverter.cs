using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL.Converter
{
   public class TasksConverter
    {
        //מקבל אוביקט מסוג מחלקה רגילה ומחזיר את האוביקט מסוג מחלקה למשתמש  DTO
        public static TasksDTO ConverToTasksDTO(Tasks tasks)
        {
            TasksDTO t = new TasksDTO();
            t.IdTasks = tasks.IdTasks;
            t.NameTasks = tasks.NameTasks;
            t.TaskLength = tasks.TaskLength;
            t.AddressTasks = tasks.AddressTasks.Trim();
            return t;
        }
        //מקבל אוביקט מסוג מחלקה של המשתמש ומחזיר את האוביקט מסוג מחלקה רגילה
        public static Tasks ConverToTasks(TasksDTO tasksDTO)
        {
            Tasks t = new Tasks();
            t.IdTasks = tasksDTO.IdTasks;
            t.NameTasks = tasksDTO.NameTasks;
            t.TaskLength = tasksDTO.TaskLength;
            t.AddressTasks = tasksDTO.AddressTasks;
            return t;
        }
        //מקבל רשימה מסוג מחלקה רגילה ומחזיר רשימה מסוג מחלקה למשתמש
        public static List<TasksDTO> ConverToTasksDTO(List<Tasks> lTasks)
        {
            return lTasks.Select(a => ConverToTasksDTO(a)).ToList();
        }
        //מקבל רשימה מסוג מחלקה של משתמש ומחזיר רשימה מסוג מחלקה רגילה
        public static List<Tasks> ConverToTasks(List<TasksDTO> lTasksDTO)
        {
            return lTasksDTO.Select(a => ConverToTasks(a)).ToList();
        }
        public static Tasks [] ConverToTasks(TasksDTO [] lTasksDTO)
        {
            return lTasksDTO.Select(a => ConverToTasks(a)).ToArray();
        }
    }
}
