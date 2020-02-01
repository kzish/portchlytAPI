using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;
using portchlytAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSite.Controllers
{
    [Route("Search")]
    public class SearchController : Controller
    {
        /// <summary>
        /// method to search for an artian with the correct services he/she offers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("findServiceArtisan")]
        public IActionResult findServiceArtisan(double lat, double lon,string services)
        {
            try
            {
                var point = new GeoJson2DGeographicCoordinates(lat, lon);
                var pnt = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(point);
                var maxDistanceInKm = 100000;//search this radius for the artisan in km
                var filter = Builders<mArtisan>.Filter.Near(i => i.location.coordinates, pnt, maxDistanceInKm);
                var acol = globals.getDB().GetCollection<mArtisan>("mArtisan");
                var ccol = globals.getDB().GetCollection<mClient>("mClient");
                //var client = ccol.Find(i => i.app_id == client_app_id).FirstOrDefault();//fetch the client in question
                var asr_col = globals.getDB().GetCollection<mArtisanServiceRequest>("mArtisanServiceRequest");

                //artisans within 100 km of the client
                //will return in ranked order of closeness
                var artisans_within_100_km_of_the_client = acol.Find(filter).ToList();
                var services_list = new List<string> { services };
                //collect all artisans who has one or more of the skills and are available and are within 100km range in order of closeness
                var list_of_artisans_with_any_of_these_services_who_are_availalable = artisans_within_100_km_of_the_client
                    .Where( i => i.skills != null && i.skills.Intersect(services_list).Any() )
                     .ToList()
                     .OrderByDescending(x => x.skills.Intersect(services_list).Count())//order by who has the most relevant jobs to this search
                     .ToList()
                     ;

                return PartialView("_ListArtisans", list_of_artisans_with_any_of_these_services_who_are_availalable);
            }
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message, res = "err" });
            }
        }



        [Route("get_address_from_geolocation")]
        public async Task<string> get_address_from_geolocation(double latitude, double longitude)
        {
            return await globals.get_address_from_geolocation(latitude, longitude);
        }


    }
}
