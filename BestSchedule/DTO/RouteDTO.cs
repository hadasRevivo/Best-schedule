using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class RouteDTO
    {
        public string nameRoute { get; set; }
        public int IdRoute { get; set; }
        public System.DateTime DateToRoute { get; set; }
        public string IdCustomers { get; set; }
        public System.DateTime StartDateToRoute { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}
