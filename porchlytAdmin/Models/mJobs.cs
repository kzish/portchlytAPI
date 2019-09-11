using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace portchlytAPI.Models
{
    public class mJobs
    {
        public string _id = Guid.NewGuid().ToString();
        public DateTime date { get; set; } = DateTime.Now;
        public string _job_id { get; set; }//this is the same job id between both the client and the artisan to sync them
        public string client_mobile;//mobile number for the cleient
        public string client_app_id;//app id fo rmqtt identfy the client for this job
        public string artisan_app_id;//app id fo rmqtt identfy the artisan for this job
        public string start_time;//this is also the start time of the job
        public string end_time;//the time this job was ended or officially cleared
        public string country;
        public string category;
        public string city_or_state;
        public string geoLocationLatitude;//coordinates of this job
        public string geoLocationLongitude;
        public string address;//the address of the client requested from will aquire through reverse geo coding
        public double price;
        public string description;//any notes the artsian may want to note
        public List<mTask> tasks = new List<mTask>();//these are te bills or break down of the job
        public JobStatus job_status;

        //if cancelled
        public string reason_for_cancelling { get; set; }
        public Who_cancelled who_cancelled { get; set; }//either client or artisan

        public mArtisan get_artisan()
        {
            var col = globals.getDB().GetCollection<mArtisan>("mArtisan");
            var artisan = col.Find(x => x.app_id == this.artisan_app_id).FirstOrDefault();
            return artisan;
        }

        public double getTheTotalPrice()
        {
            double total = 0;
            foreach (var t in tasks)
            {
                total += t.price;
            }
            return total;
        }
    }

    public enum JobStatus
    {
        opened,
        closed,
        cancelled,
        dispute,
        dispute_resolved
    }

    public class mTask
    {
        public string _id { get; set; }//id comes from  the artisan
        public string description { get; set; }//add a description to this task, eg light fitting
        public double price { get; set; }//the price of this item of the task
    }


    public enum Who_cancelled
    {
        client,
        artisan
    }

}
