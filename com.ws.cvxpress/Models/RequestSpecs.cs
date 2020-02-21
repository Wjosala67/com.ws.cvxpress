using System;
using Xamarin.Forms;

namespace com.ws.cvxpress.Models
{
    public class RequestSpecs
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CountryCodeFrom { get; set; }
        public string CountryCodeTo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public string FindWhere { get; set; }
        public int Cat_Id { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; } 
        public decimal Long { get; set; }
        public string Comments { get; set; }
        public string DeliveredAt { get; set; }
        public DateTime Created { get; set; }
        public int status { get; set; }
        public decimal ProductValue { get; set; }
        public byte[] ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Height { get; set; }
        public decimal Reward { get; set; }
        public string OpenDelivery { get; set; }
        public string OpenDays { get; set; }
        public decimal Commission { get; set; }
        public string Tolerance { get; set; }
        public int delete { get; set; }
        public ImageSource imageSource {get; set;}
        public bool canDelete { get; set; }
        public RequestSpecs()
        {
        }
    }
}
