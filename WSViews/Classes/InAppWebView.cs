using System;
using Xamarin.Forms;

namespace WSViews
{
    public class InAppWebView : ContentPage
    {
        WebView webView;


        public InAppWebView(string URL, int type)
        {



            this.Title = "Browser";

            this.BackgroundImageSource = "SplashImage.jpg";

            var layout = new StackLayout { Margin = new Thickness(5) };
            var controlBar = new StackLayout() { Orientation = StackOrientation.Horizontal,  HorizontalOptions= LayoutOptions.Center};
            var grig = new Grid();

            grig.RowDefinitions.Add(new RowDefinition() { Height = 30});
            grig.RowDefinitions.Add(new RowDefinition() { Height = 10 });
            grig.ColumnDefinitions.Add(new ColumnDefinition { Width = 30 });
           
            var backButton = new Image { Source="backizq.png", HeightRequest=30, WidthRequest=30, BackgroundColor=Color.Transparent , HorizontalOptions = LayoutOptions.Center};
            grig.Children.Add(backButton, 0,0);

            backButton.GestureRecognizers.Add( new TapGestureRecognizer()
                        {
                Command = new Command(async () =>
                {


                    await Navigation.PopModalAsync();

                }
                    )
                        });


            string source = "";

              source = (type == 0) ? "https://docs.google.com/viewer?url=" + URL : URL;

            webView = new WebView() { HeightRequest=1000, WidthRequest= 1000,  Source = source };

            controlBar.Children.Add(grig);

            layout.Children.Add(controlBar);
            layout.Children.Add(webView);

            Content = layout;
        }



         async void BackButton_Clicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack) {

                webView.GoBack();

            }
            else {

                await Navigation.PopModalAsync();
             
            }
        }

    }
}
