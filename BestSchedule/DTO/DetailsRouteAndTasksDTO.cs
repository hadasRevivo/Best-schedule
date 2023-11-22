using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DetailsRouteAndTasksDTO
    {
        public RouteDTO route { get; set; }
        public TasksCreatedDTO[] createdTasks { get; set; }
        public TasksCreatedDTO[] conflictingTasks { get; set; }
        public int scoreForAlgorithm { get; set; }
    }
}
