using System;
using System.Collections.Generic;

namespace com.ws.cvxpress.Lists.Models
{
    public class objRequesList
    {
        public ListRequestSpecs listrequestspecs { get; set; }
        public List<LRequestSpecs_det> lrequestspecs_det { get; set; }
        public int itemsInList { get; set;  }
        public decimal amount { get; set;  }
        public objRequesList()
        {
        }
    }
}
