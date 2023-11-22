using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public   class RouteWithTaskDTO
    {
        public RouteDTO route { get; set; }
        public List<TaskInRouteDetailsDTO>TaskInRouteDetails { get; set; }
    }
}
