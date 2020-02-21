using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.ViewModels;
using WSViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.ws.cvxpress.Views.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {

            var vm = new RegisterPageViewModel();
            // DisplayAlert Actions for user notifications, when was not possible, empty info and no internet available
            this.BindingContext = vm;

          
            InitializeComponent();
           
            MessagingCenter.Subscribe<RegisterPageViewModel, string>(this, "RegisterMessages", (obj, item) => {


                DisplayAlert(item.Split('-')[0], item.Split('-')[1], "Ok");


            });
            string language = "ES";
            //methods for terms and conditions
            lb_func_terms();

            void lb_func_terms()
            {
                try
                {
                    lb_terms.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {
                            string terms = Constants.ServerUrl + Constants.ServerUrlPdf + Constants.PdfTerms + language + ".pdf";
                            //loads the pdf file to the webview
                            await Navigation.PushModalAsync(new InAppWebView(terms,0));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_func_privacy();

            void lb_func_privacy()
            {
                try
                {
                    lb_privacy.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {
                            string privacy = Constants.ServerUrl + Constants.ServerUrlPdf + Constants.PdfPrivacy + language + ".pdf";
                            //loads the pdf file to the webview
                            await Navigation.PushModalAsync(new InAppWebView(privacy, 0));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }


            lb_backFunc();
            void lb_backFunc()
            {
                try
                {
                    b_Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command( () =>
                        {
                            Navigation.PopModalAsync();
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_func_facebook();

            void lb_func_facebook()
            {
                try
                {
                    buttonFacebook.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            //loads the pdf file to the webview Add lenguage ID later
                            vm.OnLoginCommand.Execute(true);

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

        }

      

      

       

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
