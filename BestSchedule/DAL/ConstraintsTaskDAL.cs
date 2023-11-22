using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConstraintsTaskDAL
    {
        BestScheduleEntities DB = new BestScheduleEntities();
        //הוספה עידכון מחיקה , GET ALL ,GET BY PRIMARY KEY
        public List<ConstraintsTask> GetConstraintsTask()
        {
            return DB.ConstraintsTask.ToList();
        }
        public ConstraintsTask GetConstraintsTask(int id)
        {
            return DB.ConstraintsTask.Find(id);
        }
        public void Add(ConstraintsTask t)
        {
            try
            {
                DB.ConstraintsTask.Add(t);
                DB.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }
        //public bool Update(ConstraintsTask t)
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
        //public bool Delete(ConstraintsTask t)
        //{
        //    try
        //    {
        //        DB.ConstraintsTask.Remove(t);
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
