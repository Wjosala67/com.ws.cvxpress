using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class DetailAcceptedPage : ContentPage
    {
        RattingBarViewModal viewModel;
        SelectedUser OB;
        RequestSpecs rspecs;
        public DetailAcceptedPage(SelectedUser ob, RequestSpecs IdInfo)
        {
            InitializeComponent();
            OB = ob;
            rspecs = IdInfo;
            BindingContext = viewModel = new RattingBarViewModal(ob);

            if (OB.image == null) ImageURL.Source = "giphy.gif";

            lb_star1Func();

            void lb_star1Func()
            {
                try
                {
                    star1.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                            viewModel.functionimage1();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
            lb_star2Func();

            void lb_star2Func()
            {
                try
                {
                    star2.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                            viewModel.functionimage2();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
            lb_star3Func();

            void lb_star3Func()
            {
                try
                {
                    star3.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                            viewModel.functionimage3();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
            lb_star4Func();

            void lb_star4Func()
            {
                try
                {
                    star4.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                            viewModel.functionimage4();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_star5Func();

            void lb_star5Func()
            {
                try
                {
                    star5.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                            viewModel.functionimage5();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            MessagingCenter.Subscribe<RattingBarViewModal, string>(this, "RatingMessage", async (obj, item) => {


                if (item == "Rating")
                {

                   var action = await DisplayAlert(Translator.getText("Notice"), Translator.getText("SendRating"), Translator.getText("Yes"), Translator.getText("No"));

                    if (action)
                    {
                      
                      string response =  await viewModel.SendRating(OB, rspecs);

                        if(response == "Created")
                        {
                           await Navigation.PopModalAsync();
                        }
                    }


                }


            });


            lb_backFunc();
            void lb_backFunc()
            {
                try
                {
                    Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            Navigation.PopModalAsync();
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
        }

    


        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
