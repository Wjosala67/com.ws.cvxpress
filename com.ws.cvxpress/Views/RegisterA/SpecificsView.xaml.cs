using System;
using System.Collections.Generic;
using System.Linq;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class SpecificsView : ContentPage
    {
      
        public SpecificsView()
        {
            InitializeComponent();

            List<int> QtyList = new List<int>();

            List<App_Con> AppConList = DatabaseHelper.getConfiguration(App.Os_Folder);


            int qty = int.Parse((from conf in AppConList where conf.Name == "ProdQuatity" select conf.Value).FirstOrDefault());

            for (int I = 1; I <= qty; I++)
            {
                QtyList.Add(I);
            }

            PickerQty.ItemsSource = QtyList;



        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if(string.IsNullOrEmpty(Weight.Text) ||
                string.IsNullOrEmpty(Weight.Text) ||
                string.IsNullOrEmpty(Weight.Text) || 
                string.IsNullOrEmpty(Weight.Text) || 
                string.IsNullOrEmpty(Weight.Text) || 
            string.IsNullOrEmpty(Weight.Text))
            {
                DisplayAlert(Translator.getText("Error"), Translator.getText("NoticeEmptyRegister"), "Ok");
            }
            else { 

                MessagingCenter.Send<SpecificsView>(this, "SpecificsView");
            Application.Current.Properties[Constants.Sspecs] = Weight.Text + "|" + 
                                                               Wide.Text + "|" + 
                                                               Height.Text+ "|" + 
                                                               Long.Text+ "|" + 
                                                               DeliveredAt.Text + "|" + 
                                                               PickerQty.SelectedItem;
            Application.Current.SavePropertiesAsync();
            Navigation.PopModalAsync();
            }
        }
    }
}
