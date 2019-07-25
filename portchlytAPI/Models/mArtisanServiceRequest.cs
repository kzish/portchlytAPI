using System;
using System.Collections.Generic;
using MongoDB.Driver;
namespace portchlytAPI.Models
{
    //this class model represents a service request from a client
    public class mArtisanServiceRequest
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();//id of this object
        public string client_request_id { get; set; }//the id of the request from the client
        public List<string> requested_services { get; set; } = new List<string>();//the names of the services eg cleaning services
        public string client_app_id { get; set; }//the id of the clients application so we can notify him as well
        public string client_mobile { get; set; }//te mobile number used by the client
        public double lat { get; set; }//the latitude will be used to show the location on the map
        public double lon { get; set; }//the longitude will be used to show the location on the map
        public DateTime time_of_request { get; set; } = DateTime.Now;//the time we first notified this artisan
        public bool notify_client_in_the_future { get; set; }//notify the client in the future if this artian is now available


      
       

    }

}






