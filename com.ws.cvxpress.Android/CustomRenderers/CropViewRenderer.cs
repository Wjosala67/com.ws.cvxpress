using System;
using System.IO;
using Acr.UserDialogs;
using Android.Content;
using Android.Graphics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Droid.CustomRenderers;
using Com.Theartofdev.Edmodo.Cropper;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CropView), typeof(CropViewRenderer))]
namespace com.ws.cvxpress.Droid.CustomRenderers
{
    [Obsolete]
    public class CropViewRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var page = Element as CropView;
            if (page != null)
            {
                var cropImageView = new CropImageView(Context);
                cropImageView.LayoutParameters = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                Bitmap bitmp = BitmapFactory.DecodeByteArray(page.Image, 0, page.Image.Length);
                cropImageView.SetImageBitmap(bitmp);

                var scrollView = new ScrollView
                {
                    Content = cropImageView.ToView(),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };




                var stackLayout = new StackLayout
                {
                    Children = { scrollView },
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };





                var rotateButton = new Button { Text = Translator.getText("Rotate") };

                rotateButton.Clicked += (sender, ex) =>
                {
                    cropImageView.RotateImage(90);
                };
                //stackLayout.Children.Add(rotateButton);

                var finishButton = new Button { Text = Translator.getText("Finish") };
               
                    finishButton.Clicked += (sender, ex) =>
                {

                        Bitmap cropped = cropImageView.CroppedImage;


                        using (MemoryStream memory = new MemoryStream())
                    {
                        UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                        cropped.Compress(Bitmap.CompressFormat.Png, 60, memory);
                        App.CroppedImage = memory.ToArray();
                        UserDialogs.Instance.HideLoading();
                    }
                    page.DidCrop = true;

                    page.Navigation.PopModalAsync();
                };

                //stackLayout.Children.Add(finishButton);

                Grid grid = new Grid
                {

                    VerticalOptions = LayoutOptions.Fill,
                    Padding = 5,
                    RowSpacing = 2,




                };

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                grid.RowDefinitions.Add(new RowDefinition { Height = 43 });
                grid.RowDefinitions.Add(new RowDefinition { Height = 43 });

                grid.Children.Add(stackLayout, 0, 0);
                grid.Children.Add(rotateButton, 0, 1);
                grid.Children.Add(finishButton, 0, 2);

                page.Content = grid;
            }
        }
    }
}


