using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace IdSrv.IdSrvCfg
{
    public static class HardcodedClients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    //implicit

                    Enabled = true,
                    ClientName = "JS Client",
                    ClientId = "js",
                    Flow = Flows.Implicit,

                    //RequireConsent = false
                    //RequireSignOutPrompt = false

                    //Note:
                    //redirect uris and allowed origins MUST be same as the client is operating on
                    //otherwise there will be errors and identity server will not let the user in

                    //this is a list of allowed redirect urls for the client
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44301/popup.html",


                        // The new page is a valid redirect page after login
                        "https://localhost:44301/silent-renew.html"
                    },

                    // Valid URLs after logging out
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44301/index.html"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44301"
                    },

                    AllowAccessToAllScopes = true,

                    //TODO - start using explicitly defined scopes at some point!
                    //AllowedScopes = new List<string>
                    //{
                    //    "openid profile email" //and some other scopes as required
                    //}

                    AccessTokenLifetime = 70
                }
            };
        }
    }
}