using System;
namespace com.ws.cvxpress.Models
{
    public class RewardObject
    {
        public int IdReward { get; set; }
        public int IdTravel { get; set; }
        public string Email { get; set; }
        public string CountryFrom { get; set; }
        public string CountryTo { get; set;  }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int ItemQty { get; set; }
        public decimal RewardTotal { get; set; }
        public RewardObject()
        {
        }
    }
}
