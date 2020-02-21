using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class ListClientsRequests : ContentPage

    {
        ClientServiceViewModel viewModel;
        public ListClientsRequests()
        {
            InitializeComponent();
            BindingContext = viewModel = new ClientServiceViewModel();
            Title = Translator.getText("GenInfo");
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            Client_Service client_Service = new Client_Service();
            client_Service = (Client_Service)e.Item;
            await Navigation.PushAsync(new ClientReqDetail(client_Service));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                Task.Run(async () => {

                    Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
                    await viewModel.InitializeAsync(profile.Email);

                   


                }).Wait();
                MyListView.ItemsSource = viewModel.LstItems;
            }
            base.OnAppearing();
        }
    }
}
