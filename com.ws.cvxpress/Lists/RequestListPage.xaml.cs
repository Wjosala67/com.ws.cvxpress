using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Lists.ListsViewModels;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Views.RegisterA;
using Xamarin.Forms;

namespace com.ws.cvxpress.Lists
{
    public partial class RequestListPage : ContentPage
    {
        RequestListPageViewModel viewModel;
        public RequestListPage(string tracking, int travelID )
        {
            Title = Translator.getText("ItemDesc");
            BindingContext = viewModel = new RequestListPageViewModel(tracking, travelID); 
            InitializeComponent();

            viewModel.ItemSavedAction += () => GoBack();

            viewModel.ItemNotSavedAction += () => NotCreated();

            lb_ProdFunc();
            void lb_ProdFunc()
            {
                try
                {
                    ProductImage.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            await Navigation.PushModalAsync(new ImagePage(Constants.ForChatPics));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }


            MessagingCenter.Subscribe<ImagePage, byte[]>(this, "ChatPhoto", (sender, arg) => {

                if (arg != null) {
                    viewModel.ImageByte = arg;
                    ProductImage.Source = "imagefilledgreen.png";
               }


            });

           



        }

        private void NotCreated()
        {
            DisplayAlert(Translator.getText("Notice"), Translator.getText("ProcessNotCompleted"), "OK");
            Navigation.PopAsync();
        }

        public void GoBack()
        {
            Navigation.PopAsync();
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {


            



        }


         void Handle_Clicked_3(object sender, System.EventArgs e)
        {
          
            Navigation.PopAsync();
           
        }




        void Handle_Clicked(object sender, System.EventArgs e)
        {

          
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
           
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            if (string.IsNullOrEmpty(E_ItemName.Text) || string.IsNullOrEmpty(E_ItemDesc.Text)
                 || string.IsNullOrEmpty(E_ItemFind.Text) || string.IsNullOrEmpty(E_ItemQty.Text)
                  || string.IsNullOrEmpty(E_ItemWeigth.Text) || string.IsNullOrEmpty(E_ItemValue.Text)
                  || PickerCat.SelectedIndex == -1
                 )
            {
                DisplayAlert(Translator.getText("Notice"), Translator.getText("NoCompleteObjetc"), "Ok");
            }else
            {
                viewModel.AddItemCommand.Execute(this);
            }


        }
    }
}
