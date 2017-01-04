/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Resources;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.EntityFramework;
using IdentityServer3.Core.Models;

namespace IdSrv.IdSrvCfg
{
    class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory();


            //Warning - users are configured elswhere through a CustomUserServiceExtensions
            //perhaps all could be done the very same way too!

            //TODO - also make the scopes and clients db based! see the IdentityServer3 EntityFramework example
            //though they also use in memory stuff to prepopulate db if empty.
            //happens once per server init



            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = "IdentityServerDb" //This could have to be dynamic of course! The good thing about the id server db is that there only will be one.
            };

            // these two calls just pre-populate the test DB from the in-memory config
            ConfigureClients(HardcodedClients.Get(), efConfig);
            ConfigureScopes(HardcodedScopes.Get(), efConfig);


            //In memory scopes and clients

            //var scopeStore = new InMemoryScopeStore(HardcodedScopes.Get());
            //factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStore);

            //var clientStore = new InMemoryClientStore(HardcodedClients.Get());
            //factory.ClientStore = new Registration<IClientStore>(resolver => clientStore);

            //this will register cofiguration services - so scopes and clients
            factory.RegisterConfigurationServices(efConfig);

            //and this will register operational services - tokens and such
            factory.RegisterOperationalServices(efConfig);

            return factory;
        }

        public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            //TODO - override the ClientConfigurationDbContext so can change schema for the objects created
            //this could perhaps be done in the migration scripts too...

            using (var db = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Clients.Any())
                {
                    foreach (var c in clients)
                    {
                        var e = c.ToEntity();
                        db.Clients.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }

        public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            //TODO - override the ScopeConfigurationDbContext so can change schema for the objects created
            //this could perhaps be done in the migration scripts too...

            using (var db = new ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Scopes.Any())
                {
                    foreach (var s in scopes)
                    {
                        var e = s.ToEntity();
                        db.Scopes.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}