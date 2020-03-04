using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.Views;
using com.ws.cvxpress.Views.Operation;
using com.ws.cvxpress.Views.Start;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Openpay.Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace com.ws.cvxpress
{
    public partial class App : Application
    {
        public static string  Os_Folder, email, pType;

        public  static string  isTheFirst { get; set; }

        public string TravelerBox { get; set; }

        public static string User { get; set; }

        string Notified , FireBaseToken;

        public static string strCurrency { get; set;  }

        public string  redirectTo { get; set;  }

        public App() {  Notified = "False"; }

        public static byte[] CroppedImage;

        public bool isBackGround;

        public App(string os_folder)
        {
            InitializeComponent();

            Os_Folder = os_folder;
            try { 
            if (Application.Current.Properties[Constants.notified] != null)
            {
                Notified = (string)Application.Current.Properties[Constants.notified];
                redirectTo = (string)Application.Current.Properties["Goto"];
            }
            }
            catch (Exception ex) {
                Application.Current.Properties["notified"] = "False";

                Application.Current.Properties["Goto"] = "";

            }




            Profile profile = DatabaseHelper.GetProfile(Os_Folder);
            if (profile != null) {

                User = profile.Email;
                if (profile.VerificationCode != null) 
                {


                    if (Notified == "True")
                    {
                       
                        if (!redirectTo.Contains("Chat|") && !redirectTo.Contains("TripEnded|") && !redirectTo.Contains("RequestList|") && !redirectTo.Contains("ItemAccepted|"))
                        {
                            switch (redirectTo)
                                {

                                    case "ClientService":
                                        Current.MainPage = new MainPage
                                        {
                                            Title = Translator.getText("Contact"),
                                            Master = new MenuPage(),
                                            Detail = new NavigationPage(new ListClientsRequests())
                                        };
                                        break;
                                    case "BoxList":

                                      
                                        Current.MainPage = new MainPage
                                        {
                                            Title = Translator.getText("MyOffers"),
                                            Master = new MenuPage(),
                                            Detail = new NavigationPage(new TravelsList())
                                        };
                                        break;
                                    case "RequestList":
                                        Current.MainPage = new MainPage
                                        {
                                            Title = Translator.getText("MyRequests"),
                                            Master = new MenuPage(),
                                            Detail = new NavigationPage(new RequestList())
                                        };
                                        break;

                                    case "Chat":
                                        // agregar información del viaje y el articulo
                                        Current.MainPage = new MainPage
                                        {
                                            Title = Translator.getText("MyRequests"),
                                            Master = new MenuPage(),
                                            Detail = new NavigationPage(new RequestList())
                                        };
                                        break;
                                    case "ItemAccepted":
                                        Current.MainPage = new MainPage
                                        {
                                            Title = Translator.getText("MyRequests"),
                                            Master = new MenuPage(),
                                            Detail = new NavigationPage(new RequestList())
                                        };
                                        break;

                                 default:
                                    Current.MainPage = new MainPage
                                    {
                                        Title = Translator.getText("MyRequests"),
                                        Master = new MenuPage(),
                                        Detail = new NavigationPage(new ProfilePage())
                                    };
                                    break;
                                    
                                }
                    } else
                        {
                            RequestSpecs requestSpecs = new RequestSpecs();
                            // agregar información del viaje y el articulo
                            if (redirectTo.Contains("Chat|"))

                            {
                                Current.MainPage = new MainPage
                                {

                                    Title = Translator.getText("MyRequests"),
                                    Master = new MenuPage(),
                                    Detail = new NavigationPage(new RequestList())
                                };
                            }else if (redirectTo.Contains("TripEnded|") || redirectTo.Contains("ItemAccepted|"))
                            {
                                requestSpecs.Id = int.Parse(redirectTo.Split('|')[1]);
                                App.WaitScreenStart(Translator.getText("Loading"));
                                Task.Run(async () => {
                                    ApiService apiService = new ApiService();
                                    requestSpecs = await apiService.getRequestById(requestSpecs);


                                }).Wait();
                                App.WaitScreenStop();
                                Current.MainPage = new MainPage
                                {
                                   
                                    //Title = Translator.getText("MyRequests"),
                                    Master = new MenuPage(),
                                    Detail = new NavigationPage(new RequestInfoPage(requestSpecs, "0"))
                                };
                            }else if (redirectTo.Contains("RequestList|"))
                            {
                                requestSpecs.Id = int.Parse(redirectTo.Split('|')[1]);
                                App.WaitScreenStart(Translator.getText("Loading"));
                                Task.Run(async () => {
                                    ApiService apiService = new ApiService();
                                    requestSpecs = await apiService.getRequestById(requestSpecs);


                                }).Wait();
                                App.WaitScreenStop();
                                Current.MainPage = new MainPage
                                {

                                    //Title = Translator.getText("MyRequests"),
                                    Master = new MenuPage(),
                                    Detail = new NavigationPage(new RequestInfoPage(requestSpecs, "0"))
                                };
                            }
                            else if (redirectTo.Contains("ItemAuth|"))
                            {
                                TravelerSpecs travelerSpecs = new TravelerSpecs();
                                travelerSpecs.Id = int.Parse(redirectTo.Split('|')[1]);
                                App.WaitScreenStart(Translator.getText("Loading"));

                                Task.Run(async () => {
                                    ApiService apiService = new ApiService();
                                    travelerSpecs = await apiService.GetTravelSpecsAsync(travelerSpecs.Id);


                                }).Wait();
                                App.WaitScreenStop();
                                Current.MainPage = new MainPage
                                {

                                    //Title = Translator.getText("MyRequests"),
                                    Master = new MenuPage(),
                                    Detail = new NavigationPage(new TravelInfoPage(travelerSpecs))
                                };
                            }
                        }






                    }
                    else
                        MainPage = new MainPage();
                } else {

                    MainPage = new VerifyPage();
                }

            }
            else
            {
               
                MainPage = new LoginPage(); // LoginPage
            }


        }
        protected override void OnStart()         {
           
               if (!AppCenter.Configured)                 Push.PushNotificationReceived += Push_PushNotificationReceived;

            if (CrossOpenpay.IsSupported) CrossOpenpay.Current.Initialize(Constants.OPPrivateKey, Constants.OPID, false);

                    AppCenter.Start("android=d0631cdb-bded-46b0-808a-f4df3b9fb5ea;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Push),typeof(Analytics), typeof(Crashes));

            Application.Current.Properties["notified"] = "False";
        
            Application.Current.Properties["Goto"] = "";
            Application.Current.SavePropertiesAsync();


            OnCommand();
                   } 

   

         private void Push_PushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)         {

            try { 
            // Add the notification message and title to the message

            Application.Current.Properties["notifiedItemRoute"] = null;


                var summary = $"Push notification received:" +                                 $"\n\tNotification title: {e.Title}" +                                 $"\n\tMessage: {e.Message}";                 // If there is custom data associated with the notification,             // print the entries             if (e.CustomData != null)             {
                string GotoPage = "";                 summary += "\n\tCustom data:\n";                 foreach (var key in e.CustomData.Keys)                 {                     summary += $"\t\t{key} : {e.CustomData[key]}\n";
                    if (key == "GoTo") {

                        GotoPage = e.CustomData[key];

                    }
                 }

                    if (GotoPage.Contains("Chat|"))
                    {
                        MessagingCenter.Send<App, string>(this, "ChatUpdate", "Update");
                    }
                    else if (GotoPage.Contains("BoxList"))
                    {
                        if (e.Message != null)
                        {
                            Application.Current.Properties["notifiedItemRoute"] += e.Message.Split(':')[1].Trim();
                            Application.Current.SavePropertiesAsync();
                            MessagingCenter.Send<App, string>(this, "NewItemsUpdate", "Update");
                        }
                    }

                    if (!isBackGround) ToastMessage(e.Message, Color.Aquamarine);


                        Application.Current.Properties["notified"] = "True";
                Application.Current.Properties["Goto"] = GotoPage;
             
                


            }
            Application.Current.SavePropertiesAsync();

            // Send the notification summary to debug output
            System.Diagnostics.Debug.WriteLine(summary);
            }
            catch (Exception x) { }

        }


      



        private async void OnCommand()
        {
            bool updateCatalogs = false;
            App_Con nSetting = new App_Con();
            ObservableCollection<App_Con> ConfDateFromWs;
            ApiService apiService = new ApiService();
           
            using (UserDialogs.Instance.Loading(Translator.getText("LookingForUpdates"), null, null, true, MaskType.Black))
            {

 

                List<App_Con> lstSetting = DatabaseHelper.getConfiguration(Os_Folder);

                // detect country

                strCurrency = " USD";

                // if Settins table is empty get configuration date (catalogs) from web service and save it to the database and asign it to
                // updateDateStored and nSettings 

                 ConfDateFromWs = await apiService.getConfAsync();

                if (ConfDateFromWs.Count > 0)
                {
                    string DateFromWebService = ConfDateFromWs.Where(x => x.Name == Constants.UpdateCatalog).First().Value;

                    string strDatabaseDate = "";

                    if (lstSetting == null) lstSetting = new List<App_Con>();

                    if (lstSetting.Count > 0)
                    {
                        strDatabaseDate = lstSetting.Where(x => x.Name == Constants.UpdateCatalog).First().Value;


                        int day = int.Parse(DateFromWebService.Split('/')[0]);
                        int month = int.Parse(DateFromWebService.Split('/')[1]);
                        int year = int.Parse(DateFromWebService.Split('/')[2]);
                        DateTime fromwebservice = new DateTime(year,month,day);
                        DateTime fromDatabase = new DateTime(1970, month, day);

                        if (fromwebservice > fromDatabase)
                        {
                            updateCatalogs = true;
                        }else
                        { updateCatalogs = false; }

                    }
                    else
                    {
                        updateCatalogs = true;




                    }
                }

            }


            if (updateCatalogs)
                {
                    using (UserDialogs.Instance.Loading(Translator.getText("LoadingResources"), null, null, true, MaskType.Black))
                    {
                        DatabaseHelper.Delete(ref nSetting, Os_Folder, "App_Con", "");

                        foreach (App_Con item in ConfDateFromWs)
                        {
                            nSetting = item;

                            DatabaseHelper.Insert(ref nSetting, Os_Folder);

                        }

                        // Delete UserTypes, UserDelivery, UserCategories
                        DatabaseHelper.Delete(ref nSetting, Os_Folder, "Categories", "");

                        DatabaseHelper.Delete(ref nSetting, Os_Folder, "Types", "");

                        DatabaseHelper.Delete(ref nSetting, Os_Folder, "DeliveryTypes", "");

                        DatabaseHelper.Delete(ref nSetting, Os_Folder, "CollectTypes", "");

                        DatabaseHelper.Delete(ref nSetting, Os_Folder, "Countries", "");

                        ObservableCollection<Categories> oc_Categories = await apiService.getCategoriesAsync();

                        ObservableCollection<Types> oc_Types = await apiService.getTypesAsync();

                        ObservableCollection<DeliveryTypes> oc_DeliveryTypes = await apiService.getDeliveryTypesAsync();

                        ObservableCollection<CollectTypes> oc_CollectTypes = await apiService.getCollectTypesAsync();

                        ObservableCollection<Countries> oc_Countries= await apiService.getCountriesAsync();

                    // Insert UserTypes, UserDelivery, UserCategories


                        foreach (Categories item in oc_Categories)
                        {
                            Categories nCategorie = new Categories();

                            nCategorie = item;

                            DatabaseHelper.Insert(ref nCategorie, Os_Folder);

                        }

                        foreach (Types item in oc_Types)
                        {
                            Types nType = new Types();

                            nType = item;

                            DatabaseHelper.Insert(ref nType, Os_Folder);

                        }

                        foreach (DeliveryTypes item in oc_DeliveryTypes)
                        {
                            DeliveryTypes nDeliType = new DeliveryTypes();

                            nDeliType = item;

                            DatabaseHelper.Insert(ref nDeliType, Os_Folder);

                        }
                        foreach (CollectTypes item in oc_CollectTypes)
                        {
                            CollectTypes nCollType = new CollectTypes();

                            nCollType = item;

                            DatabaseHelper.Insert(ref nCollType, Os_Folder);

                        }


                    foreach (Countries item in oc_Countries)
                        {
                            Countries nCountryType = new Countries();

                            nCountryType = item;

                            DatabaseHelper.Insert(ref nCountryType, Os_Folder);

                        }

                }
                }


            


        }

        protected override void OnSleep()
        {
            isBackGround = true;
        }

        protected override void OnResume()
        {
            isBackGround = false;
        }

        public static void WaitScreenStart(string message)
        {
            UserDialogs.Instance.ShowLoading(message, MaskType.Black);
        }


        public static void WaitScreenStop()
        {
            UserDialogs.Instance.HideLoading();
        }

        public static void ToastMessage(string message, Color color)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                ToastConfig tc = new ToastConfig(message);
                tc.SetBackgroundColor(color);

                if (color == Color.Aquamarine)
                {
                    tc.SetMessageTextColor(Color.Black);
                    tc.SetPosition(ToastPosition.Bottom);
                }
                else
                {
                    tc.SetMessageTextColor(Color.White);
                    tc.SetPosition(ToastPosition.Bottom);
                }

               
                tc.SetDuration(TimeSpan.FromSeconds(5.0));
             
                UserDialogs.Instance.Toast(tc);
            });
        }
    }
}
