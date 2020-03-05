using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetUrl(this ControllerBase controllerBase, string path)
        {
            string scheme = controllerBase.HttpContext.Request.Scheme;
            string host = controllerBase.HttpContext.Request.Host.Host;
            string port = controllerBase.HttpContext.Connection.LocalPort.ToString();
            
            string url = scheme + "://" + host + ":" + port + path;

            return url;
        }
    }
}
