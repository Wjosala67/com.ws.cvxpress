using System;
using com.ws.cvxpress.Models;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class SelectedUser
    {
        public ImageSource image { get; set; }
        public Users user { get; set; }
        public TravelerSpecs travelerSpecs {get; set;}
        public SelectedUser()
        {
        }
    }
}
