using System;
using System.Collections.Generic;
using System.Linq;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using WSViews;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class SpecificsTravelView : ContentPage
    {

        private string Destiny;
        public SpecificsTravelView(string destiny)
        {
            InitializeComponent();
            Destiny = destiny;
            List<int> QtyList = new List<int>();

            if(destiny == "US")
            {
                DestinyMessage.IsVisible = true;
                //DestinyMessage.DefaultText = Translator.getText("DestinyInfo");
                Lb_File.IsVisible = true;
                Lb_File.Text = Translator.getText("SeeFile");
                imgButton.IsVisible = true;
            }
            string stravelerSpecs = (string)Application.Current.Properties[Constants.Tspecs];

            if (stravelerSpecs.Contains("|"))
            {
                WeightAvail.Text = stravelerSpecs.Split('|')[0];
           
                DeliveredAt.Text = stravelerSpecs.Split('|')[1];
            }

            List<App_Con> AppConList = DatabaseHelper.getConfiguration(App.Os_Folder);


            int qty = int.Parse((from conf in AppConList where conf.Name == "ProdQuatity" select conf.Value).FirstOrDefault());

            for (int I =1; I<= qty; I++)
            {
                QtyList.Add(I);
            }
        }

        void  Handle_Clicked(object sender, System.EventArgs e)
        {
            if(Destiny == "US")
            {
                if (DestinyMessage.IsChecked)
                {

                }
                else
                {
                    DisplayAlert(Translator.getText("Error"), Translator.getText("CheckReaded"), "Ok");
                    return;
                }
            }
            Application.Current.Properties[Constants.Tspecs] = WeightAvail.Text + "|" + DeliveredAt.Text;
            Application.Current.SavePropertiesAsync();
            MessagingCenter.Send<SpecificsTravelView, string>(this, "Specs", "Done");
            Navigation.PopModalAsync();

        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            string language = "ES";
            string terms = Constants.ServerUrl + Constants.ServerUrlPdf + Constants.PdfUSGenerals + language + ".pdf";

            await Navigation.PushModalAsync(new InAppWebView(terms,0));
        }
    }
}
