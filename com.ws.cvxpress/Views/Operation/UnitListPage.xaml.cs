using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.ChatViews;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;
using WSViews.Classes;
namespace com.ws.cvxpress.Views.Operation
{
    public partial class UnitListPage : ContentPage
    {
        UniListPageViewModel viewModel;
        TravelerSpecs IdInfo;
        ApiService apiService;
        public UnitListPage(TravelerSpecs idInfo)
        {
            InitializeComponent();
            IdInfo = idInfo;

            BindingContext = viewModel = new UniListPageViewModel(IdInfo);
            MessagingCenter.Subscribe<UnitUpdatePage, string>(this, "DeletedItem1", async (obj, item) => {

                if(item == "No Content")
                {
                    //await viewModel.onsomecomandAsync();
                    //MyListView.ItemsSource = viewModel.LstItemsShow;
                  
                }
            
            });

            MessagingCenter.Subscribe< UniListPageViewModel, string>(this, "DeletedItem1", async (obj, item) => {

                if (item == "No Content")
                {
                    await viewModel.onsomecomandAsync();
                    //CapacityNumber.Text = viewModel.CapacityNumber.ToString();
                    MyListView.ItemsSource = viewModel.LstItemsShow;


                }else
                {
                    await DisplayAlert(Translator.getText("Notice"), Translator.getText("NoPossibleDelete"), "Ok");
                }

            });


            lb_backFunc();
            void lb_backFunc()
            {
                try
                {
                    Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Navigation.PopModalAsync();
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
        }

        void Handle_Clicked_4(object sender, System.EventArgs e)
        {


            RequestSpecs Rs = new RequestSpecs();
            SelectedUser ob = new SelectedUser();
            ob.travelerSpecs = IdInfo;
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

            apiService = new ApiService();
            Users user = new Users();




            var menuItem = sender as Button;
            Rs = menuItem.CommandParameter as RequestSpecs;

            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                Task.Run(async () => {

                    user = await apiService.LoginAsync(Rs.Email);



                }).Wait();

            }
           
            ob.image = ImageManager.BytesToImage( user.UserPhoto);
            Navigation.PushModalAsync(new ChatPage(ob, user.FirstName + " " + user.LastName, Rs));
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            RequestSpecs Rs = (RequestSpecs)e.Item;
            ReserveItemObj Ro = new ReserveItemObj();



            Ro.requestSpecs = Rs;
            Ro.travelerSpecs = IdInfo;
            await Navigation.PushModalAsync(new UnitUpdatePage(Ro));
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

        protected override void OnAppearing()
        {
           
           
           
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send<UnitListPage, string>(this, "UpdateItems", "Done");
            base.OnDisappearing();
        }

        private void Handle_Clicked_2(object sender, System.EventArgs e)
        {




            ReserveItemObj Ro = new ReserveItemObj();
            Ro.travelerSpecs = IdInfo;
           

            var menuItem = sender as Button;
            var selectedItem = menuItem.CommandParameter as RequestSpecs;
            Ro.requestSpecs = selectedItem;
            Navigation.PushModalAsync(new UnitUpdatePage(Ro));

        }

        async void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            var action = await DisplayAlert(Translator.getText("Notice"), Translator.getText("WhantDelete"), Translator.getText("Yes"), Translator.getText("No"));

            if (action) { 
            ReserveItemObj Ro = new ReserveItemObj();
            Ro.travelerSpecs = IdInfo;


            var menuItem = sender as Button;
            var selectedItem = menuItem.CommandParameter as RequestSpecs;
            Ro.requestSpecs = selectedItem;
            viewModel.DeleteItem(Ro);
              


            }
        }

   


    }
}
