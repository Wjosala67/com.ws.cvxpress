using System;
using System.Collections.Generic;
using com.ws.cvxpress.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class ListOfferPage : TabbedPage
    {
       
        public ListOfferPage()
        {
            InitializeComponent();
           

            var pages = Navigation.NavigationStack;
            if (pages.Count > 0)
            {
                this.Title = pages[pages.Count - 1].Title;
            }
            else
                this.Title = this.CurrentPage.Title;


            CurrentPageChanged += CurrentPageHasChanged;

        }

        private void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;
            Title = tabbedPage.CurrentPage.Title;

            if (Title == "Finalizados")
            {
                MessagingCenter.Send<ListOfferPage, string>(this, "LoadDoneOffers", Title);
            }


        }

        //Update the IsSelected state and trigger an Event that anyone can loop into.

        void Handle_CurrentPageChanged(object sender, EventArgs e)
        {
            string a = "";
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
        }
    }
}
