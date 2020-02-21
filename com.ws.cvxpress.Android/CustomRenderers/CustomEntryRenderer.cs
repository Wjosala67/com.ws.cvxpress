using System;
using System.Runtime.Remoting.Contexts;
using Android.Content;
using Android.Graphics.Drawables;
using com.ws.cvxpress.Custom;
using com.ws.cvxpress.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Graphics.ColorSpace;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace com.ws.cvxpress.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            CustomEntry elementcustom;

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                // increase or decrease to changes the corner 
               
                gd.SetCornerRadius(18);

                elementcustom = (CustomEntry)this.Element;



                Android.Graphics.Color col = elementcustom.BorderColor.ToAndroid(); 
                
                gd.SetStroke(1, Android.Content.Res.ColorStateList.ValueOf(col));

                this.Control.SetBackgroundDrawable(gd);
            }
        }
    }
}
