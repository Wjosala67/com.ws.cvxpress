using System;
using System.Collections.ObjectModel;

namespace com.ws.cvxpress.Models
{
    public class TravelInfoClass
    {
        public TravelerSpecs travelerSpecs { get; set; }
        public ObservableCollection<RequestSpecs> lstRequestSpecsNew { get; set;  }
        public ObservableCollection<RequestSpecs> lstRequestSpecsAccepted { get; set; }
    }
}
