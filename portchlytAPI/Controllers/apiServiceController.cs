using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using portchlytAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// this controller is for the porchlyt-service app api
/// </summary>
namespace portchlytAPI.Controllers
{
    [Route("apiService")]
    public class apiServiceController : Controller
    {

        HostingEnvironment host;
        public apiServiceController(HostingEnvironment e)
        {
            host = e;
        }



        [Route("")]
        public String Index()
        {
            return "porchlyt service api";
        }

        /// <summary>
        /// register the artisan via the mobile app
        /// </summary>
        /// <param name="artisan"></param>
        /// <returns></returns>
        [HttpPost("clientRegistration")]
        public JsonResult clientRegistration(string data)
        {
            mClient client = JsonConvert.DeserializeObject<mClient>(data);//convert data to object
            //check mobile exist
            var acol = globals.getDB().GetCollection<mClient>("mClient");
            var exist = acol.Find(x => x.mobile == client.mobile).FirstOrDefault();
            if (exist == null)//insert this one
            {
                //generate random 5 digit otp number
                Random r = new Random();
                var x = r.Next(0, 100000);
                string OTP = x.ToString("00000");
                client.otp = OTP;
                //insert new client into the database
                acol.InsertOne(client);

                //send bulk sms to user via the bulk sms api with the OTP
                /* WebClient wc = new WebClient();
                 wc.QueryString.Add("mobile", client.mobile);
                 wc.QueryString.Add("msg", "Your OTP is: " + OTP);
                 var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                 var responseString = UnicodeEncoding.UTF8.GetString(data_);*/

                return Json(new { res = "ok", msg = "registration complete", otp = OTP });
            }
            else if (exist != null)//simply resend the otp and update the information
            {
                try
                {
                    //send bulk sms to user via the bulk sms api with the OTP
                    /* WebClient wc = new WebClient();
                     wc.QueryString.Add("mobile", exist.mobile);
                     wc.QueryString.Add("msg", "Your OTP is: " + exist.otp);
                     var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                     var responseString = UnicodeEncoding.UTF8.GetString(data_);*/
                     

                    client._id = exist._id;//this will maintain the id
                    client.otp = exist.otp;//this maintains the otp also
                    client.app_id = exist.app_id;//maintain the app id
                    client.enabled = exist.enabled;//maintain the account status
                    acol.ReplaceOne(i => i.mobile == client.mobile, client);//replace it thus updating it incase any information is changed
                    return Json(new { res = "ok", msg = "registration complete", otp = exist.otp, app_id=exist.app_id });
                }
                catch (Exception ex)
                {
                    return Json(new { msg = ex.Message, res = "err" });
                }
            }
            else //this number is in use and is registered
            {
                return Json(new { msg = "This mobile is in use login instead", res = "err" });
            }
        }


        /// <summary>
        /// using  the correct otp we will confirm the user is now registered
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("clientConfirmRegistration")]
        public JsonResult clientConfirmRegistration(string data)
        {
            try
            {
                mClient m = JsonConvert.DeserializeObject<mClient>(data);
                var update = Builders<mClient>.Update.Set(x => x.registered, true);
                var acol = globals.getDB().GetCollection<mClient>("mClient");
                acol.UpdateOne(x => x.mobile == m.mobile, update);//update this artisans registration status
                //also check if this client is blocked and send this information to the client
                var client_col = globals.getDB().GetCollection<mClient>("mClient");
                var client = client_col.Find(x => x.mobile == m.mobile).FirstOrDefault();
                string account_status = "active";
                if (!client.enabled) account_status = "blocked";
                return Json(new { res = "ok", msg = "registration confirmed" , account_status = account_status });
            }
            catch (Exception ex)
            {
                return Json(new { res = "err", msg = ex.Message });
            }
        }


