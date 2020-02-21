using System;
using com.ws.cvxpress.ViewModels;

namespace com.ws.cvxpress.Models
{
    public class RequestTravelerObj
    {
        public RequestSpecs requestSpecs { get; set; }
        public TravelerSpecs travelerSpecs { get; set; }
        public SelectedUser user { get; set;  }
    }
}
