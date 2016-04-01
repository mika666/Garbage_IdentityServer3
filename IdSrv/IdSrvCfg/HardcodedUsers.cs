using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace IdSrv.IdSrvCfg
{
    public static class HardcodedUsers
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    //TODO - this is gonna be an email!
                    Username = "bob",

                    //TODO - pass management will be achieved through the MembershipReboot!
                    Password = "secret",

                    //this is the actual id of a user!
                    //Should be combined with the identity server uri or something when using other identity providers
                    //but this is still to be tested. Not sure how the Identity server operates with external providers yet
                    Subject = "1", 

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Email, "bob.smith@email.com")
                    }
                }
            };
        }
    }
}