        /// <summary>
        /// method to search for an artian with the correct services he/she offers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("findServiceArtisan")]
        public JsonResult findServiceArtisan(string data)
        {
            try
            {
                //the reverse geo coding is done already on the client side here simply get the geo coords
                dynamic json = JsonConvert.DeserializeObject(data);
                double lat = Double.Parse((string)json.lat);//the latitude
                double lon = Double.Parse((string)json.lon);//the longitude
                string client_app_id = (string)json.app_id;//application id of the client to be used for the mqtt
                string request_id = (string)json.request_id;//the request_id as sent by the client
                List<string> services = JsonConvert.DeserializeObject<List<string>>((string)json.services);//these are the services required by the client

                var point = new GeoJson2DGeographicCoordinates(lat, lon);
                var pnt = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(point);
                var maxDistanceInKm = 100000;//search this radius for the artisan in km
                var filter = Builders<mArtisan>.Filter.Near(i => i.location.coordinates, pnt, maxDistanceInKm);
                var acol = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var ccol = globals.getDB().GetCollection<mClient>("mClient");
                var client = ccol.Find(i => i.app_id == client_app_id).FirstOrDefault();//fetch the client in question
                var asr_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");

                //add this request to the database
                var newRequest = new mArtisanServiceRequest();
                newRequest.client_request_id = request_id;
                newRequest.requested_services = services;
                newRequest.client_app_id = client_app_id;
                newRequest.lat = lat;
                newRequest.lon = lon;
                newRequest.client_mobile = client.mobile;
                asr_col.InsertOne(newRequest);

                //artisans within 100 km of the client
                //will return in ranked order of closeness
                var artisans_within_100_km_of_the_client = acol.Find(filter).ToList();

                //collect all artisans who has one or more of the skills and are available and are within 100km range in order of closeness
                var list_of_artisans_with_any_of_these_services_who_are_availalable = artisans_within_100_km_of_the_client
                    .Where(i => i.skills.Intersect(services).Any()
                     && i.on_duty
                     && !i.busy)
                     .ToList()
                     .OrderByDescending(x => x.skills.Intersect(services).Count())//order by who has the most relevant jobs to this search
                     .ToList()
                     ;
                //fire and forget this thread
                Task.Run(() =>
                {
                    NotifyArtisansForTask(request_id, list_of_artisans_with_any_of_these_services_who_are_availalable);
                });
                return Json(new { res = "ok", msg = "results will be returned via mqtt" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message, res = "err" });
            }
        }

