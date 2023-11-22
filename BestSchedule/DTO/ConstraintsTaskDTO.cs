using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class ConstraintsTaskDTO 
    {
        public int IdConstraintTask { get; set; }
        public int IdTasksToCustomers { get; set; }
        public System.DateTime DateToConstraint { get; set; }
        public System.DateTime DateEnd { get; set; } 
    }
}
