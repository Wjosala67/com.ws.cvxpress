using System;
using System.Collections.Generic;
using Xamarin.Forms;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.Lists.ListsViewModels;
using com.ws.cvxpress.Classes;

namespace com.ws.cvxpress.Lists
{
    public partial class TravelsListPage : ContentPage
    {
        TravelsListPageViewModel viewModel;
        public TravelsListPage()
        {
            BindingContext = viewModel = new TravelsListPageViewModel(50);

            Title = Translator.getText("NextDepartures");
            viewModel.IsBusy = false;
            InitializeComponent();
        }



        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ShowTravelSpecs showobj = (ShowTravelSpecs)e.Item;

          

            await Navigation.PushAsync(new ListCodePage(showobj.travelerSpecs));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
