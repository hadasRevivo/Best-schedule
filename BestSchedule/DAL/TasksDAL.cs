using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class TasksDAL
    {
        BestScheduleEntities DB = new BestScheduleEntities();
        //הוספה עידכון מחיקה , GET ALL ,GET BY PRIMARY KEY
        public List<Tasks> GetTasks()
        {
            return DB.Tasks.ToList();
        }
        public Tasks GetTask(int IdTasks)
        {
            return DB.Tasks.Find(IdTasks);
        }
        public void Add(Tasks t)
        {
            try
            {
                DB.Tasks.Add(t);
                DB.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }
    //    public bool Update(Tasks t)
    //    {
    //        try
    //        {
    //            DB.Entry(t);
    //            DB.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    //    public bool Delete(Tasks t)
    //    {
    //        try
    //        {
    //            DB.Tasks.Remove(t);
    //            DB.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    }
}
