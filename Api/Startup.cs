using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Allow all origins
            //TODO - perhaps llimit the allowed origins for a production env
            app.UseCors(CorsOptions.AllowAll);
            //can be done through an attribute per controller / method


            // Wire token validation  - comes from Identity3.AccessTokenValidation
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44300/core", //all the IDSrv stuff could be contstants. Provided of course this goes 'global'

                // For access to the introspection endpoint
                ClientId = "api", //this is a name of an authorised scope - see scopes configuration in the identity server
                ClientSecret = "api-secret",

                RequiredScopes = new[] { "api" }
            });



            //Note: when creating own auth proxy to the IdentityServer (so the client can use own logon facilities),
            //use TokenClient, that comes with the IdentityModel (package of course)
            //also, the token once retrieved can be stored in a cookie. Then it wil be cached for further usage.
            //this should also make it possible to have the api load balanced or wbe garden'ed


            //Note: this in the default scaffolded template is done in a separate static class
            // Wire Web API
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented; //newtosoft json formatting! so nicely indented json is returned
            
            //make the json props be camel case!
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //add authorise attribute to all requests
            //httpConfiguration.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);
        }
    }
}