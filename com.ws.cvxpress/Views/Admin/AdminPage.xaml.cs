using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Views.Operation;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Admin
{
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
            Title = Translator.getText("AdminOptions");
            lb_attFunc();
            void lb_attFunc()
            {
                try
                {
                    bt_Atencion.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushAsync(new ListClientsRequests());

                        }
                                )
                    }); ;
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_ApprovalFunc();
            void lb_ApprovalFunc()
            {
                try
                {
                    bt_Approval.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            
                            await Navigation.PushAsync(new AuthListPage());

                        }
                                )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

        }
    }
}
