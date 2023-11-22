using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class HoursNotActivityOfRouteDAL
    {
        BestScheduleEntities DB = new BestScheduleEntities();
        //הוספה עידכון מחיקה , GET ALL ,GET BY PRIMARY KEY
        public List<HoursNotActivityofRoute> GetHoursNotActivityofRoute()
        {
            return DB.HoursNotActivityofRoute.ToList();
        }
        public HoursNotActivityofRoute GetHoursNotActivityofRoute(int id)
        {
            return DB.HoursNotActivityofRoute.Find(id);
        }
        public bool Add(HoursNotActivityofRoute h)
        {
            try
            {
                DB.HoursNotActivityofRoute.Add(h);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    //    public bool Update(HoursNotActivityofRoute h)
    //    {
    //        try
    //        {
    //            DB.Entry(h);
    //            DB.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception)
    //        {

    //            return false;
    //        }
    //    }
    //    public bool Delete(HoursNotActivityofRoute h)
    //    {
    //        try
    //        {
    //            DB.HoursNotActivityofRoute.Remove(h);
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
