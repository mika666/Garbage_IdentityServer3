using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityModel.Client;
using IdSrv.MembershipRebootCustomisation;
using Microsoft.Owin.Security.Jwt;

using ClaimTypes = System.IdentityModel.Claims.ClaimTypes;

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

                using (var db = new CustomDatabase("MembershipRebootIdentityDb"))
                {
                    
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                //do some logging here...

                return InternalServerError();
            }
        }

        [Route("users/login")]
        [HttpGet]
        public async Task<IHttpActionResult> Login(string login, string pass)
        {

            var token = await GetToken(login, pass);

            //validating the token is not that important really in this scenario.
            //it will be needed to get the id of a user that it was granted to when dealing with the user related resources.
            //the good thing though is that whenever a token is returned, it means the user is authenticated. it's a matter of checking out the other stuff in order to get the suer id,
            //roles, resources and such.

            var claims = await ValidateIdentityTokenAsync(token);
            //return Ok(new {token, claims});

            return Ok(token);
        }


        private async Task<TokenResponse> GetToken(string user, string password)
        {
            var client = new TokenClient(
               "https://localhost:44300/core/connect/token",
               "auth-client", //client id
               "auth-secret"); //client secret

            var result = await client.RequestResourceOwnerPasswordAsync(user, password, "api auth-scope openid profile email");

            return result;

        }


        //Ok validating the token is not even necessary here

        private async Task<IEnumerable<Claim>> ValidateIdentityTokenAsync(TokenResponse token)
        {
            return await Task.Run<IEnumerable<Claim>>(() =>
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                
                var cert = new X509Certificate2(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Certs\idsrv3test.pfx"), "idsrv3test");

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "https://localhost:44300/core/resources",
                    ValidIssuer = "https://localhost:44300/core",
                    NameClaimType = "name",

                    IssuerSigningTokens = new X509CertificateSecurityTokenProvider(
                             "https://localhost:44300/core",
                             cert).SecurityTokens
                };

                SecurityToken t;
                ClaimsPrincipal id = tokenHandler.ValidateToken(token.AccessToken, validationParameters, out t);

                //Note:
                //can get the user id (subject) off the security token.
                //i actually think it all comes from the JWT!

                var claimList = id.Claims.ToList();

                claimList.Add(new Claim(ClaimTypes.Name, id.Identity.Name ?? "no name provided")); //name can be null if not set and this will fail if so.

                return claimList.AsEnumerable();
            });

        }
    }
}
