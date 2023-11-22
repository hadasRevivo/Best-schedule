using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public  class TaskInRouteDetailsDTO
    {
        public Task Task { get; set; }
        public System.DateTime DateStart { get; set; }

        public System.DateTime DateEnd { get; set; }
        public System.DateTime DateMade { get; set; }


    }
}
