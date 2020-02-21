using System;
using com.ws.cvxpress.Models;
using Newtonsoft.Json.Linq;

namespace com.ws.cvxpress.ViewModels
{
    public class SelectUser
    {

        public SelectUser(string json)
        {
            JObject jObject =  JObject.Parse(json);

        }

       public TravelerSpecs travelerSpecs { get; set; }
        public RequestSpecs requestSpecs { get; set;  }   
    }
}
