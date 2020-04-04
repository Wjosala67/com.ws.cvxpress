using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.ChatViews;
using WSViews.Classes;
using Xamarin.Forms;
using System.Diagnostics;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class RequestInfoPage : ContentPage
    {
        RequestInfoPageViewModel viewModel;
        string ComesFrom;
        RequestSpecs IdInfo;
        public RequestInfoPage(RequestSpecs idInfo, string comesfrom)
        {
            InitializeComponent();
            IdInfo = idInfo;
            ComesFrom = comesfrom;
            BindingContext = viewModel = new RequestInfoPageViewModel(idInfo);
            Title = Translator.getText("RequestInfo");

            if (ComesFrom == "0") { Back.IsVisible = false; rowcero.Height = 1; }
                

            MessagingCenter.Subscribe<RequestInfoPageViewModel, string>(this, "UserNoUser", async (obj, item) => {


                if(item.Split('-')[0] == "NoUser")
                {
                    MyFrameView.IsVisible = false;
                    RequestedAcepted.IsVisible = true;
                   
                }
                else
                {
                    MyFrameView.IsVisible = true;
                    RequestedAcepted.IsVisible = false;
                    FirstName.Text = viewModel.FirstName;
                    //LastName.Text = viewModel.LastName;
                   
                    ImageURL.Source =  viewModel.Image;
                    Someone.Text = Translator.getText("someoneaccepted")
                    ;
                    if (idInfo.status == Constants.Confirmed)
                    {
                      
                        txt_auth.IsVisible = true;
                        txt_auth.Text = Translator.getText("Authorized");
                    }

                    if (viewModel.ShowChat) { 
                    
                    ImgChat.IsVisible = true;
                        Ratlabel.Text = Translator.getText("OpenChat");

                    }else

                   

                    if (viewModel.ShowClick && int.Parse(item.Split('-')[1]) == 0) 
                    { 
                        ImgTouch.IsVisible = true;
                        Ratlabel.Text = Translator.getText("Rating");
                    }
                    else 
                    {
                        string valoration = "";
                        switch (int.Parse(item.Split('-')[1]))
                        {
                            case 1:
                                valoration = Translator.getText("Bad");
                                break;
                            case 2:
                                valoration = Translator.getText("Regular");
                                break;
                            case 3:
                                valoration = Translator.getText("Good");
                                break;
                            case 4:
                                valoration = Translator.getText("VeryGood");
                                break;
                            case 5:
                                valoration = Translator.getText("Excellent");
                                break;
                        }
                        ImgTouch.IsEnabled = false;
                        ImgTouch.IsVisible = true;
                        ImgTouch.Source = "filledstar.png";
                        Ratlabel.Text = valoration;
                        Someone.Text = Translator.getText("someoneDelivered");
                    }
                }

                

                RequestedAcepted.Text = Translator.getText("NoAccepted");
                int stats = viewModel.Status;

               if(IdInfo.status == Constants.RequestAuth)
                {
                    bt_auth.IsVisible = true;
                    bt_Canc.IsVisible = true;
                    txt_auth.IsVisible = true;
                    MyFrameView.IsVisible = true;
                    FirstName.Text = viewModel.FirstName;
                    //LastName.Text = viewModel.LastName;
                    ImageURL.Source = viewModel.Image;
                    txt_auth.Text = (IdInfo.Long == 1) ? Translator.getText("autorizechargechanged").Replace("{0}", (idInfo.Commission + idInfo.ProductValue).ToString()).Replace("{1}", App.strCurrency) : Translator.getText("autorizecharge").Replace("{0}", (idInfo.Commission + idInfo.ProductValue).ToString()).Replace("{1}", App.strCurrency);
                    
                    //rowOne.Height = new GridLength(3, GridUnitType.Star);
                    //rowTwo.Height = new GridLength(2, GridUnitType.Star);
                }

                switch (stats)
                {
                    case 0:
                        viewModel.ImageSource1 = "baggrey.png";

                        viewModel.ImageSource2 = "takeoffgrey.png";

                        viewModel.ImageSource3 = "dcheckgrey.png";

                        viewModel.ImageSource4 = "landinggrey.png";

                       
                        break;

                    case 1:
                        viewModel.ImageSource1 = "bagorange.png";
                        viewModel.ImageSource2 = "takeoffgrey.png";

                        viewModel.ImageSource3 = "dcheckgrey.png";

                        viewModel.ImageSource4 = "landinggrey.png";
                        viewModel.Bought = Translator.getText("someoneaccepted");
                        break;
                    case 2:
                        viewModel.ImageSource1 = "bagorange.png";
                        viewModel.ImageSource2 = "takeofforange.png";

                        viewModel.ImageSource3 = "dcheckgrey.png";

                        viewModel.ImageSource4 = "landinggrey.png";
                        viewModel.Started = Translator.getText("UserStatusStarted");
                        break;
                    case 3:
                        viewModel.ImageSource1 = "bagorange.png";
                        viewModel.ImageSource2 = "takeofforange.png";
                        viewModel.ImageSource3 = "dcheckorange.png";
                        viewModel.ImageSource4 = "landinggrey.png";
                        viewModel.Delivered = Translator.getText("UserStatusDelivered");
                        break;
                    case 4:
                        viewModel.ImageSource1 = "bagorange.png";
                        viewModel.ImageSource2 = "takeofforange.png";
                        viewModel.ImageSource3 = "dcheckorange.png";
                        viewModel.ImageSource4 = "landingorange.png";
                        viewModel.Ended = Translator.getText("UserStatusEnded");
                        break;
                    case 5:
                        viewModel.ImageSource1 = "bagorange.png";
                        viewModel.ImageSource2 = "delayedorange.png";
                        viewModel.ImageSource3 = "dcheckgrey.png";
                        viewModel.ImageSource4 = "landinggrey.png";
                        viewModel.Started = Translator.getText("UserStatusDelayed");
                        break;
                    case 9:

                        viewModel.ImageSource1 = "bagorange.png";

                        viewModel.ImageSource2 = "takeofforange.png";

                        viewModel.ImageSource3 = "dcheckorange.png";

                        viewModel.ImageSource4 = "landingorange.png";

                        viewModel.Ended = Translator.getText("UserStatusEnded");



                        break;
                   
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

            lb_chatFunc();
            void lb_chatFunc()
            {
                try
                {
                    ImgChat.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            SelectedUser ob = new SelectedUser();
                            ob = viewModel.UserAccept;
                            ob.image = viewModel.Image;
                            Navigation.PushModalAsync(new ChatPage(ob, viewModel.FirstName + " " + viewModel.LastName, IdInfo));
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }


            lb_touchFunc();
            void lb_touchFunc()
            {
                try
                {
                    ImgTouch.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            SelectedUser ob = new SelectedUser();
                            ob = viewModel.UserAccept;
                            Navigation.PushModalAsync(new DetailAcceptedPage(ob, IdInfo));
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
        }

        void Handle_Clicked_3(object sender, System.EventArgs e)
        {
           
        }

        void Handle_Clicked_2(object sender, System.EventArgs e)
        {
          
        }
        async void  Handle_auth(object sender, System.EventArgs e)
        {

           
                App.WaitScreenStart(Translator.getText("Loading"));

                //sender envia autorización con status 20
                bool added = await viewModel.AuthCharge();

                if (added)
                {


                    await DisplayAlert(Translator.getText("Notice"), Translator.getText("AuthSent"), "Ok");
                    if (ComesFrom != "0") await Navigation.PopModalAsync();
                    else
                    {


                        txt_auth.IsVisible = true;
                        txt_auth.Text = Translator.getText("Authorized");
                        bt_auth.IsVisible = false;
                        bt_Canc.IsVisible = false;
                    }
                }
                
                App.WaitScreenStop();
           
        }

        async void Handle_canc(object sender, System.EventArgs e)
        {


            App.WaitScreenStart(Translator.getText("Loading"));

            // se envia articulo con status cancelado 18
            bool added = await viewModel.RefuseCharge();

            if (added)
            {

                if (ComesFrom != "0") await Navigation.PopModalAsync();
                else
                {
                    await DisplayAlert(Translator.getText("Notice"), Translator.getText("AuthDenied"), "Ok");
                    App.Current.MainPage = new MainPage
                    {

                        //Title = Translator.getText("MyRequests"),
                        Master = new MenuPage(),
                        Detail = new NavigationPage(new RequestList())
                    };
                }



                App.WaitScreenStop();

            }

        }

            void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
            {

                if (viewModel.ShowClick)
                {
                    SelectedUser ob = new SelectedUser();
                    ob = (SelectedUser)e.Item;

                    Navigation.PushModalAsync(new DetailAcceptedPage(ob, IdInfo));
                }
                else if (viewModel.ShowChat)
                {

                }
            }

            void Handle_Clicked(object sender, System.EventArgs e)
            {

            }

            void Handle_Clicked_1(object sender, System.EventArgs e)
            {
                Navigation.PopModalAsync();
            }
        
        protected override void OnAppearing()
        {


           
                    
                    viewModel.onsomecomandAsync();
                    //if(user != null  && viewModel.LstRequestsAccepted.Count > 0)
                    //MyListView.ItemsSource = viewModel.LstRequestsAccepted;


               



            base.OnAppearing();
        }
    }
}
