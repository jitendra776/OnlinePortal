using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAcess;

namespace Online.Controllers
{
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
                return Request.CreateResponse(HttpStatusCode.OK, obj.Registrations.ToList());
            }
        }
        #endregion
        #region GetBYID
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
        public HttpResponseMessage AddUser([FromBody]Registration data)
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
                obj.Registrations.Remove(data);
                obj.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Added");
            }
        }
        #endregion



    }
}
