
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter.Push;


namespace com.ws.cvxpress.Droid
{

    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, Icon = "@mipmap/ic_launcher",  RoundIcon = "@mipmap/ic_launcher_round",  NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));
           
            if (intent.Extras != null)
                intent.PutExtras(Intent.Extras); // copy push info from splash to main

          
            StartActivity(intent);
           
            //StartActivity(typeof(MainActivity));
        }
        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            Push.CheckLaunchedFromNotification(this, intent);
        }
    }
}
