using System;
using com.ws.cvxpress.Models;

namespace com.ws.cvxpress.Lists.Models
{
    public class ObjListPayment
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public UInfo uInfo { get; set;  }
        public ListRequestSpecs listRequestSpecs { get; set; }
    }
}
