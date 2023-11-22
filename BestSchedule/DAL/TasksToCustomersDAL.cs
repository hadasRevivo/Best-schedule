using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TasksToCustomersDAL
    {
        BestScheduleEntities DB = new BestScheduleEntities();
        //הוספה עידכון מחיקה , GET ALL ,GET BY PRIMARY KEY
        public List<TasksToCustomers> GetTasksToCustomers()
        {
            return DB.TasksToCustomers.ToList();
        }
        public TasksToCustomers GetTasksToCustomers(int id)
        {
            return DB.TasksToCustomers.Find(id);
        }
        public bool Add(TasksToCustomers t)
        {
            try
            {
                DB.TasksToCustomers.Add(t);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //public bool Update(TasksToCustomers t)
        //{
        //    try
        //    {
        //        DB.Entry(t);
        //        DB.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool Delete(TasksToCustomers t)
        //{
        //    try
        //    {
        //        DB.TasksToCustomers.Remove(t);
        //        DB.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}
