#pragma checksum "C:\Users\samaritan\Documents\Visual Studio 2017\projects\sirach\portchlytAPI\porchlytAdmin\Views\Admin\dash_board.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "458b8f8d9a5db1e35658de9255fec287de091cd2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_dash_board), @"mvc.1.0.view", @"/Views/Admin/dash_board.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/dash_board.cshtml", typeof(AspNetCore.Views_Admin_dash_board))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"458b8f8d9a5db1e35658de9255fec287de091cd2", @"/Views/Admin/dash_board.cshtml")]
    public class Views_Admin_dash_board : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\sirach\portchlytAPI\porchlytAdmin\Views\Admin\dash_board.cshtml"
   Layout = "_Layout";

#line default
#line hidden
            BeginContext(25, 239, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n\r\n\r\n\r\n    <div class=\"col-md-4\">\r\n        <div class=\"card text-white bg-primary mb-3\">\r\n            <div class=\"card-header\">Artisans</div>\r\n            <div class=\"card-body\">\r\n                <h4 class=\"card-title\">");
            EndContext();
            BeginContext(265, 20, false);
#line 11 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\sirach\portchlytAPI\porchlytAdmin\Views\Admin\dash_board.cshtml"
                                  Write(ViewBag.num_artisans);

#line default
#line hidden
            EndContext();
            BeginContext(285, 401, true);
            WriteLiteral(@"</h4>
            </div>
            <div class=""card-footer"">
                <a href=""/Admin/artisans"" style=""color:#fff;"">view</a>
            </div>
        </div>
    </div>


    <div class=""col-md-4"">
        <div class=""card text-white bg-warning mb-3"">
            <div class=""card-header"">Clients</div>
            <div class=""card-body"">
                <h4 class=""card-title"">");
            EndContext();
            BeginContext(687, 19, false);
#line 24 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\sirach\portchlytAPI\porchlytAdmin\Views\Admin\dash_board.cshtml"
                                  Write(ViewBag.num_clients);

#line default
#line hidden
            EndContext();
            BeginContext(706, 394, true);
            WriteLiteral(@"</h4>
            </div>
            <div class=""card-footer"">
                <a href=""/Admin/clients"" style=""color:#fff;"">view</a>
            </div>
        </div>
    </div>


    <div class=""col-md-4"">
        <div class=""card text-white bg-info mb-3"">
            <div class=""card-header"">Jobs</div>
            <div class=""card-body"">
                <h4 class=""card-title"">");
            EndContext();
            BeginContext(1101, 16, false);
#line 37 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\sirach\portchlytAPI\porchlytAdmin\Views\Admin\dash_board.cshtml"
                                  Write(ViewBag.num_jobs);

#line default
#line hidden
            EndContext();
            BeginContext(1117, 443, true);
            WriteLiteral(@"</h4>
            </div>
            <div class=""card-footer"">
                <a href=""/Admin/jobs"" style=""color:#fff;"">view</a>
            </div>
        </div>
    </div>



</div>



<div class=""row"">



    <div class=""col-md-4"">
        <div class=""card text-white bg-success mb-3"">
            <div class=""card-header"">Job Requests</div>
            <div class=""card-body"">
                <h4 class=""card-title"">");
            EndContext();
            BeginContext(1561, 24, false);
#line 59 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\sirach\portchlytAPI\porchlytAdmin\Views\Admin\dash_board.cshtml"
                                  Write(ViewBag.num_job_requests);

#line default
#line hidden
            EndContext();
            BeginContext(1585, 407, true);
            WriteLiteral(@"</h4>
            </div>
            <div class=""card-footer"">
                <a href=""/Admin/job_requests"" style=""color:#fff;"">view</a>
            </div>
        </div>
    </div>



    <div class=""col-md-4"">
        <div class=""card text-white bg-danger mb-3"">
            <div class=""card-header"">Disputes</div>
            <div class=""card-body"">
                <h4 class=""card-title"">");
            EndContext();
            BeginContext(1993, 20, false);
#line 73 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\sirach\portchlytAPI\porchlytAdmin\Views\Admin\dash_board.cshtml"
                                  Write(ViewBag.num_disputes);

#line default
#line hidden
            EndContext();
            BeginContext(2013, 211, true);
            WriteLiteral("</h4>\r\n            </div>\r\n            <div class=\"card-footer\">\r\n                <a href=\"/Admin/disputes\" style=\"color:#fff;\">view</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n   \r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
