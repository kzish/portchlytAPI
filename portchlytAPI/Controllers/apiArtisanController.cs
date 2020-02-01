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
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// this controller is for the porchlyt artisan app
/// </summary>
namespace portchlytAPI.Controllers
{
    [Route("apiArtisan")]
    public class apiArtisanController : Controller
    {
        [Route("")]
        public string alive()
        {
            return "alive";
        }


        HostingEnvironment host;
        public apiArtisanController(HostingEnvironment e)
        {
            host = e;
        }

        //todo: remove return true and open this method
        [Route("na")]
        public async Task<bool> create_artisan_sub_bank_account(mArtisan artisan, string account_bank, string account_number,string email)
        {
            //return true;
            ///convert account_bank to a number
            string json_banks_string = System.IO.File.ReadAllText(host.WebRootPath + "/data/list_of_banks_in_nigeria.json");
            dynamic json_banks_json = JsonConvert.DeserializeObject(json_banks_string);
            List<mBank> list_of_banks_in_nigeria = json_banks_json.Banks.ToObject(typeof(List<mBank>));
            mBank selected_bank = list_of_banks_in_nigeria.Where(i => i.Name == account_bank).FirstOrDefault();

            //create the sub account for the artisan
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ContentType", "application/json");

            //create json object to post
            dynamic json_post = new JObject();
            json_post.account_bank = selected_bank.Code;
            json_post.account_number = account_number;
            json_post.business_name = artisan.app_id;
            json_post.business_mobile = artisan.mobile;
            json_post.seckey = globals.rave_flutter_wave_secret_key;
            json_post.split_type = globals.rave_flutter_wave_split_type;
            json_post.split_value = globals.rave_flutter_wave_split_value;
            json_post.country = globals.rave_flutter_wave_country;
            json_post.business_email = email;
            string json_data = json_post.ToString();

            //live or sandbox
            var data_ = await client.PostAsync("https://api.ravepay.co/v2/gpx/subaccounts/create", new StringContent(json_data, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();
            //var data_ = await client.PostAsync("https://ravesandboxapi.flutterwave.com/v2/gpx/subaccounts/create?", new StringContent(json_data, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();
            dynamic json_response = JsonConvert.DeserializeObject(data_);
            string status = json_response.status;
            if (status == "success")
            {
                //proceed, sub account created successfully
                //update artisan sub account id
                string subaccunt_id = json_response.data.subaccount_id;
                string subaccunt_id_id = json_response.data.id;
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update
                    .Set(i => i.subaccount_id, subaccunt_id)
                    .Set(i => i.subaccount_id_id, subaccunt_id_id);
                artisan_col.UpdateOne(i => i.app_id == artisan.app_id, artisan_update);
                return true;
            }
            else// if (status == "error")
            {
                //return to artisan with error message
                dynamic res_ = new JObject();
                res_.res = "err";
                res_.msg = json_response.message;
                return false;
            }
        }
        [Route("na")]
        public async Task<bool> update_artisan_sub_bank_account(mArtisan artisan, string account_bank, string account_number,string email)
        {
            //return true;
            ///convert account_bank to a number
            string json_banks_string = System.IO.File.ReadAllText(host.WebRootPath + "/data/list_of_banks_in_nigeria.json");
            dynamic json_banks_json = JsonConvert.DeserializeObject(json_banks_string);
            List<mBank> list_of_banks_in_nigeria = json_banks_json.Banks.ToObject(typeof(List<mBank>));
            mBank selected_bank = list_of_banks_in_nigeria.Where(i => i.Name == account_bank).FirstOrDefault();

            //create the sub account for the artisan
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ContentType", "application/json");

            //create json object to post
            dynamic json_post = new JObject();
            json_post.account_bank = selected_bank.Code;
            json_post.account_number = account_number;
            json_post.business_name = artisan.app_id;
            json_post.business_mobile = artisan.mobile;
            json_post.seckey = globals.rave_flutter_wave_secret_key;
            json_post.split_type = globals.rave_flutter_wave_split_type;
            json_post.split_value = globals.rave_flutter_wave_split_value;
            json_post.country = globals.rave_flutter_wave_country;
            json_post.id = artisan.subaccount_id_id;
            json_post.business_email = email;
            string json_data = json_post.ToString();

            //live or sandbox
            var data_ = await client.PostAsync("https://api.ravepay.co/v2/gpx/subaccounts/edit", new StringContent(json_data, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();
            //var data_ = await client.PostAsync("https://ravesandboxapi.flutterwave.com/v2/gpx/subaccounts/edit?", new StringContent(json_data, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync();
            dynamic json_response = JsonConvert.DeserializeObject(data_);
            string status = json_response.status;
            if (status == "success")
            {
                //proceed, sub account created successfully
                //update artisan sub account id
                string subaccunt_id = json_response.data.subaccount_id;
                string subaccunt_id_id = json_response.data.id;
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update
                    .Set(i => i.subaccount_id, subaccunt_id)
                    .Set(i => i.subaccount_id_id, subaccunt_id_id);
                artisan_col.UpdateOne(i => i.app_id == artisan.app_id, artisan_update);
                return true;
            }
            else// if (status == "error")
            {
                //return to artisan with error message
                dynamic res_ = new JObject();
                res_.res = "err";
                res_.msg = json_response.message;
                return false;
            }
        }


        /// <summary>
        /// register the artisan via the mobile app
        /// </summary>
        /// <param name="artisan"></param>
        /// <returns></returns>
        [Route("artisanRegistration")]
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
                /*WebClient wc = new WebClient();
                wc.QueryString.Add("mobile", artisan.mobile);
                wc.QueryString.Add("msg", "Your OTP is: " + OTP);
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
                     wc.QueryString.Add("mobile", artisan.mobile);
                     wc.QueryString.Add("msg", "Your OTP is: " + exist.otp);
                     var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                     var responseString = UnicodeEncoding.UTF8.GetString(data_);
                     */
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
        [Route("artisanConfirmRegistration")]
        public JsonResult artisanConfirmRegistration(string mobile)
        {
            try
            {
                var update = Builders<mArtisan>.Update.Set(x => x.registered, true);
                var acol = globals.getDB().GetCollection<mArtisan>("mArtisan");
                acol.UpdateOne(x => x.mobile == mobile, update);//update this artisans registration status
                //also check if this artisan is blocked and send this information to the artisan
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan = artisan_col.Find(x => x.mobile == mobile).FirstOrDefault();
                string account_status = "active";
                if (!artisan.enabled) account_status = "blocked";
                return Json(new { res = "ok", msg = "registration confirmed", account_status = account_status });
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
                List<string> artisan_skills_ = ((string)json.artisan_skills).Split(".").ToList<string>();
                List<string> artisan_skills = new List<string>();
                foreach (string skill in artisan_skills_)
                {
                    artisan_skills.Add(skill.Trim());
                }
                var asr_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");
                //now update the service request by removing these skills since they have been selected already
                var update = Builders<mArtisanServiceRequest>.Update
                    .PullAll(i => i.requested_services, artisan_skills
                    );
                var original_request = asr_col.Find(i => i.client_request_id == request_id).FirstOrDefault();
                asr_col.UpdateOne(i => i.client_request_id == request_id, update);//update it
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var the_chosen_artisan = artisan_col.Find(i => i.app_id == artisan_app_id).FirstOrDefault();
                var client_col = globals.getDB().GetCollection<mClient>("mClient");
                var the_client_who_requested_the_service = client_col.Find(i => i.app_id == client_app_id).FirstOrDefault();


                //create and record a new job 
                var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
                var new_job = new mJobs();
                new_job._job_id = _job_id;
                new_job.client_mobile = the_client_who_requested_the_service.mobile;
                new_job.client_app_id = the_client_who_requested_the_service.app_id;
                new_job.artisan_app_id = the_chosen_artisan.app_id;
                new_job.start_time = DateTime.Now.ToString();
                new_job.end_time = null;
                new_job.country = "NG";
                new_job.category = String.Join(":", artisan_skills);
                new_job.city_or_state = null;
                new_job.geoLocationLatitude = original_request.lat.ToString();
                new_job.geoLocationLongitude = original_request.lon.ToString();
                new_job.address = null;
                new_job.price = 0.0;
                new_job.description = String.Join(":", original_request.requested_services);
                new_job.tasks = new List<mTask>();
                new_job.job_status = JobStatus.opened;
                jobs_col.InsertOne(new_job);


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
                //string request_id = json.request_id;
                //string artisan_app_id = json.artisan_app_id;
                string client_app_id = json.client_app_id;

                //save this data to the data base for records sake
                //var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");

                dynamic client_notification = new JObject();
                client_notification.data = Regex.Unescape(data);
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
        public async Task<string> update_artisan_details(string data)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");

                string artisan_app_id = json.artisan_app_id;
                string name = json.name;
                string email = json.email;
                string account_bank = json.account_bank;
                string account_number = json.account_number;
                double hourlyRate = json.hourlyRate;
                string mobile = json.mobile;


                var artisan = artisan_col.Find(i => i.app_id == artisan_app_id).FirstOrDefault();

                if (String.IsNullOrEmpty(artisan.account_bank))
                {
                    //create banking details
                    await create_artisan_sub_bank_account(artisan, account_bank, account_number,email);
                }
                else
                {
                    //update banking details
                    await update_artisan_sub_bank_account(artisan, account_bank, account_number,email);
                }




                var artisan_update = Builders<mArtisan>.Update
                    .Set(i => i.name, name)
                    .Set(i => i.email, email)
                    .Set(i => i.hourlyRate, hourlyRate)
                    .Set(i => i.account_bank, account_bank)
                    .Set(i => i.account_number, account_number)
                    .Set(i => i.mobile, mobile)
                    ;

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

                //update the payment to say that the artisan has accepted the payment
                var payment_col = globals.getDB().GetCollection<mPayment>("mPayment");
                var payment_update = Builders<mPayment>.Update.Set(i => i.artisan_accepted, true);
                payment_col.UpdateOne(i => i._job_id == _job_id, payment_update);


                //update artisan status
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>
                    .Update
                    .Set(i => i.busy, false)//update artisan from busy to not busy
                    .Inc(i => i.numJobs, 1);//increment also the number of jobs
                artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);

                //check the amount of cash on hand for the client
                var payments_col = globals.getDB().GetCollection<mPayment>("mPayment");
                //get the cash amount that is not remitted
                var payments_cash_total = payments_col.Find(i => i.artisan_app_id == artisan_app_id && i.paymentType == PaymentType.cash && !i.remitted).ToList().Sum(i => i.amount_payed);

                if (payments_cash_total >= 5000)
                {
                    //update the artisan to be busy so he will not get anymore jobs untill he remits the cash on hand
                    //diable the artisan account
                    artisan_update = Builders<mArtisan>
                    .Update
                    .Set(i => i.busy, true)//update artisan from not busy to busy
                    .Set(i => i.enabled, false)//set enabled
                    .Set(i => i.reason_for_enable, "N5000 > cash limit reached");//set reason
                    artisan_col.UpdateOne(i => i.app_id == artisan_app_id, artisan_update);

                    //also send him a message to inform him of this
                    dynamic notification = new JObject();
                    notification.type = "general_notification";
                    //notification.general_notification_message = "You have accumulated "+globals.getLocalCurrencyNG(5000)+" and must remit this money to the company, you will not be able to get any jobs until you have remitted the money";
                    notification.general_notification_message = "You have accumulated N5000 and must remit this money to the company, you will not be able to get any jobs until you have remitted the money";
                    globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(notification)), 1, false);

                    dynamic status = new JObject();
                    status.type = "account_status";
                    status.msg = "remit_cash";
                    globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(status)), 1, false);
                }

                //update the job
                var job_col = globals.getDB().GetCollection<mJobs>("mJobs");
                var job_col_update = Builders<mJobs>
                    .Update
                    .Set(i => i.end_time, DateTime.Now.ToString())
                    .Set(i => i.job_status, JobStatus.closed);
                job_col.UpdateOne(i => i._job_id == _job_id, job_col_update);


                //send acknowledgement to the client
                dynamic an = new JObject();
                an.type = "cash_payment_accepted_by_artisan";
                an._job_id = _job_id;
                globals.mqtt.Publish(client_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(an)), 1, false);

