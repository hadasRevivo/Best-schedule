using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class TasksCreatedDTO
    {
        public TasksDTO Task { get; set; }
        public ConstraintsTaskDTO Constraits { get; set; }
       // public DateTime DateTask { get; set; }
    }
}
