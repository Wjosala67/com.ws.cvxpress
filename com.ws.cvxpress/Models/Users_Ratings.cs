using System;
namespace com.ws.cvxpress.Models
{
    public class Users_Ratings
    {
        public int IdRating { get; set; }
        public int IdTraveler { get; set; }
        public int IdRequest { get; set; }
        public int IdTravel { get; set; }
        public int Rating { get; set; }
        public Users_Ratings()
        {
        }
    }
}
