using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using IdSrv.MembershipRebootCustomisation;

namespace Api.Controllers
{
    public class CreateUserDTO
    {
        //Note: this should really come from the generic DTO class.
        //unless specific to this very service of coure, then a DTO namespace could be used

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string Email { get; set; }

        public string Pass { get; set; }
    }


    [RoutePrefix("api")]
    public class UsersController : ApiController
    {
        private CustomUserAccountService _userService { get; set; }
    

        public UsersController()
        {
            _userService = new CustomUserAccountService(
                new CustomConfig(), 
                new CustomUserRepository(
                    new CustomDatabase("MembershipRebootIdentityDb")
                )    
            );
        }


        [Route("users")]
        [HttpPost]
        public IHttpActionResult Create(CreateUserDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("User object does not seem to be valid.");
                }

                //create a model off the incoming data
                var user = _userService.CreateAccount(model.UserName, model.Pass, model.Email);

                using (var db = new CustomDatabase(""))
                {
                    db.
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                //do some logging here...

                return InternalServerError();
            }
        }
    }
}
