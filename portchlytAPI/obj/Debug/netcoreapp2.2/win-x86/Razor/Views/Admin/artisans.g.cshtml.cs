#pragma checksum "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc539a57f9c4dafa28010710a782936e7afa4e3d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_artisans), @"mvc.1.0.view", @"/Views/Admin/artisans.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/artisans.cshtml", typeof(AspNetCore.Views_Admin_artisans))]
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
#line 4 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
using PagedList.Core;

#line default
#line hidden
#line 5 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
using PagedList;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc539a57f9c4dafa28010710a782936e7afa4e3d", @"/Views/Admin/artisans.cshtml")]
    public class Views_Admin_artisans : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/libs/rating/jquery.barrating.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/libs/rating/themes/css-stars.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/img/ic_worker.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("25"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("25"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img_profile"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "3", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "4", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "5", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("pager-container"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "artisans", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_14 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_15 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-keyword", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::PagedList.Core.Mvc.PagerTagHelper __PagedList_Core_Mvc_PagerTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
   Layout = "_Layout";

#line default
#line hidden
            BeginContext(25, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
  var artisans = (IPagedList<portchlytAPI.Models.mArtisan>)ViewBag.artisans;

#line default
#line hidden
            BeginContext(149, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(242, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(244, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc539a57f9c4dafa28010710a782936e7afa4e3d9207", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(305, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(307, 67, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cc539a57f9c4dafa28010710a782936e7afa4e3d10384", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(374, 879, true);
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

<h1>
    Artisans
</h1>


<table class=""table table-hover td_ellipsize"">
    <thead>
        <tr>
            <th scope=""col"">Image</th>
            <th scope=""col"">Mobile</th>
            <th scope=""col"">Name</th>
            <th scope=""col"">Jobs</th>
            <th scope=""col"">Location</th>
            <th scope=""col"">Rating</th>
            <th scope=""col"">Action</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 50 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
         foreach (var artisan in artisans)
        {

#line default
#line hidden
            BeginContext(1308, 15, true);
            WriteLiteral("            <tr");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 1323, "\"", 1367, 1);
#line 52 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 1331, artisan.enabled?"":"table-danger", 1331, 36, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1368, 25, true);
            WriteLiteral(">\r\n                <td>\r\n");
            EndContext();
#line 54 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                     if (artisan.image != null)
                    {

#line default
#line hidden
            BeginContext(1465, 28, true);
            WriteLiteral("                        <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 1493, "\"", 1513, 1);
#line 56 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 1499, artisan.image, 1499, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1514, 48, true);
            WriteLiteral(" height=\"25\" width=\"25\" class=\"img_profile\" />\r\n");
            EndContext();
#line 57 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                    }
                    else
                    {

#line default
#line hidden
            BeginContext(1634, 24, true);
            WriteLiteral("                        ");
            EndContext();
            BeginContext(1658, 83, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cc539a57f9c4dafa28010710a782936e7afa4e3d14534", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1741, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 61 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                    }

#line default
#line hidden
            BeginContext(1766, 42, true);
            WriteLiteral("                </td>\r\n                <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 1808, "\"", 1831, 1);
#line 63 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 1816, artisan.mobile, 1816, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1832, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1834, 14, false);
#line 63 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                                       Write(artisan.mobile);

#line default
#line hidden
            EndContext();
            BeginContext(1848, 26, true);
            WriteLiteral("</td>\r\n                <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 1874, "\"", 1895, 1);
#line 64 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 1882, artisan.name, 1882, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1896, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1898, 12, false);
#line 64 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                                     Write(artisan.name);

#line default
#line hidden
            EndContext();
            BeginContext(1910, 26, true);
            WriteLiteral("</td>\r\n                <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 1936, "\"", 1960, 1);
#line 65 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 1944, artisan.numJobs, 1944, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1961, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1963, 15, false);
#line 65 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                                        Write(artisan.numJobs);

#line default
#line hidden
            EndContext();
            BeginContext(1978, 26, true);
            WriteLiteral("</td>\r\n                <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2004, "\"", 2024, 2);
            WriteAttributeValue("", 2009, "td_", 2009, 3, true);
#line 66 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 2012, artisan._id, 2012, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2025, 29, true);
            WriteLiteral(">\r\n                    <label");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2054, "\"", 2071, 1);
#line 67 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 2059, artisan._id, 2059, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2072, 81, true);
            WriteLiteral(">...</label>\r\n                    <script>\r\n                        get_address(\"");
            EndContext();
            BeginContext(2154, 11, false);
#line 69 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                                Write(artisan._id);

#line default
#line hidden
            EndContext();
            BeginContext(2165, 2, true);
            WriteLiteral("\",");
            EndContext();
            BeginContext(2168, 31, false);
#line 69 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                                              Write(artisan.location.coordinates[0]);

#line default
#line hidden
            EndContext();
            BeginContext(2199, 1, true);
            WriteLiteral(",");
            EndContext();
            BeginContext(2201, 31, false);
#line 69 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
                                                                               Write(artisan.location.coordinates[1]);

#line default
#line hidden
            EndContext();
            BeginContext(2232, 77, true);
            WriteLiteral(");\r\n                    </script>\r\n                </td>\r\n                <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 2309, "\"", 2344, 2);
            WriteAttributeValue("", 2317, "rating", 2317, 6, true);
#line 72 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue(" ", 2323, artisan.getRating(), 2324, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2345, 83, true);
            WriteLiteral(">\r\n\r\n                    <select class=\"rating\" readonly>\r\n                        ");
            EndContext();
            BeginContext(2428, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc539a57f9c4dafa28010710a782936e7afa4e3d21764", async() => {
                BeginContext(2483, 1, true);
                WriteLiteral("1");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 75 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
AddHtmlAttributeValue("", 2456, artisan.getRating()==1, 2456, 25, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2493, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(2519, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc539a57f9c4dafa28010710a782936e7afa4e3d23629", async() => {
                BeginContext(2574, 1, true);
                WriteLiteral("2");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 76 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
AddHtmlAttributeValue("", 2547, artisan.getRating()==2, 2547, 25, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2584, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(2610, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc539a57f9c4dafa28010710a782936e7afa4e3d25494", async() => {
                BeginContext(2665, 1, true);
                WriteLiteral("3");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 77 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
AddHtmlAttributeValue("", 2638, artisan.getRating()==3, 2638, 25, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2675, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(2701, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc539a57f9c4dafa28010710a782936e7afa4e3d27359", async() => {
                BeginContext(2756, 1, true);
                WriteLiteral("4");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 78 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
AddHtmlAttributeValue("", 2729, artisan.getRating()==4, 2729, 25, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2766, 26, true);
            WriteLiteral("\r\n                        ");
            EndContext();
            BeginContext(2792, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc539a57f9c4dafa28010710a782936e7afa4e3d29226", async() => {
                BeginContext(2847, 1, true);
                WriteLiteral("5");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 79 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
AddHtmlAttributeValue("", 2820, artisan.getRating()==5, 2820, 25, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2857, 80, true);
            WriteLiteral("\r\n                    </select>\r\n\r\n                </td>\r\n                <td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2937, "\"", 2994, 2);
            WriteAttributeValue("", 2944, "/Admin/view_artisan?artisan_app_id=", 2944, 35, true);
#line 83 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
WriteAttributeValue("", 2979, artisan.app_id, 2979, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2995, 35, true);
            WriteLiteral(">Open</a></td>\r\n            </tr>\r\n");
            EndContext();
#line 85 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
        }

#line default
#line hidden
            BeginContext(3041, 28, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n");
            EndContext();
            BeginContext(3090, 225, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("pager", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cc539a57f9c4dafa28010710a782936e7afa4e3d32023", async() => {
            }
            );
            __PagedList_Core_Mvc_PagerTagHelper = CreateTagHelper<global::PagedList.Core.Mvc.PagerTagHelper>();
            __tagHelperExecutionContext.Add(__PagedList_Core_Mvc_PagerTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
#line 92 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
__PagedList_Core_Mvc_PagerTagHelper.List = artisans;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("list", __PagedList_Core_Mvc_PagerTagHelper.List, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 93 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\artisans.cshtml"
__PagedList_Core_Mvc_PagerTagHelper.Options = PagedList.Core.Mvc.PagedListRenderOptions.PageNumbersOnly;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("options", __PagedList_Core_Mvc_PagerTagHelper.Options, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __PagedList_Core_Mvc_PagerTagHelper.AspAction = (string)__tagHelperAttribute_13.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
            __PagedList_Core_Mvc_PagerTagHelper.AspController = (string)__tagHelperAttribute_14.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
            if (__PagedList_Core_Mvc_PagerTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-keyword", "PagedList.Core.Mvc.PagerTagHelper", "RouteValues"));
            }
            __PagedList_Core_Mvc_PagerTagHelper.RouteValues["keyword"] = (string)__tagHelperAttribute_15.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3315, 155, true);
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n<script>\r\n    $(\'.rating\').barrating({\r\n        theme: \'css-stars\',\r\n        readonly: true,\r\n        hoverState: false\r\n    });\r\n</script>\r\n\r\n\r\n");
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