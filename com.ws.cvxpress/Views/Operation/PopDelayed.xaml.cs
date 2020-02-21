using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class PopDelayed : PopupPage
    {
        TravelerSpecs IdInfo;
        public PopDelayed(TravelerSpecs idInfo)
        {
            InitializeComponent();
            IdInfo = idInfo;
           
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
           
               
            string date = PickDeparture.Date + "|" + PickDepartureTime.Time + "|" + PickArrival.Date + "|" + PickArrivalTime.Time + "|" + Coments.Text;

            string departureDate = PickDeparture.Date.ToString().Split(' ')[0] + " " + PickDepartureTime.Time;
            string ArrivalDate = PickArrival.Date.ToString().Split(' ')[0] + " " + PickArrivalTime.Time;
           
            IdInfo.FromDate = Convert.ToDateTime(departureDate);
            IdInfo.ToDate = Convert.ToDateTime(ArrivalDate);
            IdInfo.Comments = Coments.Text;
            Xamarin.Forms.MessagingCenter.Send<PopDelayed, TravelerSpecs > (this, "TravelUpdated", IdInfo);
           
             PopupNavigation.PopAsync();
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            PickDeparture.Date = IdInfo.FromDate;

            PickDepartureTime.Time = IdInfo.FromDate.TimeOfDay;

            PickArrival.Date = IdInfo.ToDate;

            PickArrivalTime.Time = IdInfo.ToDate.TimeOfDay;
            base.OnAppearing();
        }
    }
}
