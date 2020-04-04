using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using com.ws.cvxpress.Views.Admin;
using com.ws.cvxpress.Views.Operation;
using com.ws.cvxpress.Views.RegisterA;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views
{
    public partial class ProfilePage : ContentPage
    {
        public string pType { get; set; }
        public ProfilePageViewModel viewModel;
        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProfilePageViewModel();
            Title = Translator.getText("MyProfile");
            
            
            if (Application.Current.Properties.ContainsKey(Constants.UserType))
            {

                pType = (string)Application.Current.Properties[Constants.UserType];

            }
            if (pType == Constants.Traveler)
            {
                rewards.IsVisible = true;


            }

                Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

            MessagingCenter.Subscribe<ImagePage, byte[]>(this, "UserPhoto", (obj, item) => {

                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {
                    viewModel.ImageByte = item;

                    //ImageDB.IsVisible = true;
                    ImageURL.IsVisible = true;
                    ImageURL.Source = ImageManager.BytesToImage(item);

                    Task.Run(async () => { await InitializeAsync(item); }).Wait();

                }


            });

            MessagingCenter.Subscribe<TravellerPage, string>(this, "TravelSaved", (obj, item) => {


                if (item == "Done")


                {
                    Navigation.PushAsync(new TravelsList());
                    Navigation.RemovePage(this);
                }



            });
            if (profile.userImage != null)
            {
                //ImageDB.IsVisible = false;
                ImageURL.IsVisible = true;
                ImageURL.Source = ImageManager.BytesToImage(profile.userImage);
                //ImageDB.Source = ImageManager.BytesToImage(profile.userImage);

            }
            else
            {
                //ImageDB.IsVisible = false;
                ImageURL.Source = "giphy.gif";
                ImageURL.IsVisible = true;
            }

            lb_signupFunc();
            lb_forgotFunc();
            void lb_signupFunc()
            {
                try
                {
                    //ImageDB.GestureRecognizers.Add(new TapGestureRecognizer()
                    //{
                    //    Command = new Command(async () =>
                    //    {


                    //        await Navigation.PushModalAsync(new StepThreePage(Constants.ForUserPic));

                    //    }
                    //        )
                    //});
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            void lb_forgotFunc()
            {
                try
                {
                    ImageURL.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new ImagePage(Constants.ForUserPic));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
            lb_prefFunc();
            void lb_prefFunc()
            {
                try
                {
                    adjustprofile.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new PreferencesPage(Constants.Modal));

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            lb_RewFunc();
            void lb_RewFunc()
            {
                try
                {
                    rewards.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new RewardPage());

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            //CarouselView _carouselView = new CarouselView()
            //{
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //};

            //_carouselView.ItemTemplate = template;
            //_carouselView.SetBinding(ItemsView.ItemsSourceProperty, nameof(_viewModel.CarouselItems));
            //_carouselView.SetBinding(CarouselView.PositionProperty, nameof(_viewModel.Position));

            //// Create page-indicator
            //var indicator = new CarouselIndicators { ItemsSource = _viewModel.CarouselItems };
            //indicator.Margin = new Thickness(20, 20, 20, 0);
            //indicator.SetBinding(CarouselIndicators.PositionProperty, nameof(_viewModel.Position));

            lb_NewsFunc();
            void lb_NewsFunc()
            {
                try
                {
                    bt_News.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {


                            await Navigation.PushModalAsync(new NewsPage());

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }


        }

        private async Task InitializeAsync(byte[] item)
        {
            ApiService apiService = new ApiService();

            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

            profile.userImage = item;

            DatabaseHelper.Update(ref profile, App.Os_Folder);

            Users isUser = await apiService.LoginAsync(profile.Email);

            isUser.UserPhoto = item;

            await apiService.UpdateUserAsync(isUser);


        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            // if exists, loads the email to the form
            if (Application.Current.Properties.ContainsKey(Constants.UserType))
            {

                pType = (string)Application.Current.Properties[Constants.UserType];

            }
            if (pType == Constants.Traveler)
            {
                Navigation.PushModalAsync(new TravellerPage(Constants.Modal));
            }else
            {
                Navigation.PushModalAsync(new RequestPage(Constants.Modal));
            }
        }
    }
}
