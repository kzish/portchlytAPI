using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using portchlytAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

/// <summary>
/// this controller is for the porchlyt artisan app
/// </summary>
namespace portchlytAPI.Controllers
{
    [Route("apiArtisan")]
    public class apiArtisanController : Controller
    {



        HostingEnvironment host;
        public apiArtisanController(HostingEnvironment e)
        {
            host = e;
        }



        /// <summary>
        /// register the artisan via the mobile app
        /// </summary>
        /// <param name="artisan"></param>
        /// <returns></returns>
        [HttpPost("artisanRegistration")]
        public JsonResult artisanRegistration(string data)
        {
            mArtisan artisan = JsonConvert.DeserializeObject<mArtisan>(data);//convert data to object
            //check mobile exist
            var acol = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var exist = acol.Find(x => x.mobile == artisan.mobile).FirstOrDefault();
            if (exist == null)//insert this one
            {
                //generate random 5 digit otp number
                Random r = new Random();
                var x = r.Next(0, 100000);
                string OTP = x.ToString("00000");
                artisan.otp = OTP;
                //insert new artisan into the database
                acol.InsertOne(artisan);

                //send bulk sms to user via the bulk sms api with the OTP
                WebClient wc = new WebClient();
                wc.QueryString.Add("mobile", artisan.mobile);
                wc.QueryString.Add("msg", "Your OTP is: " + OTP);
                var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                var responseString = UnicodeEncoding.UTF8.GetString(data_);


                return Json(new { res = "ok", msg = "registration complete", otp = OTP });
            }
            else if (!exist.registered)//simply resend the otp and update the information
            {
                try
                {
                    //send bulk sms to user via the bulk sms api with the OTP
                    WebClient wc = new WebClient();
                    wc.QueryString.Add("mobile", artisan.mobile);
                    wc.QueryString.Add("msg", "Your OTP is: " + exist.otp);
                    var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                    var responseString = UnicodeEncoding.UTF8.GetString(data_);

                    artisan._id = exist._id;//this will maintain the id
                    artisan.otp = exist.otp;//this maintains the otp also
                    acol.ReplaceOne(i => i.mobile == artisan.mobile, artisan);//replace it thus updating it incase any information is changed
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
        [HttpPost("artisanConfirmRegistration")]
        public JsonResult artisanConfirmRegistration(string data)
        {
            try
            {
                mArtisan m = JsonConvert.DeserializeObject<mArtisan>(data);
                var update = Builders<mArtisan>.Update.Set(x => x.registered, true);
                var acol = globals.getDB().GetCollection<mArtisan>("mArtisan");
                acol.UpdateOne(x => x.mobile == m.mobile, update);//update this artisans registration status
                return Json(new { res = "ok", msg = "registration confirmed" });
            }
            catch (Exception ex)
            {
                return Json(new { res = "err", msg = ex.Message });
            }
        }

        /// <summary>
        /// /method to change the availability of the artisan set this in the database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("artisanSetUnSetAvailability")]
        public JsonResult artisanSetUnSetAvailability(string data)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                string artisan_app_id = json.artisan_app_id;
                bool available = bool.Parse((string)json.available);
                var acol = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var update = Builders<mArtisan>.Update.Set(x => x.on_duty, available);
                acol.UpdateOne(i => i.app_id == artisan_app_id, update);//update the on_duty status
                return Json(new { res = "ok", msg = "availability set" });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    res = "err",
                    msg = ex.Message
                });

            }
        }

