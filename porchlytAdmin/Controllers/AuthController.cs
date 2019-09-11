using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using portchlytAPI.Models;
using System;
using System.Linq;

namespace portchlytAPI.Controllers
{
    [Route("Auth")]
    [Route("")]
    public class AuthController : Controller
    {


        //init the hosting environtment and cache
        private IHostingEnvironment host;

        public AuthController(IHostingEnvironment e)
        {
            host = e;
        }



        [HttpGet("login")]
        [HttpGet("")]
        public IActionResult login(String msg,string type)
        {
            ViewBag.title = "Login";
            ViewBag.msg = msg;
            ViewBag.type = type;
            return View();
        }
        


        [HttpPost("login")]
        public IActionResult login(mUsers user)
        {
            try
            {
                var Ucol = globals.getDB().GetCollection<mUsers>("mUsers");


                //check default  account is there
                var default_account = Ucol.Find(x => x._id != null).FirstOrDefault();
                if (default_account == null)
                {
                    //create default account
                    default_account = new mUsers();
                    default_account.email = "admin@plyt.com";
                    default_account.password = globals.getmd5("admin");
                    Ucol.InsertOne(default_account);
                }


                var exists = Ucol.Find(x => x.email == user.email.Trim() && x.password == globals.getmd5(user.password.Trim())).FirstOrDefault();
                if (exists == null)
                {
                    return RedirectToAction("login", "Auth", new { type = "red", msg = "Invalid credentials" });
                }
                else
                {
                    HttpContext.Session.SetString("user_id", exists._id);
                    return RedirectToAction("dash_board", "Admin");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("login", "Auth", new { type = "red", msg = "Error occured" });
            }
        }


        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login", "Auth");
        }



        [Route("forgot_password")]
        public IActionResult forgot_password()
        {
            ViewBag.title = "Forgot Password";
            return View();
        }


      


        


    }
}