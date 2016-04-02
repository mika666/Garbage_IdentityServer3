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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdSrv.MRCfg
{
    public class CustomConfig : MembershipRebootConfiguration<CustomUser>
    {
        public static readonly CustomConfig Config;

        static CustomConfig()
        {
            //Note:
            //maybe can do some db house keeping here too???
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<DefaultMembershipRebootDatabase, BrockAllen.MembershipReboot.Ef.Migrations.Configuration>());

            Config = new CustomConfig();
            Config.PasswordHashingIterationCount = 10000;
            Config.RequireAccountVerification = false;
            //config.EmailIsUsername = true;

            //Note
            //some extra opts maybe...
            //Config.AllowLoginAfterAccountCreation = true;
            //Config.RequireAccountVerification = false;
        }
    }
}