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
using Plugin.Media.Abstractions;
using WSViews.Classes;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.RegisterA
{
    public partial class ImagePage : ContentPage
    {
        public StepThreeViewModel ViewModels { get; }
        private string CalledFrom { get; set; }
        private IdInfo idInfo { get; set; }
        public string coments { get; set; }
        string path { get; set; }
        ImageSource _imageSource;
        private IMedia _mediaPicker;
        Image image;
        string Server, Site;
        Byte[] imagetosave;

        public ImagePage(string calledfrom)
        {
            InitializeComponent();
            CalledFrom = calledfrom;
            BindingContext = ViewModels = new StepThreeViewModel(CalledFrom);
            ViewModels.NoRegisterCall += () => showResult();



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
               
                await TakePicturemethod();


            };

            SelectPicture.Clicked += async (sender, args) =>
            {
                await SelectPicturemethod();

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

                            await Navigation.PopModalAsync();

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            
                 MessagingCenter.Subscribe<StepThreeViewModel, string>(this, "prodpicfinished", (obj, item) => {


                     Navigation.PopModalAsync();


                 });
        }

        private async void showResult()
        {
            await DisplayAlert(Translator.getText("Success"), Translator.getText("UpdateDone"), "Ok");
            await Navigation.PopModalAsync();
        }

        async void  Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Refresh()
        {
            try
            {
                if (App.CroppedImage != null)
                {
                    Stream stream = new MemoryStream(App.CroppedImage);

                    imagetosave = new MemoryStream(App.CroppedImage).ToArray();
                    Imagecropped.Source = ImageSource.FromStream(() => stream);

                    idInfo = new IdInfo
                    {

                        comments = coments,
                        Image = _imageSource,
                        ImageByte = imagetosave,
                    };



                    createImage(path, idInfo);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

       

        private async void Setup()
        {
            if (_mediaPicker != null)
            {
                return;
            }

            ////RM: hack for working on windows phone? 
            await CrossMedia.Current.Initialize();
            _mediaPicker = CrossMedia.Current;
        }

        private async Task SelectPicturemethod()
        {
            Setup();

            _imageSource = null;
            byte[] imageAsByte;
            try
            {
                using (UserDialogs.Instance.Loading(Translator.getText("AdjustingImage"), null, null, true, MaskType.Black))
                {
                    var mediaFile = await this._mediaPicker.PickPhotoAsync();
               
               
                    _imageSource = ImageSource.FromStream(mediaFile.GetStream);

                var memoryStream = new MemoryStream();
                await mediaFile.GetStream().CopyToAsync(memoryStream);
                imageAsByte = memoryStream.ToArray();


                idInfo = new IdInfo
                {

                    comments = coments,
                    Image = _imageSource,
                    ImageByte = ImageManager.ConvertToBytes(mediaFile.Path),
                };



                path = mediaFile.Path;
                   }   
                await Navigation.PushModalAsync(new CropView(imageAsByte, Refresh));

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async  Task TakePicturemethod()
        {
            Setup();

            _imageSource = null;
            byte[] imageAsByte;
            try
            {
                using (UserDialogs.Instance.Loading(Translator.getText("AdjustingImage"), null, null, true, MaskType.Black))
                {
                    var mediaFile = await this._mediaPicker.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    DefaultCamera = CameraDevice.Front
                });


                _imageSource = ImageSource.FromStream(mediaFile.GetStream);

                var memoryStream = new MemoryStream();
                await mediaFile.GetStream().CopyToAsync(memoryStream);
                imageAsByte = memoryStream.ToArray();

                idInfo = new IdInfo
                {

                    comments = coments,
                    Image = _imageSource,
                    ImageByte = ImageManager.ConvertToBytes(mediaFile.Path),
                };

                path = mediaFile.Path;
                    }
                await Navigation.PushModalAsync(new CropView(imageAsByte, Refresh));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void createImage(string path, IdInfo idInfob)
        {
            using (UserDialogs.Instance.Loading(Translator.getText("Loading"), null, null, true, MaskType.Black))
            {

                 imagetosave = idInfob.ImageByte;

                if (CalledFrom == Constants.ForIdPics)
                {
                    if (ViewModels.IdInfos.Count < 2)
                    {
                        MessagingCenter.Send<ImagePage, byte[]>(this, "Photos", imagetosave);
                        ViewModels.id_document_type = 1;
                        ViewModels.addIdInfo(idInfob);

                       
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
                        MessagingCenter.Send<ImagePage, byte[]>(this, "UserPhoto", imagetosave);
                        ViewModels.id_document_type = 2;
                        idInfob.name = Translator.getText("UserPhoto");


                    }
                    if (CalledFrom == Constants.ForProductPic)
                    {
                        MessagingCenter.Send<ImagePage, byte[]>(this, "ProductPhoto", imagetosave);
                        ViewModels.id_document_type = 3;
                        idInfob.name = Translator.getText("ProductPhoto");
                       

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

    }
}
