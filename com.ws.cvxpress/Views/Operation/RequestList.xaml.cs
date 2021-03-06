﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.ws.cvxpress.Views.Operation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestList : TabbedPage
    {
        public RequestList()
        {
            InitializeComponent();
            Title = Translator.getText("MyRequests");

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

            if(Title == "Finalizados")
            {
                MessagingCenter.Send<RequestList, string>(this, "LoadDoneRequests", Title);
            }

           
        }
    }
}
