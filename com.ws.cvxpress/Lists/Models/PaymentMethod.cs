using System;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string HoldersName { get; set; }
        public string Entity { get; set; }
        public string Card { get; set; }
        public string Clabe { get; set; }
        public string Account { get; set; }
        public int status { get; set; }
        public byte[] EntityPhoto { get; set; }
        public ImageSource imageSource { get; set; }

    }
}
