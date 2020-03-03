using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DataAcess;

namespace Online.Controllers
{
    [EnableCorsAttribute("http://localhost:4200", "*","*")]
    [RoutePrefix("Api/Register")]
    public class RegistrationController : ApiController
    {
        #region Get
        [HttpGet]
        [Route("AllUser")]
        public  HttpResponseMessage UserDetails()
        {
            using (OnlineEntities2 obj = new OnlineEntities2())
            {
                var data = obj.Registrations.ToList().Where(x => x.IsDeleted != "False");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }
        #endregion

        //Get with api route
        #region GetBYID
        [HttpGet]
        [Route("GetUserByContact/{contact}/{password}")]
        public IHttpActionResult GetUserByContact(int? contact, string password)
        {
            using (OnlineEntities2 obj = new OnlineEntities2())
            {
                var data = obj.Registrations.FirstOrDefault(x => x.Contact == contact);
                if (data != null)
                {
                    if (data.PassWord == password)
                    {
                        return Ok(true);
                    }
                }
                return Ok(false);
            }
        }

        // Get with HTTP query string
        [HttpGet]
        [Route("GetUserByID")]
        public HttpResponseMessage GetUserByID(int id)
        {
            using (OnlineEntities2 obj = new OnlineEntities2())
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj.Registrations.FirstOrDefault(x => x.ID == id));
            }
        }
        #endregion
        #region Add
        [HttpPost]
        [Route("AddUser")]
        public HttpResponseMessage AddUser(Registration data)
        {
            using (OnlineEntities2 obj = new OnlineEntities2())
            {
                obj.Registrations.Add(data);
                obj.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Added");
            }
        }
        #endregion
        #region Edit
        [HttpPut]
        [Route("EditUser")]
        public HttpResponseMessage EditUser([FromBody] Registration data)
        {
            using (OnlineEntities2 obj = new OnlineEntities2())
            {
                Registration user = obj.Registrations.FirstOrDefault(x => x.ID == data.ID);
                if (user != null)
                {
                    user.Name = data.Name;
                    user.PassWord = data.PassWord;
                    obj.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Edited");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
                }
            }
        }
        #endregion
        #region Delete
        [HttpDelete]
        [Route("DeleteUser")]
        public HttpResponseMessage DeleteUser(int id)
        {
            using (OnlineEntities2 obj = new OnlineEntities2())
            {
                var data = obj.Registrations.FirstOrDefault(x => x.ID == id);
                data.IsDeleted = "False";
                obj.SaveChanges();
                obj.SaveChanges();
                
                return Request.CreateResponse(HttpStatusCode.OK, "Added");
            }
        }
        #endregion



    }
}
