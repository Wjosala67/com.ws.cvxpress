using System;
using System.Collections.Generic;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class RequestsListPage : ContentPage
    {
        RequestListViewModel viewModel;
        public RequestsListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RequestListViewModel(0);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            TravelerSpecs idInfo = (TravelerSpecs)e.Item;
            await Navigation.PushAsync(new TravelInfoPage(idInfo));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }


    }
}
