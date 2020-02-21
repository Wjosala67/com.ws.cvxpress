using System;
using System.Windows.Input;
using com.ws.cvxpress.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = Translator.getText("AboutPage");
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}