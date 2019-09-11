using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace TestAPI.Controllers
{
    [Route("test")]
    [Route("")]
    public class TestController : Controller
    {
        [Route("")]
        public string Index()
        {
            return DateTime.Now.ToLongDateString();
        }
    }
}
