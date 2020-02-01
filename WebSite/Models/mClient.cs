using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

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
        public string mobile_country_code { get; set; }
        public string email { get; set; }
        public string physical_address { get; set; }
        public bool registered { get; set; }
        public string otp { get; set; }
        public string app_id;//the app id of this client
        public bool synced { get; set; }
        public bool enabled { get; set; } = true;//set this accountto be enabled or disabled
        public string reason_for_enable { get; set; }//set the reason

        public long get_number_of_cancellations()
        {
            var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
            long cancells = jobs_col.Find(i => i.client_mobile == this.mobile && i.job_status == JobStatus.cancelled).CountDocuments();
            return cancells;
        }

        public long get_number_of_disputes()
        {
            var disputes_col = globals.getDB().GetCollection<mDispute>("mDispute");
            long disputes = disputes_col.Find(i => i.client_app_id == this.app_id).CountDocuments();
            return disputes;
        }

        public long get_number_of_completed_jobs()
        {
            var jobs_col = globals.getDB().GetCollection<mJobs>("mJobs");
            long jobs = jobs_col.Find(i => i.client_mobile == this.mobile && i.job_status == JobStatus.closed).CountDocuments();
            return jobs;
        }



    }
}

