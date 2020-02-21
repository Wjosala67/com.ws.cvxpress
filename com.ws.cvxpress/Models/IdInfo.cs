using System;
using Xamarin.Forms;

namespace com.ws.cvxpress.Models
{
    public class IdInfo
    {
        public string name { get; set; }
        public string comments { get; set; }
        public string desiredDate { get; set; }
        public ImageSource Image { get; set; }
        public string AproValue { get; set; }
        public byte[] ImageByte { get; set; }
        public IdInfo()
        {
        }
    }
}
