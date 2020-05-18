using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.ChatViews;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Lists.ListsViewModels;
using com.ws.cvxpress.Lists.Models;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists
{
    public partial class ListCodeAdminPage : ContentPage
    { 
        ListCodeAdminPageViewModel viewModel;
        TravelerSpecs _travelerSpecs;
        ApiService apiService;
       
        public ListCodeAdminPage(TravelerSpecs travelersSpecs)
        {
            InitializeComponent();
            Title = "Listas - Viaje: " + travelersSpecs.Id;
            _travelerSpecs = travelersSpecs;
            BindingContext = viewModel = new ListCodeAdminPageViewModel(travelersSpecs.Id);

            MessagingCenter.Subscribe<ListAdminDetailsPageViewModel, string>(this, "ListUpdated", async (obj, item) =>
            {

                if (item == "Done")
                { await Navigation.PopAsync(); }else
                {
                   await DisplayAlert(Translator.getText("Notice"), Translator.getText("ProcessNotCompleted"), "OK");
                   await Navigation.PopAsync();
                }
            });


            MessagingCenter.Subscribe<ListCodeAdminPageViewModel, string>(this, "ListUpdated", async (obj, item) =>
            {

                if (item == "Done")
                {
                    MyListView.ItemsSource = viewModel.LstItemsShow;
                }
            });

       
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ShowListsSpecs showobj = (ShowListsSpecs)e.Item;

           

            await Navigation.PushAsync(new ListAdminDetailsPage(showobj, _travelerSpecs.Id));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {

            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                
            }
            ShowListsSpecs showobj = (ShowListsSpecs)menuitem.CommandParameter ;


            var config = new ActionSheetConfig
            {
                Cancel = new ActionSheetOption(Translator.getText("Cancel")),
                Title = "Lista de:" + showobj.travelerSpecs.Email
            };


          


            config.Add(Translator.getText("ListInReview2"), new Action(async () =>
            {

                await updatePage(1, showobj.travelerSpecs);
            }));

            config.Add(Translator.getText("WaitForPayment2"), new Action(async () =>
            {
                await updatePage(2, showobj.travelerSpecs);
            }));

            config.Add(Translator.getText("ListInProcess2"), new Action(async () =>
            {
                bool result = await DisplayAlert(Translator.getText("Notice"), "¿Que desea hacer con esta lista?", "Cobrar Productos", "Solo estado");

                if (result) await updatePage(9, showobj.travelerSpecs);
                else
                    await updatePage(3, showobj.travelerSpecs);
            }));

            config.Add(Translator.getText("ListInBought2"), new Action(async () =>
            {
                await updatePage(4, showobj.travelerSpecs);
            }));

            config.Add(Translator.getText("ListCompleted2"), new Action(async () =>
            {
                await updatePage(5, showobj.travelerSpecs);
            }));
            config.Add(Translator.getText("ListCancelled2"), new Action(async () =>
            {
                await updatePage(6, showobj.travelerSpecs);
            }));


            var speechDialog = UserDialogs.Instance.ActionSheet(config);
        }

        private async Task updatePage(int v, ListRequestSpecs travelerSpecs)
        {
            App.WaitScreenStart(Translator.getText("Loading"));

            apiService = new ApiService();

            travelerSpecs.Status = v;
            await apiService.UpdateListDet(travelerSpecs);

            await viewModel.InitializeAsync();

            MyListView.ItemsSource = viewModel.LstItemsShow;

            App.WaitScreenStop();
        }

        private Task updatePage(int v)
        {
            throw new NotImplementedException();
        }

        protected override void OnAppearing()
        {
            viewModel.LoadItemsCommand.Execute(this);

            base.OnAppearing();
        }

        void chatAdmin_Clicked(System.Object sender, System.EventArgs e)
        {
            ShowListsSpecs Rs = new ShowListsSpecs();
            SelectedUser ob = new SelectedUser();
            ob.travelerSpecs = _travelerSpecs;
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            RequestSpecs Rspecs = new RequestSpecs();
            apiService = new ApiService();
            Users user = new Users();


           

            var menuItem = sender as Button;
            Rs = menuItem.CommandParameter as ShowListsSpecs;

            Rspecs.Id = Rs.travelerSpecs.Id;
            Rspecs.Email = profile.Email;

            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                Task.Run(async () => {

                    user = await apiService.LoginAsync(Rs.travelerSpecs.Email);



                }).Wait();

            }

            ob.image = (user.UserPhoto != null) ? ImageManager.BytesToImage(user.UserPhoto) : "giphy.gif";

            Navigation.PushModalAsync(new ChatPage(ob, user.FirstName + " " + user.LastName, Rspecs));
        }
    }
}
