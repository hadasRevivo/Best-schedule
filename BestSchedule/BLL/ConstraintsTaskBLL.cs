using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
   public class ConstraintsTaskBLL
    {
        ConstraintsTaskDAL ConstraintsTaskDAL = new ConstraintsTaskDAL();
        public List<ConstraintsTask> GetConstraintsTask()
        {
            return ConstraintsTaskDAL.GetConstraintsTask();
        }

        public int Add(ConstraintsTaskDTO c)
        {
            new ConstraintsTaskDAL().Add(Converter.ConstraintsTaskConverter.ConverToConstraintsTask(c));
            return new ConstraintsTaskDAL().GetConstraintsTask().Max(x => x.IdConstraintTask);
        }
    }
}
