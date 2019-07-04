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
                 wc.QueryString.Add("user_id", globals.cloudsms_user_id);
                 wc.QueryString.Add("mobile", artisan.mobile);
                 wc.QueryString.Add("msg", "Your porchlyt OTP is: " + OTP);
                 var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                 var responseString = UnicodeEncoding.UTF8.GetString(data_);
                 */
                return Json(new { res = "ok", msg = "registration complete", otp = OTP });
            }
            else if (!exist.registered)//simply resend the otp and update the information
            {
                try
                {
                    //send bulk sms to user via the bulk sms api with the OTP
                    /* WebClient wc = new WebClient();
                     wc.QueryString.Add("user_id", globals.cloudsms_user_id);
                     wc.QueryString.Add("mobile", artisan.mobile);
                     wc.QueryString.Add("msg", "Your porchlyt OTP is: " + OTP);
                     var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                     var responseString = UnicodeEncoding.UTF8.GetString(data_);
                     */

                    client._id = exist._id;//this will maintain the id
                    client.otp = exist.otp;//this maintains the otp also
                    acol.ReplaceOne(i => i.mobile == client.mobile, client);//replace it tus updating it incase any information is changed
                    return Json(new { res = "ok", msg = "registration complete", otp = exist.otp });
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
                return Json(new { res = "ok", msg = "registration confirmed" });
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
                var maxDistanceInKm = 100;//search this radius for the artisan in km
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
            if (asr==null||asr.requested_services.Count == 0)
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
                }else if(action_to_take=="ignore")
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
            var image = System.IO.File.OpenRead(host.WebRootPath+"/profile_pictures/"+artisan_app_id+".jpg");
            return File(image, "image/jpeg");
        }


        //make payment and indicate the payment type
        [Route("make_payment_for_artisan")]
        public string make_payment_for_artisan(string _job_id,double amount_payed,string client_app_id, string artisan_app_id,string payment_type)
        {
            try
            {
                //
                var payment_col = globals.getDB().GetCollection<mPayment>("mPayment");
                var exist = payment_col.Find(i => i._job_id == _job_id).FirstOrDefault();
                if(exist==null)
                {

                    //inserting


                    var payment = new mPayment();
                    payment._job_id = _job_id;
                    payment.amount_payed = amount_payed;
                    payment.client_app_id = client_app_id;
                    payment.artisan_app_id = artisan_app_id;

                    if(payment_type=="cash")
                    {
                        payment.paymentType = PaymentType.cash;
                    }
                    else if(payment_type == "card")
                    {
                        payment.paymentType = PaymentType.card;
                    }



                    //
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
                if(payment_type=="cash")an.type = "cash_payment_recieved";
                if(payment_type=="card")an.type = "card_payment_recieved";
                an._job_id = _job_id;
                an.amount_payed = amount_payed;
                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(an)), 1, false);


                dynamic r = new JObject();
                r.res = "ok";
                r.msg = "payment recieved";
                return JsonConvert.SerializeObject(r);
            }
            catch(Exception ex)
            {
                dynamic r = new JObject();
                r.res = "err";
                r.msg = ex.Message;
                return JsonConvert.SerializeObject(r);
            }
        }


        [Route("open_dispute")]
        public string open_dispute(string _job_id,string reason_for_dispute,string artisan_app_id)
        {
            try
            {

                //open dipute 
                //send email to admin
                //integrate with crm module

                var dispute = new mDispute();
                dispute.reason_for_dispute = reason_for_dispute;
                dispute._job_id = _job_id;
                var dispute_col = globals.getDB().GetCollection<mDispute>("mDispute");
                var exist = dispute_col.Find(i => i._job_id == _job_id).FirstOrDefault();
                if (exist == null)//dont  insert multiple for the same
                {
                    dispute_col.InsertOne(dispute);
                }
                else
                {
                    var dispute_update = Builders<mDispute>.Update.Set(i => i.reason_for_dispute, reason_for_dispute);
                    dispute_col.UpdateOne(i => i._job_id == _job_id, dispute_update);
                }


                //notify the artisan that a dispute has been opened
                dynamic artisan_notification = new JObject();
                artisan_notification.type = "dispute_opened";
                artisan_notification._job_id = _job_id;
                artisan_notification.reason_for_dispute = reason_for_dispute;

                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(artisan_notification)), 1, false);



                //return response to the client
                dynamic json = new JObject();
                json.res = "ok";
                json.msg = "dipute opened";
                return JsonConvert.SerializeObject(json);
            }
            catch(Exception ex)
            {
                dynamic json = new JObject();
                json.res = "err";
                json.msg = ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }


        //add the rating for this artian
        [Route("add_artisan_rating")]
        public string add_artisan_rating(string artisan_app_id,int rating)
        {
            try
            {


                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update.AddToSet(i=>i.artisanRating,rating);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);


                //notify the artisan his rating
                dynamic n = new JObject();
                n.type = "rating_notification";
                n.rating = rating;
                globals.mqtt.Publish(artisan_app_id,Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(n)),1,false);


                //send reponse to the client
                dynamic json = new JObject();
                json.res = "ok";
                json.msg = "rating saved";
                return JsonConvert.SerializeObject(json);
            }
            catch(Exception ex)
            {
                dynamic json = new JObject();
                json.res = "err";
                json.msg = ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }


    }//controller


}//namespace
