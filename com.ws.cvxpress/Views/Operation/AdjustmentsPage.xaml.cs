using System;
using System.Collections.Generic;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class AdjustmentsPage : ContentPage
    {
        AdjustmentPageViewModel viewModel;
        public string pType;
        public AdjustmentsPage()
        {

            InitializeComponent();
            BindingContext = viewModel = new AdjustmentPageViewModel();


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
            Navigation.PopModalAsync();
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            var obj = viewModel.LstItemType.ToList().FirstOrDefault();

            pType = (obj.IsSelected) ? Constants.Enabled :Constants.Disabled;
            Application.Current.Properties[Constants.Notifications] = pType;
            Application.Current.SavePropertiesAsync();
            Navigation.PopModalAsync();
        }
    }
}
