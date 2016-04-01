using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using IdSrv.IdMgrCfg;
using IdSrv.IdSrvCfg;
using Owin;

namespace IdSrv
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Since all local hosts, could do beter by registereing some extra domains for the dev machine!
            //app.Use(async (idx, next) =>
            //{
            //    Debug.WriteLine($"IdSrv Hit from: {idx.Request.RemoteIpAddress}");

            //    await next();
            //});



            //In memory hardcoded data example
            //app.UseIdentityServer(new IdentityServerOptions
            //{
            //    SiteName = "Embedded IdentityServer",
            //    SigningCertificate = LoadCertificate(),

            //    Factory = new IdentityServerServiceFactory()
            //        .UseInMemoryUsers(HardcodedUsers.Get())
            //        .UseInMemoryClients(HardcodedClients.Get())
            //        .UseInMemoryScopes(HardcodedScopes.Get())
            //});

            var mbrrbtConnStr = "MembershipRebootIdentityDb";

            //expose identity manager at /admin
            app.Map("/admin", adminApp =>
            {
                var factory = new IdentityManagerServiceFactory();
                factory.Configure(mbrrbtConnStr);

                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = factory
                });
            });

            //remap the default identityserver root path to /core
            app.Map("/core", core =>
            {
                var idSvrFactory = Factory.Configure();
                idSvrFactory.ConfigureCustomUserService(mbrrbtConnStr);

                var options = new IdentityServerOptions
                {
                    SiteName = "IdentityServer3 - UserService-MembershipReboot",

                    SigningCertificate = Certificate.Get(),
                    Factory = idSvrFactory,
                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureAdditionalIdentityProviders,
                    }
                };

                core.UseIdentityServer(options);
            });
        }

        public static void ConfigureAdditionalIdentityProviders(IAppBuilder app, string signInAsType)
        {
            //this need some xtra nuget packages!!!!

            //var google = new GoogleOAuth2AuthenticationOptions
            //{
            //    AuthenticationType = "Google",
            //    SignInAsAuthenticationType = signInAsType,
            //    ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
            //    ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
            //};
            //app.UseGoogleAuthentication(google);

            //var fb = new FacebookAuthenticationOptions
            //{
            //    AuthenticationType = "Facebook",
            //    SignInAsAuthenticationType = signInAsType,
            //    AppId = "676607329068058",
            //    AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
            //};
            //app.UseFacebookAuthentication(fb);

            //var twitter = new TwitterAuthenticationOptions
            //{
            //    AuthenticationType = "Twitter",
            //    SignInAsAuthenticationType = signInAsType,
            //    ConsumerKey = "N8r8w7PIepwtZZwtH066kMlmq",
            //    ConsumerSecret = "df15L2x6kNI50E4PYcHS0ImBQlcGIt6huET8gQN41VFpUCwNjM"
            //};
            //app.UseTwitterAuthentication(twitter);
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Certs\idsrv3test.pfx"), "idsrv3test");

            //TODO - load certificate from store. Having a signing certificate here is not a good idea; certicate should be installed to the windows certificate store and loaded from there.

            //X509Store store = new X509Store(StoreLocation.CurrentUser);
            //X509Certificate2 cer;
            //X509Certificate2Collection cers = store.Certificates.Find(X509FindType.FindBySubjectName, "My Cert's Subject Name", false);
            //if (cers.Count > 0)
            //{
            //    cer = cers[0];
            //};
        }
    }
}