        /// <summary>
        /// //this recursive function is called every 20 seconds to check for an artisan for a task
        /// the artisans are already in sorted order according to who has the most relevant skills
        /// also passed is the artisan_service_request with the required skills
        /// </summary>
        /// <param name="asr"></param>
        /// <param name="artisans"></param>
        private void NotifyArtisansForTask(string request_id, List<mArtisan> artisans)
        {
            var asr_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
            var asr = asr_col.Find(i => i.client_request_id == request_id).FirstOrDefault();//fetch it each time to check if any changes, because this can be modified elsewhere by another service
            if (asr == null || asr.requested_services.Count == 0)
            {
                //no more services to service, it means all services have been assigned
                //delete the request and break this loop
                asr_col.DeleteOne(i => i.client_request_id == request_id);
                //notify the client that this search is completed ie all services are catered for
                dynamic notification_for_client = new JObject();
                notification_for_client.request_id = request_id;
                notification_for_client.type = "artisan_search_request_notification";
                notification_for_client.msg = "search_is_complete";
                globals.mqtt.Publish(asr.client_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(notification_for_client)), 1, false);

                return;

            }
            else if (artisans.Count == 0)
            {
                //no more artisans left
                //notify client that no artisans were found for his request for this task in particular
                //then break the loop
                dynamic notification_for_client = new JObject();
                notification_for_client.request_id = request_id;
                notification_for_client.type = "artisan_search_request_notification";
                notification_for_client.msg = "no_artisans_found";
                notification_for_client.data = string.Join(":", asr.requested_services);
                globals.mqtt.Publish(asr.client_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(notification_for_client)), 1, false);
                return;
            }

            //send artisan a notification which he must respond in 20 seconds, this notification is QoS 0 becuse if he misses the notification he misses it period
            dynamic request_notification_for_artisan = new JObject();
            var items = artisans[0].skills.Intersect(asr.requested_services).ToArray();
            request_notification_for_artisan.type = "request_task_notification";
            request_notification_for_artisan.lat = asr.lat;
            request_notification_for_artisan.lon = asr.lon;
            request_notification_for_artisan.services = String.Join(",", items);//send intersect services, ie the services that this artisan can do
            request_notification_for_artisan.client_app_id = asr.client_app_id;
            request_notification_for_artisan.client_mobile = asr.client_mobile;
            request_notification_for_artisan.request_id = request_id;
            globals.mqtt.Publish(artisans[0].app_id, Encoding.ASCII.GetBytes(Regex.Unescape(JsonConvert.SerializeObject(request_notification_for_artisan))), 1, false);//send the notification
            Thread.Sleep(20000);//wait for 20 seconds before moving on
            artisans.RemoveAt(0);//remove the first artisan since we have dealt with him already
            NotifyArtisansForTask(request_id, artisans);//call function recursively, this will go to the next person on the list
        }//.NotifyArtisanForTask





        //update the request to notify the client in the future that an artisan was found
        [Route("NoArtisanFoundActionClientToTake")]
        public JsonResult NoArtisanFoundActionClientToTake(string data)
        {
            try
            {
                var request_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
                dynamic json = JsonConvert.DeserializeObject(data);
                string request_id = json.request_id;
                string action_to_take = json.action_to_take;
                if (action_to_take == "notify_me_when_artisan_is_available")
                {
                    var update = Builders<mArtisanServiceRequest>.Update.Set(ii => ii.notify_client_in_the_future, true);
                    request_col.UpdateOne(i => i.client_request_id == request_id, update);//update and notify the client as soon as an artisan is available
                }
                else if (action_to_take == "ignore")
                {
                    request_col.DeleteOne(i => i.client_request_id == request_id);//delete the request
                }
                return Json(new { res = "ok", msg = "action saved" });
            }
            catch (Exception ex)
            {
                return Json(new { res = "err", msg = ex.Message });
            }
        }



        //get the profile picture of the artisan
        [Route("fetch_artisan_profile_picture")]
        public FileResult fetch_artisan_profile_picture(string artisan_app_id)
        {
            try
            {
                var image = System.IO.File.OpenRead(host.WebRootPath + "/profile_pictures/" + artisan_app_id + ".jpg");
                return File(image, "image/jpeg");
            }catch(Exception ex)
            {
                return null;
            }
        }


        //make payment and indicate the payment type
        [Route("make_payment_for_artisan")]
        public string make_payment_for_artisan(string _job_id, double amount_payed, string client_app_id, string artisan_app_id, string payment_type)
        {
            try
            {
                //
                var payment_col = globals.getDB().GetCollection<mPayment>("mPayment");
                var exist = payment_col.Find(i => i._job_id == _job_id).FirstOrDefault();
                if (exist == null)
                {

                    //inserting


                    var payment = new mPayment();
                    payment._job_id = _job_id;
                    payment.amount_payed = amount_payed;
                    payment.client_app_id = client_app_id;
                    payment.artisan_app_id = artisan_app_id;

                    if (payment_type == "cash")
                    {
                        payment.paymentType = PaymentType.cash;
                    }
                    else if (payment_type == "card")
                    {
                        payment.paymentType = PaymentType.card;
                    }

                    //insert payment
                    payment_col.InsertOne(payment);
                }
                else
                {
                    //updating

                    if (payment_type == "cash")
                    {
                        var payment_update = Builders<mPayment>.Update
                        .Set(i => i.amount_payed, amount_payed)
                        .Set(i => i.client_app_id, client_app_id)
                        .Set(i => i.artisan_app_id, artisan_app_id)
                        .Set(i => i.paymentType, PaymentType.cash)
                        ;

                        payment_col.UpdateOne(i => i._job_id == _job_id, payment_update);

                    }
                    else if (payment_type == "card")
                    {
                        var payment_update = Builders<mPayment>.Update
                        .Set(i => i.amount_payed, amount_payed)
                        .Set(i => i.client_app_id, client_app_id)
                        .Set(i => i.artisan_app_id, artisan_app_id)
                        .Set(i => i.paymentType, PaymentType.card)
                        ;

                        payment_col.UpdateOne(i => i._job_id == _job_id, payment_update);

                    }

                }





                //send confirmation to the artisan
                dynamic an = new JObject();
                if (payment_type == "cash") an.type = "cash_payment_recieved";
                if (payment_type == "card") an.type = "card_payment_recieved";
                an._job_id = _job_id;
                an.amount_payed = amount_payed;
                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(an)), 1, false);


                dynamic r = new JObject();
                r.res = "ok";
                r.msg = "payment recieved";
                return JsonConvert.SerializeObject(r);
            }
            catch (Exception ex)
            {
                dynamic r = new JObject();
                r.res = "err";
                r.msg = ex.Message;
                return JsonConvert.SerializeObject(r);
            }
        }


        [Route("open_dispute")]
        public string open_dispute(string _job_id, string reason_for_dispute, string artisan_app_id,string client_app_id)
        {
            try
            {

                //open dipute 
                //send email to admin
                //integrate with crm module

                var dispute = new mDispute();
                dispute.reason_for_dispute = reason_for_dispute;
                dispute._job_id = _job_id;
                dispute.artisan_app_id = artisan_app_id;
                dispute.client_app_id = client_app_id;
                var dispute_col = globals.getDB().GetCollection<mDispute>("mDispute");
                var exist = dispute_col.Find(i => i._job_id == _job_id).FirstOrDefault();
                if (exist == null)//dont  insert multiple for the same
                {
                    dispute_col.InsertOne(dispute);
                }
                else
                {
                    var dispute_update = Builders<mDispute>
                        .Update
                        .Set(i => i.reason_for_dispute, reason_for_dispute)
                        .Set(i => i.artisan_app_id, artisan_app_id)
                        .Set(i => i.client_app_id, client_app_id)
                        ;
                    dispute_col.UpdateOne(i => i._job_id == _job_id, dispute_update);
                }


                //notify the artisan that a dispute has been opened
                dynamic artisan_notification = new JObject();
                artisan_notification.type = "dispute_opened";
                artisan_notification._job_id = _job_id;
                artisan_notification.reason_for_dispute = reason_for_dispute;

                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(artisan_notification)), 1, false);

                //send email notification to the admin to notify him of a dispute
                var email = new globals.emailMessage();
                email.to = globals.supportEmail;
                email.subject = "Dispute notification";
                email.message = System.IO.File.ReadAllText(host.WebRootPath + "/email_views/email_notification.html");
                email.message = email.message.Replace("{{message}}", "A new dispute has been opened, log in to view the dispute");
                Task.Run(() =>
                {
                    globals.sendEmail(email);
                });


                //return response to the client
                dynamic json = new JObject();
                json.res = "ok";
                json.msg = "dispute opened";
                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                dynamic json = new JObject();
                json.res = "err";
                json.msg = ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }


        //add the rating for this artian
        [Route("add_artisan_rating")]
        public string add_artisan_rating(string artisan_app_id, int rating)
        {
            try
            {


                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_rating = new artisanRating();
                artisan_rating.numStars = rating;
                var artisan_update = Builders<mArtisan>.Update.AddToSet(i => i.artisanRating, artisan_rating);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);


                //notify the artisan his rating
                dynamic n = new JObject();
                n.type = "rating_notification";
                n.rating = rating;
                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(n)), 1, false);


                //send reponse to the client
                dynamic json = new JObject();
                json.res = "ok";
                json.msg = "rating saved";
                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                dynamic json = new JObject();
                json.res = "err";
                json.msg = ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }


        //fetch the list of artisans on client listof artisans fragment
        [Route("fetch_the_artisans")]
        public string fetch_the_artisans(int page,int per_page, string city, string skill)
        {

            //supported locations and their geocoordintes
            Dictionary<string, List<double>> supported_locations = new Dictionary<string, List<double>>();
            //supported_locations.Add("Abuja FCT", new List<double>(){ 9.0016626, 7.4219573 } );
            supported_locations.Add("Abuja FCT", new List<double>(){ 9.072264, 7.491302} );
            supported_locations.Add("Port Harcourt", new List<double>(){ 4.824167, 7.033611 } );
            supported_locations.Add("Kano", new List<double>(){ 12.000000, 8.516667 } );
            supported_locations.Add("Lagos", new List<double>(){ 6.465422, 3.406448 } );
            supported_locations.Add("Harare", new List<double>(){ -17.815907, 30.9018361 } );

            //
            var selected_city = supported_locations[city];
            //lat lng
            var point = new GeoJson2DGeographicCoordinates(selected_city[0],selected_city[1]);
            var pnt = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(point);
            var maxDistanceInKm = 100000;//search this radius for the artisan in km
            var filter = Builders<mArtisan>.Filter.Near(i => i.location.coordinates, pnt, maxDistanceInKm);

            //
            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");


            //
            //will return in ranked order of closeness
            var artisans_within_100_km_of_the_city = artisan_col.Find(filter).ToList();

            //collect all artisans who has one or more of the skills and are available and are within 100km range in order of closeness
            var list_of_artisans_with_any_of_these_services_who_are_in_the_city = artisans_within_100_km_of_the_city
                .Where(i => i.skills.Contains(skill)).ToList();


            var artisans = list_of_artisans_with_any_of_these_services_who_are_in_the_city.Skip(per_page*page).Take(per_page);

            //serialize
            JArray ja = new JArray();
            foreach (var a in artisans)
            {
                dynamic artisan = new JObject();
                artisan.artisan_app_id = a.app_id;
                artisan.name = a.name;
                artisan.num_of_jobs = a.numJobs;
                artisan.skills = String.Join(" ", a.skills);
                artisan.hourly_rate = a.hourlyRate;
                artisan.mobile = a.mobile;
                artisan.rating = a.getRating();

                ja.Add(JsonConvert.SerializeObject(artisan));
            }


            //return to client
            return JsonConvert.SerializeObject(ja);

        }

      

        [Route("client_cancel_job")]
        public string client_cancel_job(string _job_id,string reason,string artisan_app_id, string client_app_id)
        {
            try
            {

                //update job status
                var job_col = globals.getDB().GetCollection<mJobs>("mJobs");
                var job_update = Builders<mJobs>
                    .Update
                    .Set(i => i.who_cancelled, Who_cancelled.client)//set who cancelled
                    .Set(i => i.job_status, JobStatus.cancelled)//set status
                    ;
                job_col.UpdateOne(i => i._job_id == _job_id, job_update);//update the job

                //set artisan.busy = false
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update.Set(i => i.busy, false);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);

                //now notify the artisan the job was cancelled
                dynamic artisan_notification = new JObject();
                artisan_notification.type = "job_cancelled";
                artisan_notification._job_id = _job_id;
                artisan_notification.reason_for_cancellation = reason;

                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(artisan_notification)), 1, false);




                //send reponse to the client
                dynamic json = new JObject();
                json.res = "ok";
                json.msg = "job canceled";
                return JsonConvert.SerializeObject(json);

            }
            catch(Exception ex)
            {

                //send reponse to the client
                dynamic json = new JObject();
                json.res = "err";
                json.msg = ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }


        //obsolete
        [Route("save_client_settings")]
        public string save_client_settings(string data)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                string client_app_id = json.app_id;
                string client_mobile = json.mobile;
                string client_mobile_country_code = json.mobile_country_code;

                var client_col = globals.getDB().GetCollection<mClient>("mClient");
                var client_update = Builders<mClient>.Update
                    .Set(i => i.mobile, client_mobile)
                    .Set(i => i.mobile_country_code, client_mobile_country_code);
                client_col.UpdateOne(i => i.app_id == client_app_id, client_update);

                dynamic res = new JObject();
                res.res = "ok";
                res.msg = "saved";
                return JsonConvert.SerializeObject(res);


            }
            catch(Exception ex)
            {
                dynamic res = new JObject();
                res.res = "err";
                res.msg = ex.Message;
                return JsonConvert.SerializeObject(res);
            }

        }


        [Route("client_change_mobile")]
        public string client_change_mobile(string mobile,string client_app_id)
        {
            try
            {
                var client_col = globals.getDB().GetCollection<mClient>("mClient");
                var client_update = Builders<mClient>.Update
                    .Set(i=>i.mobile,mobile)
                    ;
                client_col.UpdateOne(i => i.app_id == client_app_id, client_update);

                dynamic res = new JObject();
                res.res = "ok";
                res.msg = "mobile number changed";
                return JsonConvert.SerializeObject(res);
            }
            catch(Exception ex)
            {
                dynamic res = new JObject();
                res.res = "err";
                res.msg = ex.Message;
                return JsonConvert.SerializeObject(res);
            }
        }






    }//controller


}//namespace
