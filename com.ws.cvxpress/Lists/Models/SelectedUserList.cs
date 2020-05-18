using System;
using com.ws.cvxpress.Models;

namespace com.ws.cvxpress.Lists.Models
{
    public class SelectedUserList
    {
        public byte[] image { get; set; }
        public Users user { get; set; }
        public TravelerSpecs travelerSpecs { get; set; }
    }
}
