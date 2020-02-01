using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSite.Controllers
{
    [Route("Downloads")]
    public class DownloadsController : Controller
    {
        HostingEnvironment host;
        public DownloadsController(HostingEnvironment host)
        {
            this.host = host;
        }

        [Route("client.apk")]
        public IActionResult DownloadClientApp()
        {
            var stream = System.IO.File.OpenRead($"{host.WebRootPath}/downloads/client.apk");
            if (stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.
            return File(stream, "application/octet-stream"); // returns a FileStreamResult
        }

        [Route("artisan.apk")]
        public IActionResult DownloadArtisanApp()
        {
            var stream = System.IO.File.OpenRead($"{host.WebRootPath}/downloads/artisan.apk");
            if (stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.
            return File(stream, "application/octet-stream"); // returns a FileStreamResult
        }
    }
}
