using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDB.Driver;
using portchlytAPI.Models;

namespace portchlytAPI.Controllers
{
    [Route("apiAdmin")]
    public class apiAdminController : Controller
    {


        //update_artisan_services from the admin console
        [Route("update_artisan_services")]
        public string update_artisan_services(string artisan_app_id, List<string> artisan_skills)
        {
            try
            {

                dynamic json = new JObject();
                json.type = "update_artisan_services";
                json.services = String.Join(":", artisan_skills);

                //save into database
                var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var artisan_update = Builders<mArtisan>.Update.Set(i=>i.skills,artisan_skills);
                artisan_col.UpdateOne(i=>i.app_id==artisan_app_id,artisan_update);

                //send to artisan
                globals.mqtt.Publish(artisan_app_id, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json)), 1, false);
                return "ok";
            }
            catch (Exception ex)
            {
                return "err";
            }
        }



        [Route("get_address_from_geolocation")]
        public String get_address_from_geolocation(double latitude, double longitude)
        {
            return globals.get_address_from_geolocation(latitude, longitude);
        }



    }//controller
}//namespace
