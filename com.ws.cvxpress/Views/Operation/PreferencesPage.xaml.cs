using System;
using System.Collections.Generic;
using System.Diagnostics;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Views.RegisterA;
using com.ws.cvxpress.Views.Start;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class PreferencesPage : ContentPage
    {
        string pType { get; set; }
        public PreferencesPage(string type)
        {
            InitializeComponent();
            Title = Translator.getText("Settings");

            if (Application.Current.Properties.ContainsKey(Constants.UserType))
            {

                pType = (string)Application.Current.Properties[Constants.UserType];

            }
            if (pType == Constants.Traveler)
            {
                bt_prod.IsVisible = true;
                Lb_art.IsVisible = true;
            } else
            {
                bt_prod.IsVisible = false;
                Lb_art.IsVisible = false;
            }

            if(type == Constants.Modal)
            {
                Gridmodal.IsVisible = true;
            }

                img_InfoFunc();
            img_prodFunc();
            img_IdFunc();
            img_passCh();
            img_conf();
            void img_InfoFunc()
            {
                try
                {
                    bt_info.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new StepOnePage(Constants.RegisterNoCall));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            void img_prodFunc()
            {
                try
                {
                    bt_prod.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new StepTwoPage(Constants.RegisterNoCall));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            void img_IdFunc()
            {
                try
                {
                    bt_ident.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new ImagePage(Constants.ForIdPics));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            void img_passCh()
            {
                try
                {
                    bt_chPass.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new ChangePwdPage());

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
            void img_conf()
            {
                try
                {
                    bt_conf.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new AdjustmentsPage());

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

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

            lb_CardsFunc();
            void lb_CardsFunc()
            {
                try
                {
                    bt_cards.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {
                            await Navigation.PushModalAsync(new MyCardsPage());
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
    }
}
