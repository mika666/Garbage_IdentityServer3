using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentityServer3.Core.Configuration;
using IdSrv.Configuration;
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

            app.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Embedded IdentityServer",
                SigningCertificate = LoadCertificate(),

                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(Users.Get())
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
            });
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