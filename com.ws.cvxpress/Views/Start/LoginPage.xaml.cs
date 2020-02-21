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
    public partial class LoginPage : ContentPage
    {
      

            public LoginPage()
            {
            InitializeComponent();
            var vm = new LoginViewModel();
                this.BindingContext = vm;
            // DisplayAlert Actions for user notifications, when was not possible, empty info and no internet available
                vm.DisplayInvalidLoginPrompt += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeLogin"), "OK");
                vm.DisplayInvalidLoginEmpty += () => DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeEmptyData"), "OK");
                vm.DisplayNoInternet += () => DisplayAlert(Translator.getText("Notice"), Translator.getText("NoInternet"), "OK");


                Email.Completed += (object sender, EventArgs e) =>
                {
                    if(Email.Text != null)Email.Text.Trim();
                    Password.Focus();
                };

                Password.Completed += (object sender, EventArgs e) =>
                {
                    //vm.SubmitCommand.Execute(null);
                };
                
                // methods for label gestures
                lb_signupFunc();
                lb_forgotFunc();
                void lb_signupFunc()
                {
                    try
                    {
                        lb_signup.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(async () =>
                            {


                                await Navigation.PushModalAsync(new RegisterPage());

                            }
                                )
                        });
                    }
                    catch (Exception ex) { Debug.WriteLine(ex); }
                }

                void lb_forgotFunc()
                {
                    try
                    {
                        lb_forgot.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(async () =>
                            {


                                await Navigation.PushModalAsync(new ForgotPage());

                            }
                                )
                        });
                    }
                    catch (Exception ex) { Debug.WriteLine(ex); }
                }
            //methods for terms and conditions
            lb_func_terms();

            void lb_func_terms()
            {
                try
                {
                    lb_accepted_terms.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            //loads the pdf file to the webview Add lenguage ID later
                            string language = "ES";
                            string terms = Constants.ServerUrl + Constants.ServerUrlPdf + Constants.PdfTerms + language + ".pdf";

                            await Navigation.PushModalAsync(new InAppWebView(terms, 0));

                        }
                            )
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
        
    }
}
