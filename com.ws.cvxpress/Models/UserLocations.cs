using System;
namespace com.ws.cvxpress.Models
{
    public class UserLocations
    {
        public string  Email { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string IMEI { get; set; }

        public UserLocations()
        {
        }
    }
}
