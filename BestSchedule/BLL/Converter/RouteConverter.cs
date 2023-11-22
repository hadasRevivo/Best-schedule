using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL.Converter
{
   public class RouteConverter
    {
        public static RoutsPerCustomerDTO ConverToRoutsPerCustomerDTO(Route Route)
        {
            RoutsPerCustomerDTO r = new RoutsPerCustomerDTO();
            r.nameRoute = Route.nameRoute;
            r.dateRoute =(DateTime) Route.StartDateToRoute;
            r.idRoute = Route.IdRoute;
            return r;
        }
        public static List<RoutsPerCustomerDTO> ConverRoutsPerCustomerDTO(List<Route> lRoute)
        {
            return lRoute.Select(a => ConverToRoutsPerCustomerDTO(a)).ToList();
        }
        //מקבל אוביקט מסוג מחלקה רגילה ומחזיר את האוביקט מסוג מחלקה למשתמש  DTO
        public static RouteDTO ConverToRouteDTO(Route route)
        {
            RouteDTO r = new RouteDTO();
            r.IdRoute = route.IdRoute;
            r.DateToRoute = (DateTime)(route.DateToRoute);
            r.IdCustomers = route.IdCustomers;
            r.StartDateToRoute = (DateTime)route.StartDateToRoute;
            r.EndDate = (DateTime)route.EndDate;
            r.nameRoute = route.nameRoute;
            return r;
        }
        //מקבל אוביקט מסוג מחלקה של המשתמש ומחזיר את האוביקט מסוג מחלקה רגילה
        public static Route ConverToRoute(RouteDTO routeDTO)
        {
            Route r = new Route();
            r.IdRoute = routeDTO.IdRoute;
            r.DateToRoute = routeDTO.DateToRoute;
            r.IdCustomers = routeDTO.IdCustomers;
            r.StartDateToRoute = routeDTO.StartDateToRoute;
            r.EndDate = routeDTO.EndDate;
            r.nameRoute = routeDTO.nameRoute;
            return r;
        }
        //מקבל רשימה מסוג מחלקה רגילה ומחזיר רשימה מסוג מחלקה למשתמש
        public static List<RouteDTO> ConverToRouteDTO(List<Route> lRoute)
        {
            return lRoute.Select(a => ConverToRouteDTO(a)).ToList();
        }
        //מקבל רשימה מסוג מחלקה של משתמש ומחזיר רשימה מסוג מחלקה רגילה
        public static List<Route> ConverToRoute(List<RouteDTO> lRouteDTO)
        {
            return lRouteDTO.Select(a => ConverToRoute(a)).ToList();
        }
    }
}
