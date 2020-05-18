using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Lists.ListsViewModels;
using com.ws.cvxpress.Models;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists
{
    public partial class ListAdminDetailsPage : ContentPage
    {
        ListAdminDetailsPageViewModel viewModel;
        decimal ppqk = 0;
        public ListAdminDetailsPage(ShowListsSpecs showListsSpecs, int idTravel)
        {
            InitializeComponent();
            List<App_Con> Lconf = DatabaseHelper.getConfiguration(App.Os_Folder);
            string strPpqk = Lconf.Where(x => x.Name == "CostoPorKilo").Select(x => x.Value).FirstOrDefault().ToString();
            ppqk = decimal.Parse(strPpqk);


            Title = "Viaje:" + idTravel + "-Lista:" + showListsSpecs.travelerSpecs.Id;
            BindingContext = viewModel = new ListAdminDetailsPageViewModel(showListsSpecs, idTravel);

      

        }

        void MyListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }

        protected override void OnAppearing()
        {
            MyListView.ItemsSource = null;
            viewModel.ItemsLoadCommand.Execute(this);

            

            MyListView.ItemsSource = viewModel.ListDetails;

            en_intshipfee.Text = viewModel.InteriorShipment.ToString();
            en_service.Text = viewModel.Service.ToString();
            en_desc.Text = viewModel.ServiceDesc;

            base.OnAppearing();
        }

       

        void CustomEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            ppk.Text = (viewModel.TotalWeight * ppqk).ToString();
        }
    }
}
