using System;
using System.Collections.Generic;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Lists;
using com.ws.cvxpress.Lists.ListsViewModels;
using com.ws.cvxpress.Models;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Admin
{
    public partial class CILPage : ContentPage
    {
        TravelsListPageViewModel viewModel;
        public CILPage()
        {
            InitializeComponent();
            Title = Translator.getText("NextDepartures");
            BindingContext = viewModel = new TravelsListPageViewModel(50);
        }


        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ShowTravelSpecs showobj = (ShowTravelSpecs)e.Item;

            //if (Convert.ToDateTime(showobj.travelerSpecs.ToDate.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            //{
            //    await DisplayAlert(Translator.getText("Notice"), Translator.getText("TravelNotAvailable"), "Ok");
            //    return;
            //}

            await Navigation.PushAsync(new ListCodeAdminPage(showobj.travelerSpecs));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
