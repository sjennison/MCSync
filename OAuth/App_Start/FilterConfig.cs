﻿using API.Common.Attributes;
using System.Web;
using System.Web.Mvc;

namespace OAuth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
