using System;

namespace com.ws.cvxpress.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string type { get; set; }
        public int quantity { get; set; }
        public double amount { get; set; }
        public byte[] image { get; set; }
    }
}