using System;
namespace com.ws.cvxpress.Models
{
    public class ReserveItemObj
    {

        public int status { get; set;  }
        public RequestSpecs requestSpecs { get; set; }
        public Traveler_Request travelerRequest { get; set;  } 
        public TravelerSpecs travelerSpecs { get; set;  }
        public ReserveItemObj()
        {
        }
    }
}
