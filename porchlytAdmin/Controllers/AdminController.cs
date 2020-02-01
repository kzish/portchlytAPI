using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList.Core;
using portchlytAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace portchlytAPI.Controllers
{
    [Route("Admin")]
    [Route("")]
    [AuthFilterAccount]
    public class AdminController : Controller
    {


        [Route("dash_board")]
        public IActionResult dash_board()
        {
            ViewBag.title = "dash_board";

            var artisans_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var num_artisans = artisans_col.Find(x => x._id != null).CountDocuments();
            ViewBag.num_artisans = num_artisans;


            var clients_col = globals.getDB().GetCollection<mClient>("mClient");
            var num_clients = clients_col.Find(x => x._id != null).CountDocuments();
            ViewBag.num_clients = num_clients;


            var disputes_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var num_disputes = disputes_col.Find(x => x._id != null).CountDocuments();
            ViewBag.num_disputes = num_disputes;


            var job_requests_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
            var num_job_requests = job_requests_col.Find(x => x._id != null).CountDocuments();
            ViewBag.num_job_requests = num_job_requests;


            var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
            var num_jobs = jobs_col.Find(x => x._id != null).CountDocuments();
            ViewBag.num_jobs = num_jobs;




            return View();
        }


        [Route("artisans")]
        public IActionResult artisans(int page = 1)
        {
            ViewBag.title = "artisans";
            var items_per_page = 25;

            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var list_ofartisans = artisan_col.Find(i => i._id != null).ToList();
            var artisans = (from d in list_ofartisans select d).AsQueryable().ToPagedList(page, items_per_page);
            ViewBag.artisans = artisans;
            return View();
        }


        [Route("clients")]
        public IActionResult clients(int page = 1)
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
        public IActionResult job_requests(int page = 1)
        {
            ViewBag.title = "job_requests";

            var items_per_page = 25;

            var job_requests_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
            var list_of_job_requests = job_requests_col.Find(i => i._id != null).ToList();
            var job_requests = (from d in list_of_job_requests select d ).OrderByDescending(x => x.time_of_request).AsQueryable().ToPagedList(page, items_per_page);
            ViewBag.job_requests = job_requests;
            return View();
        }


        [Route("disputes")]
        public IActionResult disputes(int page = 1)
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
        public IActionResult jobs(int page = 1)
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
        public IActionResult view_artisan(string artisan_app_id)
        {
            ViewBag.title = "view_artisan";
            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var artisan = artisan_col.Find(i => i.app_id == artisan_app_id).FirstOrDefault();

            var disputes_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var num_disputes = disputes_col.Find(i => i.artisan_app_id == artisan_app_id).CountDocuments();
            ViewBag.num_disputes = num_disputes;




            var payment_col = globals.getDB().GetCollection<mPayment>("mPayment");

            //grand total
            var cash_payments = payment_col.Find(i => i.artisan_app_id == artisan.app_id && i.paymentType == PaymentType.cash).ToList();
            var card_payments = payment_col.Find(i => i.artisan_app_id == artisan.app_id && i.paymentType == PaymentType.card).ToList();
            double total_card_payments = card_payments.Sum(i => i.amount_payed);
            double total_cash_payments = cash_payments.Sum(i => i.amount_payed);
            double abs_total = total_card_payments + total_cash_payments;
            double company_percentage = globals.company_artisan_split_value;
            double companys_cut = (company_percentage / 100) * abs_total;
            double artisans_earning = (abs_total - companys_cut);


            //for this current month wc have not been remitted
            var cm_cash_payments = cash_payments.Where(i => i.date.Month == DateTime.Now.Month && !i.remitted).ToList(); //payment_col.Find(i => i.artisan_app_id == artisan.app_id && i.paymentType == PaymentType.cash  && i.date.Month == DateTime.Now.Month).ToList();
            var cm_card_payments = card_payments.Where(i => i.date.Month == DateTime.Now.Month && !i.remitted).ToList();//payment_col.Find(i => i.artisan_app_id == artisan.app_id && i.paymentType == PaymentType.card && i.date.Month == DateTime.Now.Month).ToList();
            double cm_total_card_payments = cm_card_payments.Sum(i => i.amount_payed);
            double cm_total_cash_payments = cm_cash_payments.Sum(i => i.amount_payed);
            double cm_abs_total = cm_total_card_payments + cm_total_cash_payments;
            double cm_company_percentage = globals.company_artisan_split_value;
            double cm_companys_cut = (cm_company_percentage / 100) * cm_abs_total;
            double cm_artisans_earning = (cm_abs_total - cm_companys_cut);


            //Grand total
            ViewBag.artisans_earning = artisans_earning;
            ViewBag.companys_cut = companys_cut;
            ViewBag.abs_total = abs_total;
            ViewBag.total_cash_payments = total_cash_payments;
            ViewBag.total_card_payments = total_card_payments;

            //this current month
            ViewBag.cm_artisans_earning = cm_artisans_earning;
            ViewBag.cm_companys_cut = cm_companys_cut;
            ViewBag.cm_abs_total = cm_abs_total;
            ViewBag.cm_total_cash_payments = cm_total_cash_payments;
            ViewBag.cm_total_card_payments = cm_total_card_payments;




            if (artisan.skills == null) artisan.skills = new List<string>();//prevent null exception
            ViewBag.artisan = artisan;






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
                TempData["type"] = "green";
                TempData["msg"] = "Aritsan Skills Assigned";
                return RedirectToAction("view_artisan", "Admin", new { artisan_app_id });

            }
            catch (Exception ex)
            {
                TempData["type"] = "red";
                TempData["msg"] = "Error Occurred";
                return RedirectToAction("view_artisan", "Admin", new { artisan_app_id });
            }
        }


        [Route("view_client")]
        public IActionResult view_client(string client_app_id)
        {
            var client_col = globals.getDB().GetCollection<mClient>("mClient");
            var client = client_col.Find(i => i.app_id == client_app_id).FirstOrDefault();
            ViewBag.client = client;

            ViewBag.title = "view_client";

            var disputes_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var client_disputes = disputes_col.Find(i => i.client_app_id == client_app_id).ToList();

            var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
            var client_jobs_completed = jobs_col.Find(i => i.client_app_id == client_app_id && i.job_status == JobStatus.closed).ToList();
            var client_jobs_cancelled = jobs_col.Find(i => i.client_app_id == client_app_id && i.job_status == JobStatus.cancelled).ToList();

            ViewBag.client_jobs_completed = client_jobs_completed;
            ViewBag.client_jobs_cancelled = client_jobs_cancelled;
            ViewBag.client_disputes = client_disputes;

            return View();
        }

        [Route("remit_artisan_cash")]
        public IActionResult remit_artisan_cash(string artisan_app_id)
        {

            try
            {
                //update the artisan busy status to false
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update
                    .Set(i => i.busy, false);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);


                //update the payments col also to clear the cash payment for this artisan
                var payments_col = globals.getDB().GetCollection<mPayment>("mPayment");
                var payments_update = Builders<mPayment>.Update
                    .Set(i => i.remitted, true);//set these payments to be remitted
                payments_col.UpdateMany(i => i.artisan_app_id == artisan_app_id && i.paymentType == PaymentType.cash, payments_update);

                //send statement to the artisan
                var artisans_earning = fetch_artisan_earnings_data(artisan_app_id);


                //notify the artisan that his money has been remitted
                dynamic notification = new JObject();
                notification.type = "general_notification";
                notification.general_notification_message = "You cash has been remitted, you will now continue to recieve jobs.";
                notification.monthly_earning = artisans_earning;//add statement information for the artisan
                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(notification)), 1, false);

                //new { type = "green", msg = "Cash Remitted Succesfully", artisan_app_id = artisan_app_id }
                TempData["type"] = "green";
                TempData["msg"] = "Cash Remitted Succesfully";
                return RedirectToAction("view_artisan", "Admin", new { artisan_app_id });

            }
            catch (Exception ex)
            {
                //return RedirectToAction("view_artisan", "Admin", new { type = "red", msg = "Error occurred", artisan_app_id = artisan_app_id });
                TempData["type"] = "red";
                TempData["msg"] = "Error occurred";
                return RedirectToAction("view_artisan", "Admin", new { artisan_app_id });
            }
        }



        [Route("enable_artisan_account")]
        public IActionResult enable_artisan_account(string artisan_app_id, string reason)
        {
            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var artisan_update = Builders<mArtisan>.Update
                .Set(i => i.busy, false)
                .Set(i => i.reason_for_enable, reason)
                .Set(i => i.enabled, true);
            artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);
            TempData["type"] = "green";
            TempData["msg"] = "Account Enabled";

            //send notification to the artisan
            dynamic notification = new JObject();
            notification.type = "general_notification";
            notification.general_notification_message = "Your account has been enabled, for the following reason: " + reason;
            var json = JsonConvert.SerializeObject(notification);
            globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(json), 1, false);

            dynamic status = new JObject();
            status.type = "account_status";
            status.msg = "active";
            globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(status)), 1, false);



            return RedirectToAction("view_artisan", "Admin", new { artisan_app_id });
        }

        [Route("disable_artisan_account")]
        public IActionResult disable_artisan_account(string artisan_app_id, string reason)
        {
            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var artisan_update = Builders<mArtisan>.Update
                .Set(i => i.busy, true)
                .Set(i => i.reason_for_enable, reason)
                .Set(i => i.enabled, false);
            artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);
            TempData["type"] = "red";
            TempData["msg"] = "Account Dissabled";

            //send notification to the artisan
            dynamic notification = new JObject();
            notification.type = "general_notification";
            notification.general_notification_message = "Your account has been disabled, for the following reason: " + reason;
            var json = JsonConvert.SerializeObject(notification);
            globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(json), 1, false);

            dynamic status = new JObject();
            status.type = "account_status";
            status.msg = "blocked";
            globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(status)), 1, false);

            return RedirectToAction("view_artisan", "Admin", new { artisan_app_id });
        }



        [Route("enable_client_account")]
        public IActionResult enable_client_account(string client_app_id, string reason)
        {
            var client_col = globals.getDB().GetCollection<mClient>("mClient");
            var client_update = Builders<mClient>.Update
                .Set(i => i.reason_for_enable, reason)
                .Set(i => i.enabled, true);
            client_col.UpdateOne(i => i.app_id == client_app_id, client_update);
            TempData["type"] = "green";
            TempData["msg"] = "Account Enabled";

            //send notification to the artisan
            dynamic notification = new JObject();
            notification.type = "enable_client";
            notification.general_notification_message = "Your account has been enabled, for the following reason: " + reason;
            var json = JsonConvert.SerializeObject(notification);
            globals.mqtt.Publish(client_app_id, Encoding.ASCII.GetBytes(json), 1, false);

            

            return RedirectToAction("view_client", "Admin", new {client_app_id });
        }

        [Route("disable_client_account")]
        public IActionResult disable_client_account(string client_app_id, string reason)
        {
            var client_col = globals.getDB().GetCollection<mClient>("mClient");
            var client_update = Builders<mClient>.Update
                .Set(i => i.reason_for_enable, reason)
                .Set(i => i.enabled, false);
            client_col.UpdateOne(i => i.app_id == client_app_id, client_update);
            TempData["type"] = "red";
            TempData["msg"] = "Account Dissabled";

            //send notification to the artisan
            dynamic notification = new JObject();
            notification.type = "disable_client";
            notification.general_notification_message = "Your account has been disabled, for the following reason: " + reason;
            var json = JsonConvert.SerializeObject(notification);
            globals.mqtt.Publish(client_app_id, Encoding.ASCII.GetBytes(json), 1, false);

            return RedirectToAction("view_client", "Admin", new { client_app_id });
        }



        [Route("view_dispute")]
        public IActionResult view_dispute(string dispute_id)
        {
            var dispute_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var dispute = dispute_col.Find(i => i._id == dispute_id).FirstOrDefault();
            ViewBag.title = "view_dispute";
            ViewBag.dispute = dispute;
            return View();
        }

        [HttpPost("save_dispute_status")]
        public IActionResult save_dispute_status(DisputeStatus dispute_status, string dispute_id,string resolution)
        {
            var dispute_col = globals.getDB().GetCollection<mDispute>("mDispute");
            var dispute_update = Builders<mDispute>
                .Update
                .Set(i => i.dispute_status, dispute_status)
                .Set(i => i.resolution, resolution);
            dispute_col.UpdateOne(i => i._id == dispute_id, dispute_update);
            TempData["msg"] = "Saved";
            TempData["type"] = "green";
            return RedirectToAction("view_dispute", "Admin", new { dispute_id });
        }

        //static method to fetch earnings
        public string fetch_artisan_earnings_data(string artisan_app_id)
        {
            var payments_col = globals.getDB().GetCollection<mPayment>("mPayment");
            var my_payments = payments_col.Find(i => i.artisan_app_id == artisan_app_id).ToList();


            var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
            var my_jobs = jobs_col.Find(i => i.artisan_app_id == artisan_app_id).ToList();


            //total_earnings
            double total_earnings = my_payments.Sum(i => i.amount_payed);
            //remove 15% for company
            total_earnings = total_earnings - ((globals.company_artisan_split_value / 100) * total_earnings);


            //total_earnings_this_month
            double total_earnings_this_month = my_payments.Where(i => i.date.Month == DateTime.Now.Month && i.date.Year == DateTime.Now.Year).Sum(i => i.amount_payed);
            //remove 15% for company
            total_earnings_this_month = total_earnings_this_month - ((globals.company_artisan_split_value / 100) * total_earnings_this_month);


            //total_jobs
            long total_jobs = my_jobs.Count();

            //total_jobs_this_month
            long total_jobs_this_month = my_jobs.Where(i => i.date.Month == DateTime.Now.Month && i.date.Year == DateTime.Now.Year).Count();



            dynamic res = new JObject();
            res.total_earnings = total_earnings;
            res.total_earnings_this_month = total_earnings_this_month;
            res.total_jobs = total_jobs;
            res.total_jobs_this_month = total_jobs_this_month;
            return JsonConvert.SerializeObject(res);
        }

    }//controller

   }//namespace
