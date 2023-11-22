using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL.Converter
{
    public class TasksCreatedConverter
    {
        ConstraintsTaskBLL ConstraintsTask = new ConstraintsTaskBLL();

        public static TasksCreatedDTO ConverToTasksCreatedDTO(TasksToCustomers tasksToCustomers)
        {
            TasksCreatedDTO t = new TasksCreatedDTO();
            t.Task = Converter.TasksConverter.ConverToTasksDTO(tasksToCustomers.Tasks);
            t.Constraits = tasksToCustomers.ConstraintsTask.Count() > 0 ? Converter.ConstraintsTaskConverter.ConverToConstraintsTaskDTO(tasksToCustomers.ConstraintsTask.ElementAt(0)) : null;
            // t.DateTask = tasksToCustomers.Date;
            return t;
        }

        public static TasksCreatedDTO ConverToTasksCreatedDTO(Points p, Tasks[] tasks, ConstraintsTask[] constraints)
        {
            TasksCreatedDTO t = new TasksCreatedDTO();
            t.Task = Converter.TasksConverter.ConverToTasksDTO(tasks.FirstOrDefault(a => a.IdTasks == p.IdTask));
            t.Constraits = Converter.ConstraintsTaskConverter.ConverToConstraintsTaskDTO(constraints.FirstOrDefault(a => a.IdTasksToCustomers == p.IdTask));
            // t.DateTask =p.DateStart;
            return t;
        }
        public static TasksCreatedDTO[] ConverToTasksCreatedDTO(Points[] p, Tasks[] tasks, ConstraintsTask[] constraints)
        {
            return p.Select(x => ConverToTasksCreatedDTO(x, tasks, constraints)).ToArray();
        }
        public static List<TasksCreatedDTO> ConverToTasksCreatedDTO(List<TasksToCustomers> lTasksToCustomers)
        {
            return lTasksToCustomers.Select(a => ConverToTasksCreatedDTO(a)).ToList();
        }
        public TasksCreatedDTO ConverToTasksCreatedDTO(Tasks tasks, int idRoute)
        {
            TasksCreatedDTO t = new TasksCreatedDTO();
            t.Task = Converter.TasksConverter.ConverToTasksDTO(tasks);
            t.Constraits =Converter.ConstraintsTaskConverter.ConverToConstraintsTaskDTO(ConstraintsTask.GetConstraintsTask().FirstOrDefault(a => a.TasksToCustomers.IdTasks == t.Task.IdTasks && a.TasksToCustomers.IdRoute == idRoute));
            // t.DateTask = tasksToCustomers.Date;
            return t;
        }
        public List<TasksCreatedDTO> ConverToTasksCreatedDTO(List<Tasks> lTasks, int idRoute)
        {
            return lTasks.Select(a => ConverToTasksCreatedDTO(a, idRoute)).ToList();
        }

    }
}
