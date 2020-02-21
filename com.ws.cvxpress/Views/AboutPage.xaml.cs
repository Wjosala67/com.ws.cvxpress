using System;
using WSViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.ws.cvxpress.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();


            linkFunc();

            void linkFunc()
            {
                Link.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command(async () =>
                    {

                      
                        await Navigation.PushModalAsync(new InAppWebView("http://www.cvexpressweb.com", 1));

                    }
                            )
                });


            }

        }
    }
}