using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class ClientReqDetail : ContentPage
    {
        ClientResViewModel viewModel;
        Client_Service SendCliente_service;
        public ClientReqDetail(Client_Service client_Service)
        {

            SendCliente_service = client_Service;
            InitializeComponent();
            Title = Translator.getText("ResponseUser");
            Topic.Text = client_Service.Title;
            Subject.Text = client_Service.Text;
            BindingContext = viewModel = new ClientResViewModel();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                SendCliente_service.Response = SubjectResponse.Text;
                SendCliente_service.DateResponse = DateTime.Now;
                SendCliente_service.status = 1;

                ApiService apiService = new ApiService();

                bool sendMessage = await apiService.UpdateClientRequesAsync(SendCliente_service);

                if (sendMessage)
                {
                    await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateDone"), "Ok");
                }
                else
                {
                    await DisplayAlert(Translator.getText("Notice"), Translator.getText("UpdateUnDone"), "Ok");
                }

            }

            await Navigation.PopAsync();
        }
    }
}
