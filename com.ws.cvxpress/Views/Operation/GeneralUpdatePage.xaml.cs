using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class GeneralUpdatePage : ContentPage
    {
        GeneralUpdateViewModel viewModel;
        TravelerSpecs IdInfo;
        public GeneralUpdatePage(TravelerSpecs idInfo)
        {
            InitializeComponent();
            IdInfo = idInfo;
            BindingContext = viewModel = new GeneralUpdateViewModel(idInfo);
            Title = Translator.getText("GeneralUpdate");
            lb_frame1Func();



            DateTime today = DateTime.Now;
            bool isEnabled = false;
            bool isEnabledlevel = false;
            bool isEnabledLevel4 = false;
            if(idInfo.FromDate.Date <= today.Date)
            {
                isEnabled = true ;
            }

            if(idInfo.status > 1)
            {
                isEnabledlevel = true;
            }
            if(idInfo.status > 2)
            {
                isEnabledLevel4 = true;
            }
            MessagingCenter.Subscribe<GeneralUpdateViewModel, string>(this, "DelayedMessage", async (obj, item) => {


                if (item == "Delayed")
                {
                    
                   await PopupNavigation.PushAsync(new PopDelayed(idInfo));
                }


            });


            MessagingCenter.Subscribe<PopDelayed, TravelerSpecs>(this, "TravelUpdated", (obj, item) => {


               if(item != null)
                {

                    viewModel.DateFrom = item.FromDate;
                    viewModel.DateTo = item.ToDate;
                    IdInfo = item;
                    DateFrom.Text = Convert.ToDateTime(item.FromDate.ToString()).ToString("dd/MM/yyyy hh:mm tt");
                    DateTo.Text = Convert.ToDateTime(item.ToDate.ToString()).ToString("dd/MM/yyyy hh:mm tt");
                }
               


            });


            MessagingCenter.Subscribe<GeneralUpdateViewModel, string>(this, "CloseTravel", (obj, item) => {

                if (item == "Close")
                {
                    MessagingCenter.Send<GeneralUpdatePage, string>(this, "UpdateItems", "Done");
                    MessagingCenter.Send<GeneralUpdatePage, TravelerSpecs>(this, "UpdateTravelInfo", IdInfo);

                    Navigation.PopModalAsync();

                }

            });

            void lb_frame1Func()
            {
                try
                {
                    frame1.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command( () =>
                        {

                            viewModel.frameOneAction();
                            

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_frame2Func();

            void lb_frame2Func()
            {
                try
                {
                    frame2.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            if (isEnabled)
                            { 
                                viewModel.frameTwoAction();
                            }else
                            {
                                DisplayAlert(Translator.getText("Notice"), Translator.getText("NoStartDate"), "Ok");
                            }

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_frame22Func();
            void lb_frame22Func()
            {
                try
                {
                    frame22.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                           

                            if (isEnabled)
                            {
                                viewModel.frame22Action();
                            }
                            else
                            {
                                DisplayAlert(Translator.getText("Notice"), Translator.getText("NoUpdateDate"), "Ok");
                            }

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
            lb_frame3Func();

            void lb_frame3Func()
            {
                try
                {
                    frame3.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                           
                     
                            if (isEnabledlevel)
                            {
                                viewModel.frameThreeAction();
                            }
                            else
                            {
                                DisplayAlert(Translator.getText("Notice"), Translator.getText("NoDeliveredDate"), "Ok");
                            }

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_frame4Func();

            void lb_frame4Func()
            {
                try
                {
                    frame4.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {

                          

                            if (isEnabledLevel4)
                            {
                                viewModel.frameFourAction();
                            }
                            else
                            {
                                DisplayAlert(Translator.getText("Notice"), Translator.getText("NoEndtravelDate"), "Ok");
                            }

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

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            // async of the view model for travel state update
            MessagingCenter.Send<GeneralUpdatePage, string>(this, "UpdateItems", "Done");

           
            Navigation.PopModalAsync();
        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            if (viewModel.Status == 9)
            {
                var action = await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateAllItemsAndEnd"), Translator.getText("Yes"), Translator.getText("Cancel"));

                if (action)
                {
                    viewModel.SubmitCommand.Execute(true);
                }
            }else
            {

                if(viewModel.Status == 3)
                {
                    int openRequests = await viewModel.checkOpenRequests(IdInfo.Id);

                    if(openRequests > 0)
                    {
                      await  DisplayAlert(Translator.getText("Notice"), Translator.getText("NotAllAreUpdated"), Translator.getText("Yes"));
                        return;
                    }
                }

                var action = await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateAllItems"), Translator.getText("Yes"), Translator.getText("Cancel"));

               
                if (action)
                {
                    viewModel.SubmitCommand.Execute(true);
                }
                //viewModel.SubmitCommand.Execute(true);
            }
        }
    }
}
