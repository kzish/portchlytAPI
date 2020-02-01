using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSite.Controllers
{
    [Route("Home")]
    [Route("")]
    public class HomeController : Controller
    {
        HostingEnvironment host;
        public HomeController(HostingEnvironment host)
        {
            this.host = host;
        }

        [Route("Index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
