using System;
using Xamarin.Forms;

namespace com.ws.cvxpress.Models
{
    public class IdInfoTra
    {
       

        public string From { get; set; }
        public string To { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Capacity { get; set; }
        public string comments { get; set; }
        public ImageSource Image { get; set; }
        public byte[] ImageByte { get; set; }
        public IdInfoTra()
        {
        }
    }
}
