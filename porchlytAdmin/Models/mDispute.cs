using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace portchlytAPI.Models
{
    public class mDispute
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public DateTime date { get; set; } = DateTime.Now;
        public string _job_id { get; set; }
        public string artisan_app_id { get; set; }
        public string client_app_id { get; set; }
        public string reason_for_dispute { get; set; }
        public DisputeStatus dispute_status { get; set; } = DisputeStatus.open;
        public string resolution { get; set; }//how this dispute was resolved

        public mArtisan get_artisan()
        {
            var artisan_col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var artisan = artisan_col.Find(i => i.app_id == this.artisan_app_id).FirstOrDefault();
            return artisan;
        }

        public mClient get_client()
        {
            var client_col = globals.getDB().GetCollection<mClient>("mClient");
            var client = client_col.Find(i => i.app_id == this.client_app_id).FirstOrDefault();
            return client;
        }

    }

    public enum DisputeStatus
    {
        open,
        closed

    }
}
