#pragma checksum "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "58d864e647d88df60b698a39c4331f05b087b8ad"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_disputes), @"mvc.1.0.view", @"/Views/Admin/disputes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/disputes.cshtml", typeof(AspNetCore.Views_Admin_disputes))]
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
#line 4 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
using PagedList.Core;

#line default
#line hidden
#line 5 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
using PagedList;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"58d864e647d88df60b698a39c4331f05b087b8ad", @"/Views/Admin/disputes.cshtml")]
    public class Views_Admin_disputes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("pager-container"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "disputes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
   Layout = "_Layout";

#line default
#line hidden
            BeginContext(25, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
  var disputes = (IPagedList<portchlytAPI.Models.mDispute>)ViewBag.disputes;

#line default
#line hidden
            BeginContext(149, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(242, 309, true);
            WriteLiteral(@"



<h1>Diputes</h1>


<table class=""table table-hover td_ellipsize"">
    <thead>
        <tr>
            <th scope=""col"">Status</th>
            <th scope=""col"">Date</th>
            <th scope=""col"">Reason</th>
            <th scope=""col"">Action</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 26 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
         foreach (var dispute in disputes)
        {

#line default
#line hidden
            BeginContext(606, 37, true);
            WriteLiteral("            <tr>\r\n                <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 643, "\"", 674, 1);
#line 29 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
WriteAttributeValue("", 651, dispute.dispute_status, 651, 23, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(675, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(677, 22, false);
#line 29 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
                                               Write(dispute.dispute_status);

#line default
#line hidden
            EndContext();
            BeginContext(699, 26, true);
            WriteLiteral("</td>\r\n                <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 725, "\"", 769, 1);
#line 30 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
WriteAttributeValue("", 733, dispute.date.ToString("d MMM yyyy"), 733, 36, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(770, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(772, 35, false);
#line 30 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
                                                            Write(dispute.date.ToString("d MMM yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(807, 26, true);
            WriteLiteral("</td>\r\n                <td");
            EndContext();
            BeginWriteAttribute("title", " title=\"", 833, "\"", 868, 1);
#line 31 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
WriteAttributeValue("", 841, dispute.reason_for_dispute, 841, 27, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(869, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(871, 26, false);
#line 31 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
                                                   Write(dispute.reason_for_dispute);

#line default
#line hidden
            EndContext();
            BeginContext(897, 85, true);
            WriteLiteral("</td>\r\n                <td><a href=\"/view_dispute\">Open</a></td>\r\n            </tr>\r\n");
            EndContext();
#line 34 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
        }

#line default
#line hidden
            BeginContext(993, 28, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n");
            EndContext();
            BeginContext(1042, 225, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("pager", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "58d864e647d88df60b698a39c4331f05b087b8ad8640", async() => {
            }
            );
            __PagedList_Core_Mvc_PagerTagHelper = CreateTagHelper<global::PagedList.Core.Mvc.PagerTagHelper>();
            __tagHelperExecutionContext.Add(__PagedList_Core_Mvc_PagerTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#line 41 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
__PagedList_Core_Mvc_PagerTagHelper.List = disputes;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("list", __PagedList_Core_Mvc_PagerTagHelper.List, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 42 "C:\Users\samaritan\Documents\Visual Studio 2017\projects\portchlytAPI\portchlytAPI\Views\Admin\disputes.cshtml"
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
            BeginContext(1267, 20, true);
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
