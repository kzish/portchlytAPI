using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//this is the landlord/property owner authfilter
namespace portchlytAPI.Controllers
{
    public class AuthFilterAccount: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var sess = filterContext.HttpContext.Session.GetString("user_id");
                
                if (sess == null || sess == "")
                {
                    filterContext.Result = new RedirectResult("/Auth/login");
                }
            }
            catch (Exception e)
            {
                filterContext.Result= new RedirectResult("/Auth/login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }

}