                //response
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
        public String fetch_extra_jobs(string artisan_skills, int page, int per_page)
        {
            try
            {

                List<string> skills_ = artisan_skills.Split('.').ToList();
                List<string> skills = new List<string>();
                foreach (string s in skills_)
                {
                    skills.Add(s.Trim());
                }
                ///get the srvices which are requered to inform in the future
                var service_request_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest").Find(i => i.notify_client_in_the_future).ToList();
                var jobs = service_request_col
                    .Where(i => i.requested_services.Intersect(skills).Any() && i.notify_client_in_the_future)
                    .OrderByDescending(i=>i.time_of_request)
                    .ToList()
                    .Skip(page * per_page)
                    .Take(per_page)
                    ;
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
                /*WebClient wc = new WebClient();
                wc.QueryString.Add("mobile", artisan.mobile);
                wc.QueryString.Add("msg", "Your OTP is: " + artisan.otp);
                var data_ = wc.UploadValues(globals.cloudsms_api + "/sendSMS", "POST", wc.QueryString);
                var responseString = UnicodeEncoding.UTF8.GetString(data_);
                */

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
                var job_update = Builders<mJobs>
                    .Update
                    .Set(i => i.who_cancelled, Who_cancelled.artisan)//set who cancelled
                    .Set(i => i.job_status, JobStatus.cancelled)//set status
                    ;
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

        //static method to fetch earnings
        public static string fetch_earnings_data_(string artisan_app_id)
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

        [Route("fetch_earnings_data")]
        public string fetch_earnings_data(string artisan_app_id)
        {
            return fetch_earnings_data_(artisan_app_id);
        }

        [Route("serve_artisan_profile_picture")]
        public FileResult serve_artisan_profile_picture(string artisan_app_id)
        {
            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var artisan = artisan_col.Find(i => i.app_id == artisan_app_id).FirstOrDefault();
            var image = Path.Combine(host.WebRootPath, artisan.image);
            return File(image, "image/jpeg");
        }



    }//controller
}//namespace
