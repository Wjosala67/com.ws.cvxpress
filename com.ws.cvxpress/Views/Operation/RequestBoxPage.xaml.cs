using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class RequestBoxPage : ContentPage
    {
        RequestListViewModel viewModel;
        public RequestBoxPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RequestListViewModel(0);
            NavigationPage.SetHasNavigationBar(this, false);
            //Title = Translator.getText("MyRequests");
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ShowRequestSpecs idInfo = (ShowRequestSpecs)e.Item;
            await Navigation.PushModalAsync(new RequestInfoPage(idInfo.requestSpecs, "1"));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnAppearing()
        {


            //using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            //{
            //Task.Run(async () => {

            viewModel.ExecuteLoadItemsCommand();

            MyListView.ItemsSource = viewModel.LstItemsShow;


            //    }).Wait();

            //}

            base.OnAppearing();
        }
    }
}
