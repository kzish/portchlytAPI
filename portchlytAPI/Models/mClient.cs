using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portchlytAPI.Models
{
    /// <summary>
    /// this is the client on the system ie the buyer the renter
    /// </summary>
    public class mClient
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public string name { get; set; }
        public string surname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string physical_address { get; set; }
        public bool registered { get; set; }
        public string otp { get; set; }
        public String app_id;//the app id of this client
        public bool synced { get; set; }

    }
}
