using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using com.ws.cvxpress.Custom;
using com.ws.cvxpress.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRenderer))]
namespace com.ws.cvxpress.Droid.CustomRenderers
{
    #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
    public class CustomTimePickerRenderer : TimePickerRenderer
    {

        CustomTimePicker element;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            element = (CustomTimePicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {




                Control.Background = AddPickerStyles(element.Image);
            }


        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            //ShapeDrawable border = new ShapeDrawable();
            //border.Paint.Color = Android.Graphics.Color.Black;
            //border.SetPadding(10, 10, 10, 10);
            //border.Paint.SetStyle(Paint.Style.Stroke);

            GradientDrawable gd = new GradientDrawable();
            // increase or decrease to changes the corner 

            gd.SetCornerRadius(18);

            Android.Graphics.Color col = element.BorderColor.ToAndroid();

            gd.SetStroke(1, Android.Content.Res.ColorStateList.ValueOf(col));

           

            Drawable[] layers = { gd, GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(1, 1, 1, 1, 1);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            int widthHeight = 90;

            if (Android.OS.Build.VERSION.SdkInt <= Android.OS.BuildVersionCodes.O)
            { widthHeight = 90; }

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, widthHeight, widthHeight, true));
            result.Gravity = Android.Views.GravityFlags.Right;



            return result;
        }
    }
}
