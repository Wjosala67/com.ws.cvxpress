using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Admin
{
    public partial class NewsPage : ContentPage
    {
        NewPageViewModel viewModel;
        public NewsPage()
        {
            BindingContext = viewModel = new NewPageViewModel();
            InitializeComponent();

            lb_backFunc();
            void lb_backFunc()
            {
                try
                {
                    b_Back.GestureRecognizers.Add(new TapGestureRecognizer()
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

    }
}
