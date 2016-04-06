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
using System.Linq;
using System.Web;

namespace IdSrv.MembershipRebootCustomisation
{

    /// <summary>
    /// Customised model for the User entity
    /// </summary>
    public class CustomUser : RelationalUserAccount
    {
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }
        public virtual int? Age { get; set; }

        //will this be saved to the db??? --looks like it will ;)
        //public virtual string SomeCustomUserProperty { get; set; }

        //although I am having some propblems adding migrations and updating db. ef related in general but nut sure of this is because postgres is used or there is something else to be considered

        //public virtual int? someNullableIdProp { get; set; }
    }


    //Note: there is also option to customise this without the group.
    //so it may well be a better option here.

    
    public class CustomUserAccountService : UserAccountService<CustomUser>
    {
        public CustomUserAccountService(CustomConfig config, CustomUserRepository repo)
            : base(config, repo)
        {
        }
    }

    public class CustomUserRepository : DbContextUserAccountRepository<CustomDatabase, CustomUser>
    {
        public CustomUserRepository(CustomDatabase ctx)
            : base(ctx)
        {
        }
    }
}