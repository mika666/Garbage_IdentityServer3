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


namespace IdSrv.IdSrvCfg
{
    class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory();


            //TODO - also make the scopes and clients db based! see the IdentityServer3 EntityFramework example
            //though they also use in memory stuff to prepopulate db if empty.
            //happens once per server init


            var scopeStore = new InMemoryScopeStore(HardcodedScopes.Get());
            factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStore);

            var clientStore = new InMemoryClientStore(HardcodedClients.Get());
            factory.ClientStore = new Registration<IClientStore>(resolver => clientStore);

            return factory;
        }
    }
}