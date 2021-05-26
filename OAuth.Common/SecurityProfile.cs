﻿using System;
using System.Collections.Generic;

namespace OAuth.Common
{
    public class SecurityProfile
    {
        public long UserID { get; set; }
        public DateTime Expiration { get; set; }
        public string AccessToken { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();
    }
}
