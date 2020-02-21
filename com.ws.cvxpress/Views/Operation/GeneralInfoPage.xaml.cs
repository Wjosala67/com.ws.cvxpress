using System;
using System.Collections.Generic;
using com.ws.cvxpress.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class GeneralInfoPage : ContentPage
    {
        public GeneralInfoPage()
        {
            InitializeComponent();
            Title = Translator.getText("GenInfo");
        }
    }
}
