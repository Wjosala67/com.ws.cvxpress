using System;
using Android.Graphics;
using com.ws.cvxpress.Custom;
using com.ws.cvxpress.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Underline), typeof(UnderlineRenderer))]
namespace com.ws.cvxpress.Droid.CustomRenderers
{
    public class UnderlineRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                Control.PaintFlags = PaintFlags.UnderlineText;
                Control.SetTextColor(Android.Graphics.Color.White);
            }

        }
    }
}
