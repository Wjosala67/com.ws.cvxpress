using System;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists.Models
{
    public class LRequestSpecs_det
    {
        public int id_det { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FindWhere { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal ProductValue { get; set; }
        public int Cat_Id { get; set; }
        public byte[] ProductImage { get; set; }
        public string TrackingNumber { get; set; }
        public ImageSource imageSource { get; set; }
      

    }
}
