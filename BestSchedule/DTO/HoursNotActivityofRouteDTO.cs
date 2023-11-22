using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class HoursNotActivityofRouteDTO
    {
        public int IdHoursNotActivityofRoute { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int IdRoute { get; set; }
    }
}
