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
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IdentityManager;
using IdentityManager.MembershipReboot;

namespace IdSrv.MRCfg
{
    public class CustomDatabase : MembershipRebootDbContext<CustomUser, CustomGroup>
    {
        //Note: paramless constructor needed when adding migrations
        public CustomDatabase() { }

        public CustomDatabase(string name)
            : base(name)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Hello there ;)
            //need to override the schema for the object here as it defaults to dbo of course
            //could also override the properties mapping to lower case, but ignore it for the time being
            //TODO

            base.OnModelCreating(modelBuilder);
        }
    }
}