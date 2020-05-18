using System;
using com.ws.cvxpress.Lists.Models;
using Xamarin.Forms;

namespace com.ws.cvxpress.Models
{
    public class ShowListsSpecs
    {
        public bool imageShow { get; set; }
        public ImageSource imageSource { get; set; }
        public ListRequestSpecs travelerSpecs { get; set; }
      
    }
}
