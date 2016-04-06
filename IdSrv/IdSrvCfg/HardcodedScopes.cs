using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace IdSrv.IdSrvCfg
{
    public static class HardcodedScopes
    {
        /// <summary>
        /// Returns a list of scopes that a client can ask for when authenticating a user
        /// </summary>
        /// <returns></returns>
        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,

                StandardScopes.Email,

                //when requested, return an access token that can be used
                //to verify user athentication when calling the api
                new Scope
                {
                    Name = "api",

                    DisplayName = "Access to API",
                    Description = "This will grant you access to the API",

                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("api-secret".Sha256()) //so can use the token validation on the service level - thanks to that can call the api from backend
                    },

                    Type = ScopeType.Resource

                    //can use this to include needed user claims with the resource token
                    //,Claims = new List<ScopeClaim>()
                    //, IncludeAllClaimsForUser = true
                }
            };
        }
    }
}