        /// <summary>
        /// function to update the coordinates of the artsian using his gps coordinates
        /// this method is updated via the mqtt push to keep power sage low
        /// </summary>
        /// <param name="data"></param>
        public static void artisanLocationUpdate(string data)
        {

            try
            {
                //unpacking the data
                dynamic json = JsonConvert.DeserializeObject(data);
                double lat = json.lat;
                double lng = json.lng;
                string artisan_app_id = json.artisan_app_id;
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                Location loc = new Location(lat, lng);
                var update = Builders<mArtisan>.Update.Set(i => i.location, loc);//set the new coordinates
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, update);
            }
            catch (Exception ex)
            {

            }
        }



        [Route("SetReasonForNotResponding")]
        public JsonResult SetReasonForNotResponding(string data)
        {
            try
            {
                dynamic artisan_response = JsonConvert.DeserializeObject(data);
                string request_id = artisan_response.request_id;
                string response = artisan_response.response;
                string artisan_app_id = artisan_response.artisan_app_id;
                var response_col = globals.getDB().GetCollection<mArtisanReasonForNotResponding>("mArtisanReasonForNotResponding");

                var res = new mArtisanReasonForNotResponding();
                res.artisan_app_id = artisan_app_id;
                res.reason_for_not_responding_to_request = response;
                res.request_id = request_id;

                response_col.InsertOne(res);
                return Json(new { res = "ok", msg = "response recorded" });
            }
            catch (Exception ex)
            {
                return Json(new { res = "err", msg = ex.Message });
            }
        }



        /// <summary>
        /// the artisan has accepted the request 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("ArtisanAcceptRequest")]
        public string ArtisanAcceptRequest(string data)
        {
            try
            {

                dynamic json = JsonConvert.DeserializeObject(data);
                string request_id = json.request_id;
                string _job_id = json._job_id;//get the job id
                string artisan_app_id = json.artisan_app_id;
                string client_app_id = json.client_app_id;
                List<string> artisan_skills = ((string)json.artisan_skills).Split(":").ToList<string>();
                var asr_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
                //now update the service request by removing these skill since they have been selected already
                var update = Builders<mArtisanServiceRequest>.Update
                    .PullAll(i => i.requested_services, artisan_skills
                    );
                var original_request = asr_col.Find(i => i.client_request_id == request_id).FirstOrDefault();
                asr_col.UpdateOne(i => i.client_request_id == request_id, update);//update it
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var the_chosen_artisan = artisan_col.Find(i => i.app_id == artisan_app_id).FirstOrDefault();
                var client_col = globals.getDB().GetCollection<mClient>("mClient");
                var the_client_who_requested_the_service = client_col.Find(i => i.app_id == client_app_id).FirstOrDefault();

                //send message to the client that we have found this artisan
                //notify the client that an artisan has accepted the job
                dynamic notification_for_client = new JObject();
                notification_for_client.request_id = request_id;
                notification_for_client.type = "artisan_search_request_notification";
                notification_for_client.msg = "job_accepted";
                notification_for_client.artisan_rating = the_chosen_artisan.getRating();
                notification_for_client.jobs_accepted = String.Join(":", artisan_skills.Intersect(original_request.requested_services));//send the accepted jobs to the client
                notification_for_client.artisan_json_data = JsonConvert.SerializeObject(the_chosen_artisan);
                //create and add job data for the client response too
                //this response is for the client
                dynamic job_data = new JObject();
                job_data.request_id = request_id;
                job_data._job_id = _job_id;//set the job id
                job_data.artisan_app_id = artisan_app_id;
                job_data.artisan_mobile = the_chosen_artisan.mobile;
                job_data.artisan_name = the_chosen_artisan.name;//the name
                job_data.requested_skills = String.Join(":", artisan_skills.Intersect(original_request.requested_services));//send the accepted jobs to the artisan
                job_data.latitude = original_request.lat;
                job_data.longitude = original_request.lon;
                notification_for_client.job_data = JsonConvert.SerializeObject(job_data);//add the job data to the notification item
                globals.mqtt.Publish(client_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(notification_for_client)), 1, false);

                //this response is for the artisan
                dynamic responce = new JObject();
                responce.res = "ok";
                responce.msg = "request accepted";
                responce.request_id = request_id;
                responce.client_app_id = client_app_id;
                responce.requested_skills = String.Join(":", artisan_skills.Intersect(original_request.requested_services));//send the accepted jobs to the artisan
                responce.latitude = original_request.lat;
                responce.longitude = original_request.lon;
                responce.client_mobile = the_client_who_requested_the_service.mobile;


                //chnage the status of the artisan to be bsy until the current job is completed
                var artisan_update = Builders<mArtisan>.Update.Set(i => i.busy, true);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);
                return JsonConvert.SerializeObject(responce);
            }
            catch (Exception ex)
            {
                dynamic response = new JObject();
                response.res = "err";
                response.msg = ex.Message;
                return JsonConvert.SerializeObject(response);

            }

        }



        //function to forward the bill to the client and also save the bill for records keepin
        [Route("forward_bill_to_client")]
        public string forward_bill_to_client(string data)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                string request_id = json.request_id;
                string artisan_app_id = json.artisan_app_id;
                string client_app_id = json.client_app_id;

                //save this data to the data base for records sake
                var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");

                dynamic client_notification = new JObject();
                client_notification.data = data;
                client_notification.type = "client_job_bill_notification";

                //forward all this data to the client as a notification
                globals.mqtt.Publish(client_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(client_notification)), 1, false);


                //respond to the artisan request
                dynamic res = new JObject();
                res.res = "ok";
                res.msg = "saved";
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                //send the artisan a response
                dynamic res = new JObject();
                res.res = "err";
                res.msg = ex.Message;
                return JsonConvert.SerializeObject(res);
            }
        }



        [Route("update_artisan_details")]
        public string update_artisan_details(string data)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                string artisan_app_id = json.artisan_app_id;
                string name = json.name;
                string email = json.email;
                double hourlyRate = json.hourlyRate;
                var artisan_update = Builders<mArtisan>.Update
                    .Set(i => i.name, name)
                    .Set(i => i.email, email)
                    .Set(i => i.hourlyRate, hourlyRate);

                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);

                dynamic res = new JObject();
                res.res = "ok";
                res.msg = "details updated";
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                dynamic res = new JObject();
                res.res = "err";
                res.msg = ex.Message;
                return JsonConvert.SerializeObject(res);
            }

        }


        [Route("upload_image_from_mobile")]
        public string upload_image_from_mobile(IFormCollection file, string artisan_app_id)
        {
            try
            {
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan = artisan_col.Find(i => i.app_id == artisan_app_id).FirstOrDefault();
                var filename = Path.Combine(host.WebRootPath, "profile_pictures/" + artisan_app_id + Path.GetExtension(file.Files[0].FileName));
                using (var stream = new FileStream(filename, FileMode.Create))
                {
                    file.Files[0].CopyTo(stream);
                    stream.Close();
                    stream.Dispose();
                }
                var img_url = "/profile_pictures/" + Path.GetFileName(filename);
                var artisan_update = Builders<mArtisan>.Update.Set(i => i.image, img_url);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);
                dynamic json = new JObject();
                json.res = "ok";
                json.msg = "picture saved";
                return JsonConvert.SerializeObject(json);
            }
            catch (Exception ex)
            {
                dynamic json = new JObject();
                json.res = "err";
                json.msg = ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }//.upload_image_from_mobile




        [Route("remove_profile_picture")]
        public string remove_profile_picture(string artisan_app_id)
        {
            try
            {
                //the image is the app_id.jpg
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update.Set(i => i.image, null);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);//
                System.IO.File.Delete(host.WebRootPath + "/profile_pictures/" + artisan_app_id + ".jpg");//delete the file from the system
                dynamic res = new JObject();
                res.res = "ok";
                res.msg = "picture removed";
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                dynamic res = new JObject();
                res.res = "err";
                res.msg = ex.Message;
                return JsonConvert.SerializeObject(res);
            }
        }


        //confirm cash payment recieved
        [Route("artisan_accept_cash_payment")]
        public string artisan_accept_cash_payment(string _job_id, string client_app_id, string artisan_app_id)
        {
            try
            {

                //
                var payment_col = globals.getDB().GetCollection<mPayment>("mPayment");
                var payment_update = Builders<mPayment>.Update.Set(i => i.artisan_accepted, true);
                payment_col.UpdateOne(i => i._job_id == _job_id, payment_update);

                //upadte artisan from busy to not busy
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update.Set(i => i.busy, false);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);


                //send acknowledgement to the client
                dynamic an = new JObject();
                an.type = "cash_payment_accepted_by_artisan";
                an._job_id = _job_id;
                globals.mqtt.Publish(client_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(an)), 1, false);

                //
                dynamic res = new JObject();
                res.res = "ok";
                res.msg = "payment confirmed";
                return JsonConvert.SerializeObject(res);

            }
            catch (Exception ex)
            {
                dynamic res = new JObject();
                res.res = "err";
                res.msg = ex.Message;
                return JsonConvert.SerializeObject(res);
            }
        }


        [Route("fetch_extra_jobs")]
        public String fetch_extra_jobs(string artisan_skills)
        {
            try
            {

                List<string> skills = artisan_skills.Split(':').ToList();
                ///get the srvices which are requered to inform in the future
                var service_request_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest").Find(i => i.notify_client_in_the_future).ToList();
                var jobs = service_request_col.Where(i => i.requested_services.Intersect(skills).Any()).ToList();
                return JsonConvert.SerializeObject(jobs);

            }
            catch (Exception ex)
            {
                return "err";
            }
        }




        ///artian login instead of sigining up since this artisan already has loged in
        ///this method will return all the details and current jobs etc
        ///in future the artian must be able to donwload all his previous information from the cloud
        [Route("artisan_login")]
        public string artisan_login(string mobile)
        {
            try
            {
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan = artisan_col.Find(i => i.mobile == mobile).FirstOrDefault();


                if (artisan == null)
                {

                    dynamic r = new JObject();
                    r.res = "ok_not_exist";
                    r.otp = "none";
                    r.msg = "artisan does not exist";
                    return JsonConvert.SerializeObject(r);//return

                }

                //use the sms api ot send the otp
                //send bulk sms to user via the bulk sms api with the OTP
                 WebClient wc = new WebClient();
                 wc.QueryString.Add("mobile", artisan.mobile);
                 wc.QueryString.Add("msg", "Your OTP is: " + artisan.otp);
                 var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                 var responseString = UnicodeEncoding.UTF8.GetString(data_);
                 

                {
                    dynamic r = new JObject();
                    r.res = "ok";
                    r.otp = artisan.otp;
                    r.msg = "successfully signed in";
                    r.artisan_data = JsonConvert.SerializeObject(artisan);
                    return JsonConvert.SerializeObject(r);
                }
            }
            catch (Exception ex)
            {
                dynamic r = new JObject();
                r.res = "err";
                r.msg = ex.Message;
                r.otp = "none";
                return JsonConvert.SerializeObject(r);
            }
        }

        [Route("artisan_cancel_job")]
        public string artisan_cancel_job(string _job_id, string reason, string artisan_app_id, string client_app_id)
        {
            try
            {

                //update job status
                var job_col = globals.getDB().GetCollection<mJobs>("mJobs");
                var job_update = Builders<mJobs>.Update.Set(i => i.who_cancelled, Who_cancelled.artisan);
                job_col.UpdateOne(i => i._job_id == _job_id, job_update);//update the job


                //set artisan.busy = false
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update.Set(i => i.busy, false);
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);



                //now notify the client the job was cancelled
                dynamic artisan_notification = new JObject();
                artisan_notification.type = "job_cancelled";
                artisan_notification._job_id = _job_id;
                artisan_notification.reason_for_cancellation = reason;

                globals.mqtt.Publish(client_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(artisan_notification)), 1, false);




                //send reponse to the client
                dynamic json = new JObject();
                json.res = "ok";
                json.msg = "job canceled";
                return JsonConvert.SerializeObject(json);

            }
            catch (Exception ex)
            {

                //send reponse to the client
                dynamic json = new JObject();
                json.res = "err";
                json.msg = ex.Message;
                return JsonConvert.SerializeObject(json);
            }
        }




    }//controller
}//namespace
