using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portchlytAPI.Models
{
    public class mDispute
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public DateTime date { get; set; } = DateTime.Now;
        public string _job_id { get; set; }
        public string reason_for_dispute { get; set; }
        public DisputeStatus dispute_status { get; set; } = DisputeStatus.open;
    }

    public enum DisputeStatus
    {
        open,
        closed

    }
}
