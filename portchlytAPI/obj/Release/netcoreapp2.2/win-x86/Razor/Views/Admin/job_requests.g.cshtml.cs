#pragma checksum "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fefafe02ded0a77e287781b8c3c4a65658918775"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_job_requests), @"mvc.1.0.view", @"/Views/Admin/job_requests.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/job_requests.cshtml", typeof(AspNetCore.Views_Admin_job_requests))]
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
#line 4 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
using PagedList.Core;

#line default
#line hidden
#line 5 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
using PagedList;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fefafe02ded0a77e287781b8c3c4a65658918775", @"/Views/Admin/job_requests.cshtml")]
    public class Views_Admin_job_requests : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("pager-container"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "job_requests", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-keyword", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::PagedList.Core.Mvc.PagerTagHelper __PagedList_Core_Mvc_PagerTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
   Layout = "_Layout";

#line default
#line hidden
            BeginContext(25, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
  var job_requests = (IPagedList<portchlytAPI.Models.mArtisanServiceRequest>)ViewBag.job_requests;

#line default
#line hidden
            BeginContext(171, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(264, 729, true);
            WriteLiteral(@"
<style>
    .img_profile {
        border-radius: 100px;
    }
</style>

<script>

    function get_address(label, lat, lng) {
        $.ajax({
            url: ""/apiAdmin/get_address_from_geolocation?latitude="" + lat + ""&longitude="" + lng,
            success: function (address) {
                $(""#""+label).text(address);
                $(""#td_""+label).attr(""title"",address);
            }
        })
    }
</script>


<h1>Requested Jobs</h1>


<table class=""table table-hover td_ellipsize"">
    <thead>
        <tr>
            <th scope=""col"">Requested Services</th>
            <th scope=""col"">Client</th>
            <th scope=""col"">Address</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 42 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
         foreach (var job in job_requests)
        {

#line default
#line hidden
            BeginContext(1048, 29, true);
            WriteLiteral("        <tr>\r\n            <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 1077, "\"", 1128, 1);
#line 45 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
WriteAttributeValue("", 1085, String.Join(" " ,job.requested_services), 1085, 43, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1129, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1132, 40, false);
#line 45 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
                                                                Write(String.Join(" " ,job.requested_services));

#line default
#line hidden
            EndContext();
            BeginContext(1173, 22, true);
            WriteLiteral("</td>\r\n            <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 1195, "\"", 1221, 1);
#line 46 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
WriteAttributeValue("", 1203, job.client_mobile, 1203, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1222, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1224, 17, false);
#line 46 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
                                      Write(job.client_mobile);

#line default
#line hidden
            EndContext();
            BeginContext(1241, 22, true);
            WriteLiteral("</td>\r\n            <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 1263, "\"", 1279, 2);
            WriteAttributeValue("", 1268, "td_", 1268, 3, true);
#line 47 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
WriteAttributeValue("", 1271, job._id, 1271, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1280, 25, true);
            WriteLiteral(">\r\n                <label");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 1305, "\"", 1318, 1);
#line 48 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
WriteAttributeValue("", 1310, job._id, 1310, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1319, 77, true);
            WriteLiteral(">...</label>\r\n                <script>\r\n                        get_address(\"");
            EndContext();
            BeginContext(1397, 7, false);
#line 50 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
                                Write(job._id);

#line default
#line hidden
            EndContext();
            BeginContext(1404, 2, true);
            WriteLiteral("\",");
            EndContext();
            BeginContext(1407, 7, false);
#line 50 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
                                          Write(job.lat);

#line default
#line hidden
            EndContext();
            BeginContext(1414, 1, true);
            WriteLiteral(",");
            EndContext();
            BeginContext(1416, 7, false);
#line 50 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
                                                   Write(job.lon);

#line default
#line hidden
            EndContext();
            BeginContext(1423, 65, true);
            WriteLiteral(");\r\n                </script>\r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 54 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
        }

#line default
#line hidden
            BeginContext(1499, 28, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n");
            EndContext();
            BeginContext(1548, 233, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("pager", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "fefafe02ded0a77e287781b8c3c4a6565891877510470", async() => {
            }
            );
            __PagedList_Core_Mvc_PagerTagHelper = CreateTagHelper<global::PagedList.Core.Mvc.PagerTagHelper>();
            __tagHelperExecutionContext.Add(__PagedList_Core_Mvc_PagerTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#line 61 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
__PagedList_Core_Mvc_PagerTagHelper.List = job_requests;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("list", __PagedList_Core_Mvc_PagerTagHelper.List, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 62 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\job_requests.cshtml"
__PagedList_Core_Mvc_PagerTagHelper.Options = PagedList.Core.Mvc.PagedListRenderOptions.PageNumbersOnly;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("options", __PagedList_Core_Mvc_PagerTagHelper.Options, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __PagedList_Core_Mvc_PagerTagHelper.AspAction = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __PagedList_Core_Mvc_PagerTagHelper.AspController = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__PagedList_Core_Mvc_PagerTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-keyword", "PagedList.Core.Mvc.PagerTagHelper", "RouteValues"));
            }
            __PagedList_Core_Mvc_PagerTagHelper.RouteValues["keyword"] = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1781, 20, true);
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
