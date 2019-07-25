using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portchlytAPI.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using PagedList.Core;
using PagedList;
using System.Text;
using Newtonsoft.Json.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace portchlytAPI.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
       

        [Route("dash_board")]
        public IActionResult dash_board()
        {
            ViewBag.title = "dash_board";

            var artisans_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var num_artisans = artisans_col.Find(x => x._id != null).Count();
            ViewBag.num_artisans = num_artisans;


            var clients_col = globals.getDB().GetCollection<mClient>("mClient");
            var num_clients = clients_col.Find(x => x._id != null).Count();
            ViewBag.num_clients = num_clients;


            var disputes_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var num_disputes = clients_col.Find(x => x._id != null).Count();
            ViewBag.num_disputes = num_disputes;


            var job_requests_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
            var num_job_requests = job_requests_col.Find(x => x._id != null).Count();
            ViewBag.num_job_requests = num_job_requests;


            var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
            var num_jobs = job_requests_col.Find(x => x._id != null).Count();
            ViewBag.num_jobs = num_jobs;




            return View();
        }


        [Route("artisans")]
        public IActionResult artisans(int page=1)
        {
            ViewBag.title = "artisans";
            var items_per_page = 25;

            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var list_ofartisans = artisan_col.Find(i => i._id != null).ToList();
            var artisans = (from d in  list_ofartisans select d).AsQueryable().ToPagedList(page,items_per_page);
            ViewBag.artisans = artisans;
            return View();
        }


        [Route("clients")]
        public IActionResult clients(int page=1)
        {
            ViewBag.title = "clients";

            var items_per_page = 25;

            var clients_col = globals.getDB().GetCollection<mClient>("mClient");
            var list_of_clients = clients_col.Find(i => i._id != null).ToList();
            var clients = (from d in list_of_clients select d).AsQueryable().ToPagedList(page, items_per_page);
            ViewBag.clients = clients;
            return View();
        }


        [Route("job_requests")]
        public IActionResult job_requests(int page=1)
        {
            ViewBag.title = "job_requests";
            
                var items_per_page = 25;

            var job_requests_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
            var list_of_job_requests = job_requests_col.Find(i => i._id != null).ToList();
            var job_requests = (from d in list_of_job_requests select d).AsQueryable().ToPagedList(page, items_per_page);
            ViewBag.job_requests = job_requests;
            return View();
        }


        [Route("disputes")]
        public IActionResult disputes(int page=1)
        {
            ViewBag.title = "disputes";
            var items_per_page = 25;

            var disputes_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var list_of_disputes = disputes_col.Find(i => i._id != null).ToList();
            var disputes = (from d in list_of_disputes select d).AsQueryable().ToPagedList(page, items_per_page);
            ViewBag.disputes = disputes;
            return View();
        }


        [Route("jobs")]
        public IActionResult jobs(int page=1)
        {
            ViewBag.title = "jobs";
            var items_per_page = 25;

            var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
            var list_of_jobs = jobs_col.Find(i => i._id != null).ToList();
            var jobs = (from d in list_of_jobs select d).AsQueryable().ToPagedList(page, items_per_page);
            ViewBag.jobs = jobs;
            return View();
        }


        [HttpGet("view_artisan")]
        public IActionResult view_artisan(String artisan_app_id,string type, string msg)
        {
            ViewBag.title = "view_artisan";
            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var artisan = artisan_col.Find(i => i.app_id == artisan_app_id).FirstOrDefault();
            ViewBag.artisan = artisan;
            ViewBag.type = type;
            ViewBag.msg = msg;

            var disputes_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var num_disputes = disputes_col.Find(i => i.artisan_app_id == artisan_app_id).CountDocuments();
            ViewBag.num_disputes = num_disputes;

            double total_card_payments = 100;
            double total_cash_payments = 500;

            double abs_total = total_card_payments + total_cash_payments;

            double company_percentage = 15;

            double companys_cut = (company_percentage / 100) * abs_total;

            double artisans_earning = abs_total - companys_cut;


            ViewBag.artisans_earning = artisans_earning;
            ViewBag.companys_cut = companys_cut;
            ViewBag.abs_total = abs_total;
            ViewBag.total_cash_payments = total_cash_payments;
            ViewBag.total_card_payments = total_card_payments;



            return View();
        }



        [HttpPost("assign_artisan_skills")]
        public IActionResult assign_artisan_skills(List<string> artisan_skills, string artisan_app_id)
        {
            try
            {

                dynamic json = new JObject();
                json.type = "update_artisan_services";
                json.services = String.Join(":", artisan_skills);

                //save into database
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update.Set(i => i.skills, artisan_skills);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);

                //send to artisan
                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json)), 1, false);
                return RedirectToAction("view_artisan", "Admin", new { type = "green", msg = "skills saved", artisan_app_id= artisan_app_id });

            }
            catch (Exception ex)
            {
                return RedirectToAction("view_artisan", "Admin", new { type="red",msg="error occured", artisan_app_id =artisan_app_id});
            }
        }



    }
}
