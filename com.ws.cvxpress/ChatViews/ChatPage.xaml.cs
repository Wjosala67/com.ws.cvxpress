using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.ChatViews
{
    public partial class ChatPage : ContentPage
    {
        public ICommand ScrollListCommand { get; set; }

        ChatUIViewModel viewModel;

        public ChatPage(SelectedUser ob, string IdInfo, RequestSpecs requestSpecs)
        {
            InitializeComponent();

            if(requestSpecs.DeliveredAt == "1") {
                ReserveItemObj RO = new ReserveItemObj();
                ApiService _apiService = new ApiService();

                requestSpecs.DeliveredAt = "0";
                RO.requestSpecs = requestSpecs;
                 _apiService.UpdateItemFTravel(RO);
            }else
            {

            }
            if (IdInfo.Contains("-1"))
            {
                Back.IsVisible = false;
                rowone.Height = 1;
                Title = "Chat";
                chaterName.Text = IdInfo.Replace("-1","");
               
            }
            else
            {
                chaterName.Text = IdInfo;
                
            }


            ImageURL.Source = ob.image ;




            this.BindingContext = viewModel = new ChatUIViewModel();
            viewModel.travelID = ob.travelerSpecs.Id;
            viewModel.requesID = requestSpecs.Id;
            viewModel.traveler = ob.travelerSpecs.Email;
            viewModel.sender = requestSpecs.Email;
            ScrollListCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                ChatList.ScrollTo((this.BindingContext as ChatUIViewModel).Messages, ScrollToPosition.End, true)
                              );
            });

            MessagingCenter.Subscribe<ChatIntpuBarView, string>(this, "ScrollList", (obj, Item) => { 
            
               
                if(Item == "Done")
                {
                    ScrollListCommand.Execute(null);
                }
            
            
            } );


            MessagingCenter.Subscribe<App, string>(this, "ChatUpdate", async (obj, item)  => {

                if(item == "Update")
                {
                    await viewModel.ExecuteLoadItemsCommand();
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
    


        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = viewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (vm.DelayedMessages.Count > 0)
                        {
                            vm.Messages.Insert(0, vm.DelayedMessages.Dequeue());
                        }
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;
                        //ChatList?.ScrollToFirst();
                    });


                }

            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            //chatInput.UnFocusEntry();
        }

        protected override void OnAppearing()
        {

           
                Task.Run(async () => {

                   await viewModel.ExecuteLoadItemsCommand();

                   


                }).Wait();



            base.OnAppearing();
        }
    }
}
