using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using DTO;
//using System.Web.Http.Cors;
using BLL;
using Microsoft.AspNetCore.Cors;
using System.Web.Http;

namespace API.Controllers
{
    [System.Web.Http.Cors.EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {

        // GET: api/Login
        LoginBLL LoginBLL = new LoginBLL();
        [Route("api/Login/Login"),HttpGet]
        public CustomersDTO Login(string email,string pass)
        {
          return  LoginBLL.Login(email, pass);

        }

        // GET: api/Login/5
        [Route("api/Login/Add"), HttpGet]

        public bool Add([FromBody] CustomersDTO customer)
        {
            return CustomerBLL.Add(customer);
        }
        CustomerBLL CustomerBLL = new CustomerBLL();
        // POST: api/Login
        [Route("api/Login/Post"), HttpPost]
        public bool Post([FromBody] CustomersDTO customer)
        {
            return CustomerBLL.Add(customer);
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
