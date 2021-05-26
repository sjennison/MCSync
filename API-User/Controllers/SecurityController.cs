﻿using API_User.Cache;
using OAuth.Common;
using OAuth.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_User.Controllers
{
    [AllowedIPsOnly]
    [PresharedAuth]
    public class SecurityController : ApiController
    {
        [HttpPost]
        public void Grant(SecurityProfile profile)
        {
            SecurityCache.AddSecurityProfile(profile);
        }

        [HttpPost]
        public void Revoke(RevokeParameter token)
        {
            SecurityCache.Revoke(token.token);
        }

        public class RevokeParameter
        {
            public string token { get; set; }
        }
    }
}
