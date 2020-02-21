using System;
using System.Collections.Generic;
using com.ws.cvxpress.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.End
{
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
             Title = Translator.getText("Logout");
        }
    }
}
