using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL.Converter
{
   public class ConstraintsTaskConverter
    {
        //מקבל אוביקט מסוג מחלקה רגילה ומחזיר את האוביקט מסוג מחלקה למשתמש  DTO
        public static ConstraintsTaskDTO ConverToConstraintsTaskDTO(ConstraintsTask constraintsTask)
        {
            ConstraintsTaskDTO c = new ConstraintsTaskDTO();
            c.IdConstraintTask = constraintsTask.IdConstraintTask;
            c.IdTasksToCustomers = constraintsTask.IdTasksToCustomers;
            c.DateToConstraint = constraintsTask.DateToConstraint;
            return c;
        }
        //מקבל אוביקט מסוג מחלקה של המשתמש ומחזיר את האוביקט מסוג מחלקה רגילה
        public static ConstraintsTask ConverToConstraintsTask(ConstraintsTaskDTO constraintsTaskDTO)
        {
            ConstraintsTask c = new ConstraintsTask();
            c.IdConstraintTask = constraintsTaskDTO.IdConstraintTask;
            c.IdTasksToCustomers = constraintsTaskDTO.IdTasksToCustomers;
            c.DateToConstraint = constraintsTaskDTO.DateToConstraint;
            c.DateEnd = constraintsTaskDTO.DateEnd;
            return c;
        }
        //מקבל רשימה מסוג מחלקה רגילה ומחזיר רשימה מסוג מחלקה למשתמש
        public static List<ConstraintsTaskDTO> ConverToConstraintsTaskDTO(List<ConstraintsTask> lConstraintsTask)
        {
            return lConstraintsTask.Select(a => ConverToConstraintsTaskDTO(a)).ToList();
        }
        //מקבל רשימה מסוג מחלקה של משתמש ומחזיר רשימה מסוג מחלקה רגילה
        public static List<ConstraintsTask> ConverToConstraintsTask(List<ConstraintsTaskDTO> lConstraintsTaskDTO)
        {
            return lConstraintsTaskDTO.Select(a => ConverToConstraintsTask(a)).ToList();
        }
    }
}
