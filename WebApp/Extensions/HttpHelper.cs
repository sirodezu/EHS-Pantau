using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class HttpHelper 
    {
        private static IHttpContextAccessor _HttpContextAccessor;
        public HttpHelper(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }

        public static string GetSessionValue(string keyval) {
            string result = _HttpContextAccessor.HttpContext.Session.GetString(keyval).ToString();
            return result;
        }
    }
}
