using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Views.Admin;
using com.ws.cvxpress.Views.End;
using com.ws.cvxpress.Views.Operation;
using com.ws.cvxpress.Views.RegisterA;
using com.ws.cvxpress.Views.Start;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.ws.cvxpress.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public string pType { get; set; }

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage( )
        {

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

           


            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.MyProfile, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {

           
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {

                    case (int)MenuItemType.OfferBox:
                        MenuPages.Add(id, new NavigationPage(new TravellerPage(Constants.NoModal)));
                        break;
                    case (int)MenuItemType.ListOfferBox:
                        MenuPages.Add(id, new NavigationPage(new TravelsList()));
                        break;
                    case (int)MenuItemType.RequestBox:
                        MenuPages.Add(id, new NavigationPage(new RequestPage(Constants.NoModal)));
                        break;
                    case (int)MenuItemType.ListRequestBox:
                        MenuPages.Add(id, new NavigationPage(new RequestList()));
                        break;
                    case (int)MenuItemType.Contact:
                        MenuPages.Add(id, new NavigationPage(new ContactPage()));
                        break;
                    case (int)MenuItemType.GeneralInfo:
                        MenuPages.Add(id, new NavigationPage(new AdminPage()));
                        break;
                    case (int)MenuItemType.Prererences:
                        MenuPages.Add(id, new NavigationPage(new PreferencesPage(Constants.NoModal)));
                        break;
                    case (int)MenuItemType.Logout:
                        MenuPages.Add(id, new NavigationPage(new LogoutPage()));
                        break;
                    case (int)MenuItemType.AboutPage:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}