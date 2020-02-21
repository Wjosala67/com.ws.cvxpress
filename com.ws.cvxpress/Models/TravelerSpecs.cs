using System;
namespace com.ws.cvxpress.Models
{
    public class TravelerSpecs
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CountryCodeFrom { get; set;  }
        public string CountryCodeTo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set;  } 
        public string Capacity { get; set; }
        public string DeliveredAt { get; set; }
        public string Comments { get; set; }
        public DateTime Created { get; set;  }
        public int status { get; set; }

    }
}
