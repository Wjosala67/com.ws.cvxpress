using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Plugin.Media;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class StepThreePage : ContentPage
    {

        public StepThreeViewModel ViewModels { get; }
        private string CalledFrom { get; set; }
        private IdInfo idInfo { get; set; }
        public string coments { get; set; }
        public StepThreePage(string calledfrom)
        {
            InitializeComponent();
            CalledFrom = calledfrom;
            BindingContext = ViewModels = new StepThreeViewModel(CalledFrom);
            ViewModels.NoRegisterCall += () => ShowResult();

            ViewModels.VerifyProfile += async () => await GoToLoginAsync();

            switch (CalledFrom)
            {
                case "ForUserPic":
                    coments = Translator.getText("UserPhoto");
                    CamTitle.Text = Translator.getText("UserPhoto");
                    break;
                case "ForIdPics":
                    coments = Translator.getText("IdPhoto");
                    CamTitle.Text = Translator.getText("TextIdents"); 
                    break;
                case "ForProductPic":
                    coments = Translator.getText("ProductPhoto");
                    CamTitle.Text = Translator.getText("ProductPhoto");
                    break;
                case "ForIdTrav":
                    coments = Translator.getText("IdPhoto");
                    CamTitle.Text = Translator.getText("IdPhoto");
                    break;
            }

            TakePicture.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    CompressionQuality = 90,
                    AllowCropping = true
                });

                if (file == null)
                    return;

                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {

                    idInfo = new IdInfo
                    {

                        comments = coments,
                        Image = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        }),
                        ImageByte = ImageManager.ConvertToBytes(file.Path),
                    };
                }

                    createImage(file.Path, idInfo);






            };

            SelectPicture.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported )
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    CompressionQuality = 90,
                   
                });

                if (file == null)
                    return;

                //await DisplayAlert("File Location", file.Path, "OK");


                using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
                {

                    idInfo = new IdInfo
                    {

                        comments = coments,
                        Image = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        }),
                        ImageByte = ImageManager.ConvertToBytes(file.Path),
                    };
                }

                    createImage(file.Path, idInfo);
                

            };


            lb_BackFunc();
            void lb_BackFunc()
            {
                try
                {
                    Back.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {

                            Navigation.PopModalAsync();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }
        }

        private async Task GoToLoginAsync()
        {
            await DisplayAlert(Translator.getText("Success"), Translator.getText("UpdateDone"), "Ok");
            await Navigation.PopModalAsync();
        }

        private async void createImage(string path, IdInfo idInfob)
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                byte[] imagetosave = ImageManager.ConvertToBytes(path);

                if (CalledFrom == Constants.ForIdPics)
                {
                    if (ViewModels.IdInfos.Count < 3)
                    {
                        ViewModels.id_document_type = 1;
                        ViewModels.addIdInfo(idInfob);
                       
                        MessagingCenter.Send<StepThreePage, byte[]>(this, "Photos", imagetosave);
                    }
                    else
                    {
                        await DisplayAlert("Error", Translator.getText("Only3"), "OK");
                    }





                }
                else
                {
                    if (CalledFrom == Constants.ForUserPic)
                    {
                        ViewModels.id_document_type = 2;
                        idInfob.name = Translator.getText("UserPhoto");
                        MessagingCenter.Send<StepThreePage, byte[]>(this, "UserPhoto", imagetosave);

                    }
                    if (CalledFrom == Constants.ForProductPic)
                    {
                        ViewModels.id_document_type = 3;
                        idInfob.name = Translator.getText("ProductPhoto");
                        MessagingCenter.Send<StepThreePage, byte[]>(this, "ProductPhoto", imagetosave);
                    }


                    if (ViewModels.IdInfos.Count == 0)
                    {
                        ViewModels.addIdInfo(idInfob);
                    }
                    else
                    {
                        await DisplayAlert("Error", Translator.getText("OnlyOnePick"), "OK");
                    }

                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Potential Code Quality Issues", "RECS0165:Los métodos asincrónicos deben devolver una tarea en lugar de un valor nulo", Justification = "<pendiente>")]
        private async void ShowResult()
        {
            await DisplayAlert(Translator.getText("Success"), Translator.getText("UpdateDone"), "Ok");
            await Navigation.PopModalAsync();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
