using System;

using Android.App;
using Android.Content;
using Android.Graphics;

namespace com.ws.cvxpress.Droid
{
    public class NotificationHelper : ContextWrapper
    {
        public const string PRIMARY_CHANNEL = "default";
        public const string SECONDARY_CHANNEL = "second";

        NotificationManager manager;
        NotificationManager Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = (NotificationManager)GetSystemService(NotificationService);
                }
                return manager;
            }
        }

        int SmallIcon => Android.Resource.Drawable.StatNotifyChat;

        public NotificationHelper(Context context) : base(context)
        {
            var chan1 = new NotificationChannel(PRIMARY_CHANNEL,
                    "Primary Channel", NotificationImportance.Default);
            chan1.LightColor = Color.Green;
            chan1.LockscreenVisibility = NotificationVisibility.Private;
            Manager.CreateNotificationChannel(chan1);

            var chan2 = new NotificationChannel(SECONDARY_CHANNEL,
                    "Secondary Channel", NotificationImportance.High);
            chan2.LightColor = Color.Blue;
            chan2.LockscreenVisibility = NotificationVisibility.Public;
            Manager.CreateNotificationChannel(chan2);
        }

        public Notification.Builder GetNotification1(String title, String body)
        {
            return new Notification.Builder(ApplicationContext, PRIMARY_CHANNEL)
                     .SetContentTitle(title)
                     .SetContentText(body)
                     .SetSmallIcon(SmallIcon)
                     .SetAutoCancel(true);
        }

        public Notification.Builder GetNotification2(String title, String body)
        {
            return new Notification.Builder(ApplicationContext, SECONDARY_CHANNEL)
                     .SetContentTitle(title)
                     .SetContentText(body)
                     .SetSmallIcon(SmallIcon)
                     .SetAutoCancel(true);
        }

        public void Notify(int id, Notification.Builder notification)
        {
            Manager.Notify(id, notification.Build());
        }

    }

}
