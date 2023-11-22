
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
  public class TasksBLL
    {
        TasksDAL TasksDAL = new TasksDAL();

        //לפי הצורך ןירטואלי
        public List<Tasks> GetTasks()
        {
            return TasksDAL.GetTasks();
        }

        public int Add(TasksDTO t)
        {
            TasksDAL.Add(Converter.TasksConverter.ConverToTasks(t));
            return TasksDAL.GetTasks().Max(x => x.IdTasks);
        }
    }
}
