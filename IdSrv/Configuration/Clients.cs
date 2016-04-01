﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace IdSrv.Configuration
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "JS Client",
                    ClientId = "js",
                    Flow = Flows.Implicit,

                    //Note:
                    //redirect uris and allowed origins MUST be same as the client is operating on
                    //otherwise there will be errors and identity server will not let the user in

                    //this is a list of allowed redirect urls for the client
                    RedirectUris = new List<string>
                    {
                        "http://localhost:62891/popup.html",


                        // The new page is a valid redirect page after login
                        "http://localhost:62891/silent-renew.html"
                    },

                    // Valid URLs after logging out
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:62891/index.html"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:62891"
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