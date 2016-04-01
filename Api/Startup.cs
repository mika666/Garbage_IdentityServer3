using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Cors;
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

            // Wire token validation
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44300",

                // For access to the introspection endpoint
                ClientId = "api", //this is a name of an authorised scope - see scopes configuration in the identity server
                ClientSecret = "api-secret",

                RequiredScopes = new[] { "api" }
            });

            // Wire Web API
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(httpConfiguration);
        }
    }
}