using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AllDataForRouteAndTaskfromClientDTO
    {
        public RouteDTO route { get; set; }
        public TasksDTO[] Tasks { get; set; }
        public ConstraintsTaskDTO[] ConstraintsTask { get; set; }
        public bool goBackHome { get; set; }
        public List<HoursNotActivityofRouteDTO> HoursNotActivityofs { get; set; }
    }
}
