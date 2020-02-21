using System;
namespace com.ws.cvxpress.Models
{
    public class Traveler_Request
    {

        public int IdRequestSpecs { get; set; }
        public int IdTravelerSpecs { get; set; }
        public int status { get; set; }
        public string Comments { get; set; }
        public DateTime Date_Accepted { get; set; }
        public string Email { get; set;  }
        public Traveler_Request()
        {
        }
    }
}
