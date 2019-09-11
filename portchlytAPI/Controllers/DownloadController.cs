using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace portchlytAPI.Controllers
{
    [Route("Download")]
    public class DownloadController : Controller
    {
        HostingEnvironment host;
        public DownloadController(HostingEnvironment e)
        {
            host = e;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("client")]
        public FileResult client()
        {
            var filepath = host.WebRootPath + "/downloads/";
            var filename   = "porchlyt_client.apk";
            IFileProvider provider = new PhysicalFileProvider(filepath);
            IFileInfo fileInfo = provider.GetFileInfo(filename);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.android.package-archive";
            return File(readStream, mimeType, filename);
        }

        [Route("artisan")]
        public FileResult artisan()
        {
            var filepath = host.WebRootPath + "/downloads/";
            var filename = "porchlyt_artisan.apk";
            IFileProvider provider = new PhysicalFileProvider(filepath);
            IFileInfo fileInfo = provider.GetFileInfo(filename);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.android.package-archive";
            return File(readStream, mimeType, filename);
        }


    }


    
   


}
