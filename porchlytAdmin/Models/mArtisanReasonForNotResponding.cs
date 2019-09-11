using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portchlytAPI.Models
{
    public class mArtisanReasonForNotResponding
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public DateTime date { get; set; } = DateTime.Now;
        public string artisan_app_id { get; set; }
        public string request_id { get; set; }
        public string reason_for_not_responding_to_request { get; set; }
    }
}
