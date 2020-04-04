using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Provider;
using com.ws.cvxpress.Classes;
using Plugin.FacebookClient;
using Acr.UserDialogs;
//using Plugin.CurrentActivity;
using PanCardView.Droid;
using Android.Util;
using static Android.App.ActivityManager;
using Android.Gms.Common;
using Microsoft.AppCenter.Push;




namespace com.ws.cvxpress.Droid
{
    [Activity(Label = "FlightBox", Icon = "@mipmap/ic_launcher", RoundIcon = "@mipmap/ic_launcher_round", Theme = "@style/MainTheme", MainLauncher = false,ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";

        internal static readonly int NOTIFICATION_ID = 100;


        public const int NOTI_PRIMARY1 = 1100;
        public const int NOTI_PRIMARY2 = 1101;
        public const int NOTI_SECONDARY1 = 1200;
        public const int NOTI_SECONDARY2 = 1201;



        NotificationHelper notificationHelper;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }


            base.OnCreate(savedInstanceState);
            FacebookClientManager.Initialize(this);
            //CrossCurrentActivity.Current.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: false);
          

            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            bool avail = IsPlayServicesAvailable();

            if (avail  && (Build.VERSION.SdkInt >= BuildVersionCodes.O))
            {

                notificationHelper = new NotificationHelper(this);

            }
           

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CardsViewRenderer.Preserve();
            global::Openpay.Xamarin.OpenpayAndroidImpl.Init(this);
            LoadApplication(new App(Constants.Android_db_folder));
           
          
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
        }
        public override void OnRequestPermissionsResult(int requestCode,
           string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            //Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions,grantResults);
        }
        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            Push.CheckLaunchedFromNotification(this, intent);
        }

        public bool IsPlayServicesAvailable()
        {
            string Text = "";
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                Text = "Google Play Services is available.";
                return true;
            }
        }



     


        public void SendNotification(int id, string title, string body)
        {
            Notification.Builder nb = null;
            switch (id)
            {
                case NOTI_PRIMARY1:
                    nb = notificationHelper.GetNotification1(title, body);
                    break;

                case NOTI_PRIMARY2:
                    nb = notificationHelper.GetNotification1(title, body);
                    break;

                case NOTI_SECONDARY1:
                    nb = notificationHelper.GetNotification2(title, body);
                    break;

                case NOTI_SECONDARY2:
                    nb = notificationHelper.GetNotification2(title, body);
                    break;
            }
            if (nb != null)
            {
                notificationHelper.Notify(id, nb);
            }
        }

        public void GoToNotificationSettings()
        {
            var i = new Intent(Settings.ActionAppNotificationSettings);
            i.PutExtra(Settings.ExtraAppPackage, PackageName);
            StartActivity(i);
        }

        public void GoToNotificationSettings(string channel)
        {
            var i = new Intent(Settings.ActionChannelNotificationSettings);
            i.PutExtra(Settings.ExtraAppPackage, PackageName);
            i.PutExtra(Settings.ExtraChannelId, channel);
            StartActivity(i);
        }




    }
}