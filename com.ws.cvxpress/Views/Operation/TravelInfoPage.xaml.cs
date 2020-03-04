using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.ChatViews;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.Services;
using com.ws.cvxpress.ViewModels;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class TravelInfoPage : ContentPage
    {
        TravelerSpecs IdInfo;
        TravelInfoPageViewModel viewModel;
        ApiService apiService;
        public Command LoadItemsCommand { get; set; }
        public TravelInfoPage(TravelerSpecs idInfo)
        {
            InitializeComponent();
            IdInfo = idInfo;
            Title = Translator.getText("TravelInfo");
          
            BindingContext = viewModel = new TravelInfoPageViewModel( idInfo);
            viewModel.IsBusy = false;
            SearchBox.Placeholder = Translator.getText("AcceptedOnRoute").Replace(":","");
            SearchBox.PlaceholderColor = Color.Black;

            #region Updates From GeneralUpdatePage

            MessagingCenter.Subscribe<GeneralUpdatePage, string>(this, "CloseTravel", async (obj, item) => {

                if (item == "Close")
                {
                    await Navigation.PopModalAsync();

                }

            });


            MessagingCenter.Subscribe<GeneralUpdateViewModel, string>(this, "UpdateItems", async (obj, item) => {

                if (item == "Done")
                {
                    using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                    {
                        await viewModel.onsomecomandAsync();
                        updatePage();
                       

                    }

                }

            });


            MessagingCenter.Subscribe<GeneralUpdatePage, TravelerSpecs>(this, "UpdateTravelInfo", async (obj, item) => {


                viewModel.DateFrom = item.FromDate;
                viewModel.DateTo = item.ToDate;

                DateFrom.Text = Convert.ToDateTime(item.FromDate.ToString()).ToString("dd/MM/yyyy hh:mm tt");
                DateTo.Text = Convert.ToDateTime(item.ToDate.ToString()).ToString("dd/MM/yyyy hh:mm tt");



            });


          

            #endregion

            MessagingCenter.Subscribe<UnitUpdateViewModel, string>(this, "UnitUpdateFrom", async (obj, item) => {

                if (item == "NoContent")
                {

                    await viewModel.onsomecomandAsync();
                    updatePage();
                   

                }

            });

            

            MessagingCenter.Subscribe<TravelInfoPageViewModel, string>(this, "DeletedItem1", async (obj, item) => {

                if (item != "No Content")
                {
                    
                
                  await  DisplayAlert(Translator.getText("Notice"), Translator.getText("NoDelete"), "Ok");
                }

            });

            MessagingCenter.Subscribe<App, string>(this, "NewItemsUpdate", async (obj, item) => {

                if (item == "Update")
                {
                    await viewModel.onsomecomandAsync();
                    updatePage();
                  
                }

            });

            MessagingCenter.Subscribe<UnitUpdatePage, string>(this, "UpdateFromUnit", async (obj, item) => {

                if (item == "Update")
                {
                    await viewModel.onsomecomandAsync();
                    updatePage();

                }

            });

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


            lb_discardFunc();
            void lb_discardFunc()
            {
                try
                {
                    discardDown.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            dismissBottomSheet();
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
            

          lb_openFunc();
            void lb_openFunc()
            {
                try
                {
                    openDrawer.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            openBottomSheet();
                        })
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
        }

        void updatePage()
        {
           
            MyListView.ItemsSource = null;
            MyListView2.ItemsSource = null;
            MyListView.ItemsSource = viewModel.LstItemsShow;
            MyListView2.ItemsSource = viewModel.LstItemsShow2;
            MyListView.IsRefreshing = false;
            MyListView2.IsRefreshing = false;
            IdInfo = viewModel.TravelInfo.travelerSpecs;


        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
           
           if (e.Item == null)
                return;
            
            RequestSpecs requestSpecs = (RequestSpecs)e.Item;

            if (viewModel.CapacityNumber < requestSpecs.Weight)
            {


                await DisplayAlert(Translator.getText("Notice"), Translator.getText("NotEnoughRoom"), "Ok");

            }
            else { 
                var Action = await DisplayAlert(Translator.getText("Notice"), Translator.getText("TransportQuestion"), Translator.getText("Yes"), Translator.getText("No")); ;

                if (Action)
                {
                    App.WaitScreenStart(Translator.getText("Loading"));

                    bool added = await viewModel.ReserveItem(requestSpecs, IdInfo);
                    App.WaitScreenStop();
                    if (!added)
                        {


                            await DisplayAlert(Translator.getText("Notice"), Translator.getText("ItemTaken"), "Ok"); ;

                        }
                        else
                        {
                          
                           
                            await viewModel.onsomecomandAsync();
                            updatePage();
                          
                        await DisplayAlert(Translator.getText("Notice"), Translator.getText("ItemTakenbyYou"), "Ok");
                    }
                   
                    }
                }


            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {

            await Navigation.PushModalAsync(new GeneralUpdatePage(IdInfo));
            dismissBottomSheet();
        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new UnitListPage(IdInfo));
            dismissBottomSheet();
        }

        async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new RequestsListPage());
        }


        protected override void OnAppearing()
        {



          
           
          



            base.OnAppearing();
        }

        /// jjjjj
        ///

        void ToolItem_Clicked(object sender, System.EventArgs e)
        {
            //RecipesListView.ItemsSource = RecipeData.RecipesList;
            //IngredientsListView.ItemsSource = IngredientsData.IngredientsList;
        }

        void Handle_IngredientSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
                return;

            //var ingredient = (Ingredient)e.SelectedItem;

            ////filter the ListView
            //var recipes = (List<Recipe>)RecipesListView.ItemsSource;
            //var filteredRecipes = new List<Recipe>();
            //foreach (var recipe in recipes)
            //{
            //    if (recipe.Name.Contains(ingredient.Name))
            //    {
            //        filteredRecipes.Add(recipe);
            //    }
            //}

            //RecipesListView.ItemsSource = filteredRecipes;
            ((ListView)sender).SelectedItem = null;
            dismissBottomSheet();
        }


        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Accepted = viewModel.LstItemsShow2;

            if (e.NewTextValue != string.Empty)
            {
                //only show the grid view when no text is displayed
                

                var filteredItems = new List<RequestSpecs>();
                foreach (var request in Accepted)
                {
                    if (request.Description.ToString().Contains(e.NewTextValue))
                    {
                        filteredItems.Add(request);
                    }
                }
                MyListView2.ItemsSource = filteredItems;
                return;
            }

            MyListView2.ItemsSource = viewModel.LstItemsShow2;
            //GridFilter.IsVisible = true;
        }


        // Important Code Lives Below
        double x, y;


        void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            //GridFilter.IsVisible = true;
            openBottomSheet();
        }


        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            // Handle the pan
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't y + e.TotalY pan beyond the wrapped user interface element bounds.
                    var translateY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs((Height * .25) - Height));
                    bottomSheet.TranslateTo(bottomSheet.X, translateY, 20);
                    break;
                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    y = bottomSheet.TranslationY;

                    //at the end of the event - snap to the closest location
                    var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getClosestLockState(e.TotalY + y)));

                    //depending on Swipe Up or Down - change the snapping animation
                    if (isSwipeUp(e))
                    {
                        bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 250, Easing.SpringIn);
                    }
                    else
                    {
                        bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 250, Easing.SpringOut);
                    }

                    //dismiss the keyboard after a transition
                    SearchBox.Unfocus();
                    y = bottomSheet.TranslationY;

                    break;

            }

        }

        public bool isSwipeUp(PanUpdatedEventArgs e)
        {
            if (e.TotalY < 0)
            {
                return true;
            }
            return false;
        }

        //TO-DO: Make this cleaner
        public double getClosestLockState(double TranslationY)
        {
            //Play with these values to adjust the locking motions - this will change depending on the amount of content ona  apge
            var lockStates = new double[] { 0, .5, .85 };

            //get the current proportion of the sheet in relation to the screen
            var distance = Math.Abs(TranslationY);
            var currentProportion = distance / Height;

            //calculate which lockstate it's the closest to
            var smallestDistance = 10000.0;
            var closestIndex = 0;
            for (var i = 0; i < lockStates.Length; i++)
            {
                var state = lockStates[i];
                var absoluteDistance = Math.Abs(state - currentProportion);
                if (absoluteDistance < smallestDistance)
                {
                    smallestDistance = absoluteDistance;
                    closestIndex = i;
                }
            }

            var selectedLockState = lockStates[closestIndex];
            var TranslateToLockState = getProportionCoordinate(selectedLockState);

            return TranslateToLockState;
        }

        public double getProportionCoordinate(double proportion)
        {
            return proportion * Height;
        }

        void dismissBottomSheet()
        {

            SearchBox.Unfocus();
            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getProportionCoordinate(0)));
            bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 450, Easing.SpringOut);
            SearchBox.Text = string.Empty;
            openDrawer.IsVisible = true;
            discardDown.IsVisible = false;
          
        }

        void openBottomSheet()
        {
            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getProportionCoordinate(.87)));
            bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 150, Easing.SpringIn);
            openDrawer.IsVisible = false;
            discardDown.IsVisible = true;
           
        }

       
        private void Handle_Clicked_5(object sender, System.EventArgs e)
        {



            dismissBottomSheet();
            ReserveItemObj Ro = new ReserveItemObj();
            Ro.travelerSpecs = IdInfo;


            var menuItem = sender as Button;
            var selectedItem = menuItem.CommandParameter as RequestSpecs;
            Ro.requestSpecs = selectedItem;
            Navigation.PushModalAsync(new UnitUpdatePage(Ro));

        }

        async void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            var action = await DisplayAlert(Translator.getText("Notice"), Translator.getText("WhantDelete"), Translator.getText("Yes"), Translator.getText("No"));

            if (action)
            {
                ReserveItemObj Ro = new ReserveItemObj();
                Ro.travelerSpecs = IdInfo;


                var menuItem = sender as Button;
                var selectedItem = menuItem.CommandParameter as RequestSpecs;
                Ro.requestSpecs = selectedItem;
                viewModel.DeleteItem(Ro);



            }
        }
        void Handle_Clicked_4(object sender, System.EventArgs e)
        {


            RequestSpecs Rs = new RequestSpecs();
            SelectedUser ob = new SelectedUser();
            ob.travelerSpecs = IdInfo;
            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

            apiService = new ApiService();
            Users user = new Users();




            var menuItem = sender as Button;
            Rs = menuItem.CommandParameter as RequestSpecs;

            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {
                Task.Run(async () => {

                    user = await apiService.LoginAsync(Rs.Email);



                }).Wait();

            }

            ob.image = (user.UserPhoto != null) ? ImageManager.BytesToImage(user.UserPhoto) : "giphy.gif";
            
            Navigation.PushModalAsync(new ChatPage(ob, user.FirstName + " " + user.LastName, Rs));
        }


    }
}
