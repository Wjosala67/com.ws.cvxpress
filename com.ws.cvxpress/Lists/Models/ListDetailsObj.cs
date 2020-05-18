using System;
using System.Collections.Generic;
using com.ws.cvxpress.Models;

namespace com.ws.cvxpress.Lists.Models
{
    public class ListDetailsObj
    {
     
        public Users user { get; set; }
        public ListRequestSpecs listrequestSpecs { get; set; }
        public List<LRequestSpecs_det> lrequestSpecs_det { get; set; }
    }
}
