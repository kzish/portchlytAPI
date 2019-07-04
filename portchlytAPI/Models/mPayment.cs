using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portchlytAPI.Models
{
    public class mPayment
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        DateTime date { get; set; } = DateTime.Now;
        public PaymentType paymentType { get; set; } = PaymentType.cash;
        public string artisan_app_id { get; set; }
        public string client_app_id { get; set; }
        public double amount_payed { get; set; }
        public bool artisan_accepted { get; set; }
        public string _job_id { get; set; }
    }

    public enum PaymentType {
        cash,
        card,
        transfer,
        
    }
}
