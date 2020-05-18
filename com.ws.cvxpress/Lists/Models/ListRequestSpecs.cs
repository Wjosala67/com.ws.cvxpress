using System;
namespace com.ws.cvxpress.Lists.Models
{
    public class ListRequestSpecs
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Email { get; set; }
        public string CountryCodeFrom { get; set; }
        public string CountryCodeTo { get; set; }
        public DateTime  TravelDate { get; set; }
        public DateTime TravelLimit { get; set; }
        public string Addressee { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Status { get; set; }
        public string OpenDelivery { get; set; }
        public string OpenDays { get; set; }
        public decimal TotalProductValue { get; set; }
        public decimal TotalWeight { get; set; }
        public string AddresseePhone { get; set; }
        public int TotalItems { get; set; }
        public decimal ShipmentFee { get; set; }
        public decimal ServiceFee { get; set; }
        public string ServiceDesc { get; set; }
        public string DeliveredAt { get; set; }

    }
}
