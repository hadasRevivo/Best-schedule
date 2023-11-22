using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class RouteDAL
    {
        BestScheduleEntities DB = new BestScheduleEntities();
        //הוספה עידכון מחיקה , GET ALL ,GET BY PRIMARY KEY
        public List<Route> GetRoute()
        {
            return DB.Route.ToList();
        }
        public Route GetRoute(int idRoute)
        {
            return DB.Route.Find(idRoute);
        }
        public void Add(Route r)
        {
            try
            {
                r.DateToRoute = DateTime.Now;
                DB.Route.Add(r);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool Update(Route r)
        {
            try
            {
                DB.Entry(r);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //public bool Delete(Route r)
        //{
        //    try
        //    {
        //        DB.Route.Remove(r);
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

