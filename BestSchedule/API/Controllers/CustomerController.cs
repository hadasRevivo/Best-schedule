using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using DTO;

namespace API.Controllers
{
    [System.Web.Http.Cors.EnableCors("*", "*", "*")]

    public class CustomerController : ApiController
    {
        // GET: api/Customer
        [Route("api/Customer/CalcMyRoute"), HttpPost]
        public DetailsRouteAndTasksDTO CalcMyRoute([FromBody] AllDataForRouteAndTaskfromClientDTO a)
         {
            return new CustomerBLL().alldataCustomerToConvert(a);
         }
        [Route("api/Customer/currentRoute/{id}"), HttpGet]
        public DetailsRouteAndTasksDTO currentRoute([FromUri] string id)//צריכה להחזיר כזה 
        {
            return new RouteBLL().currentRoute(id);
        }

        [Route("api/Customer/AddMyRoute"), HttpPost]
        public bool AddMyRoute([FromBody] DetailsRouteAndTasksDTO a)
        {
            return new RouteBLL().AddNewRoute(a);
        }
        [Route("api/Customer/UpdateAccount"), HttpPost]
        public bool UpdateAccount([FromBody] CustomersDTO id)
        {
            return new CustomerBLL().UpdateAccount(id);
        }
        [Route("api/Customer/PreviousTasksPerCustomer/{id}"), HttpGet]
        public List<TasksCreatedDTO> PreviousTasksPerCustomer(string id)
        {
            return new CustomerBLL().PreviousTasksPerCustomer(id);
        }

        [Route("api/Customer/PreviousTasksPerThisRoute/{idCustomer},{idRoute}"), HttpGet]
        public List<TasksCreatedDTO> PreviousTasksPerThisRoute(string idCustomer, int idRoute)
        {
            return new CustomerBLL().PreviousTasksPerThisRoute(idCustomer, idRoute);
        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }
        [Route("api/Customer/previousRoutesPerCustomer/{idCustomer}"), HttpGet]
        public List<RoutsPerCustomerDTO> previousRoutesPerCustomer( string idCustomer)
        {
            return new CustomerBLL().previousRoutesPerCustomer(idCustomer);
        }
    }
}
