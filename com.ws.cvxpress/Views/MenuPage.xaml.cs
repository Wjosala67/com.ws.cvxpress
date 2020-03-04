using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.ws.cvxpress.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public string pType { get; set; }
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        MenuPageViewModel viewModel;

        public MenuPage()
        {
            InitializeComponent();
           
            BindingContext = viewModel = new MenuPageViewModel();
            List<App_Con> lstConf = DatabaseHelper.getConfiguration(App.Os_Folder);
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);
            UserEmail.Text = profile.Email;

            bool Admin = false;

          


            App_Con isUserAdmin = lstConf.Find(x => x.Name == "EmailAdmin" && x.Value == profile.Email.Trim() );

            if (isUserAdmin != null) Admin = true;

            // if exists, loads the email to the form
            if (Application.Current.Properties.ContainsKey(Constants.UserType))
            {

                pType = (string)Application.Current.Properties[Constants.UserType];

            }
            if(pType == Constants.Traveler)
             {
                Toggle.Checked = true;

                viewModel.ItemList = new ObservableCollection<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.MyProfile, Title= Translator.getText("MyProfile"), Icon = "profile.png" },
                    new HomeMenuItem {Id = MenuItemType.OfferBox, Title=Translator.getText("OfferBox"), Icon = "results.png" },
                    new HomeMenuItem {Id = MenuItemType.ListOfferBox, Title=Translator.getText("MyOffers"), Icon = "results.png" },

                    new HomeMenuItem {Id = MenuItemType.Contact, Title=Translator.getText("Contact"), Icon = "contact.png" },
                    new HomeMenuItem {Id = MenuItemType.Prererences, Title=Translator.getText("Settings"), Icon = "preferences.png" },

                    new HomeMenuItem {Id = MenuItemType.AboutPage, Title=Translator.getText("AboutPage"), Icon = "nos.png" },
                    new HomeMenuItem {Id = MenuItemType.Logout, Title=Translator.getText("Logout"), Icon = "salir.png" }
                };

                if (Admin)
                {
                   
                    viewModel.ItemList.Add(new HomeMenuItem { Id = MenuItemType.GeneralInfo, Title = Translator.getText("AdminInfo"), Icon = "info.png" });
                }


            }
            else 
            {
                Toggle.Checked = false;
                viewModel.ItemList = new ObservableCollection<HomeMenuItem> 
                {
                    new HomeMenuItem {Id = MenuItemType.MyProfile, Title= Translator.getText("MyProfile"), Icon = "profile.png" },
                    new HomeMenuItem {Id = MenuItemType.RequestBox, Title=Translator.getText("RequestBox"), Icon = "results.png" },
                    new HomeMenuItem {Id = MenuItemType.ListRequestBox, Title=Translator.getText("MyRequests"), Icon = "results.png" },
                   
                    new HomeMenuItem {Id = MenuItemType.Contact, Title=Translator.getText("Contact"), Icon = "contact.png" },
                    new HomeMenuItem {Id = MenuItemType.Prererences, Title=Translator.getText("Settings"), Icon = "preferences.png" },

                    new HomeMenuItem {Id = MenuItemType.AboutPage, Title=Translator.getText("AboutPage"), Icon = "nos.png" },
                    new HomeMenuItem {Id = MenuItemType.Logout, Title=Translator.getText("Logout"), Icon = "salir.png" }
                
                };
                if (Admin)
                {
                   
                    viewModel.ItemList.Add(new HomeMenuItem { Id = MenuItemType.GeneralInfo, Title = Translator.getText("AdminInfo"), Icon = "info.png" });
                }
                else
                {
                   
                }

            }
            ListViewMenu.ItemsSource = viewModel.ItemList;

            ListViewMenu.SelectedItem = viewModel.ItemList[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
              
                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };

            lb_signupFunc();
           
            void lb_signupFunc()
            {
                try
                {
                    Toggle.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            if (pType != Constants.Sender)
                            {
                                var action = await DisplayAlert(Translator.getText("Error"), Translator.getText("ChangeToSender"), Translator.getText("Yes"), Translator.getText("No"));

                                if (action)
                                {
                                    Application.Current.Properties[Constants.UserType] = Constants.Sender;
                                    await Application.Current.SavePropertiesAsync();
                                    Toggle.Checked = false;
                                    using (UserDialogs.Instance.Loading(Translator.getText("LoadingProfile"), null, null, true, MaskType.Clear))
                                    {
                                        await Wait();
                                    }
                                        Application.Current.MainPage = new MainPage();
                                }
                                else
                                {
                                    Toggle.Checked = true;
                                }

                            }
                            else
                            {
                                var action = await DisplayAlert(Translator.getText("Error"), Translator.getText("ChangeToTraveler"), Translator.getText("Yes"), Translator.getText("No"));

                                if (action)
                                {
                                    Application.Current.Properties[Constants.UserType] = Constants.Traveler;
                                    await Application.Current.SavePropertiesAsync();
                                    Toggle.Checked = true;
                                    using (UserDialogs.Instance.Loading(Translator.getText("LoadingProfile"), null, null, true, MaskType.Clear))
                                    {
                                       await Wait();
                                    }
                                    Application.Current.MainPage = new MainPage();
                                }
                                else
                                {
                                    Toggle.Checked = false;
                                }
                            }
                           

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
        }

        async void  Handle_Tapped(object sender, System.EventArgs e)
        {
            BackgroundColor = Color.Orange; //touch color
            await Task.Delay(250);          //time to show new color
            BackgroundColor = Color.Red;
        }

        private async  Task Wait()
        {
            await Task.Delay(1000);

         
        }
      
    }
}