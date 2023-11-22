using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
   public class HoursNotActivityofRouteConverter
    {
        //מקבל אוביקט מסוג מחלקה רגילה ומחזיר את האוביקט מסוג מחלקה למשתמש  DTO
        public static HoursNotActivityofRouteDTO ConverToHoursNotActivityofRouteDTO(HoursNotActivityofRoute hoursNotActivityofRoute)
        {
            HoursNotActivityofRouteDTO h = new HoursNotActivityofRouteDTO();
            h.IdHoursNotActivityofRoute = hoursNotActivityofRoute.IdHoursNotActivityofRoute;
            h.StartDate = hoursNotActivityofRoute.StartDate;
            h.EndDate = hoursNotActivityofRoute.EndDate;
            h.IdRoute = hoursNotActivityofRoute.IdRoute;
            return h;
        }
        //מקבל אוביקט מסוג מחלקה של המשתמש ומחזיר את האוביקט מסוג מחלקה רגילה
        public static HoursNotActivityofRoute ConverToHoursNotActivityofRoute(HoursNotActivityofRouteDTO hoursNotActivityofRouteDTO)
        {
            HoursNotActivityofRoute h = new HoursNotActivityofRoute();
            h.IdHoursNotActivityofRoute = hoursNotActivityofRouteDTO.IdHoursNotActivityofRoute;
            h.StartDate = hoursNotActivityofRouteDTO.StartDate;
            h.EndDate = hoursNotActivityofRouteDTO.EndDate;
            h.IdRoute = hoursNotActivityofRouteDTO.IdRoute;
            return h;
        }
        //מקבל רשימה מסוג מחלקה רגילה ומחזיר רשימה מסוג מחלקה למשתמש
        public static List<HoursNotActivityofRouteDTO> ConverToHoursNotActivityofRouteDTO(List<HoursNotActivityofRoute> lHoursNotActivityofRoute)
        {
            return lHoursNotActivityofRoute.Select(a => ConverToHoursNotActivityofRouteDTO(a)).ToList();
        }
        //מקבל רשימה מסוג מחלקה של משתמש ומחזיר רשימה מסוג מחלקה רגילה
        public static List<HoursNotActivityofRoute> ConverToHoursNotActivityofRoute(List<HoursNotActivityofRouteDTO> lHoursNotActivityofRouteDTO)
        {
            return lHoursNotActivityofRouteDTO.Select(a => ConverToHoursNotActivityofRoute(a)).ToList();
        }
    }
}
