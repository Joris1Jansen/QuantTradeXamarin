using System;
using Firebase.Storage;
using QuantTrade.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuantTrade
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userId = Auth.GetCurrentUserId();
            
            try {
                var image = await new FirebaseStorage("quanttrade-d7eed.appspot.com",
                        new FirebaseStorageOptions
                        {
                            ThrowOnCancel = true
                        })
                    .Child(userId).GetDownloadUrlAsync();
                
                QTLogo.Source = ImageSource.FromUri(new Uri(image));
            }
            catch (Exception Ex) {
                var assembly = typeof(MainPage);
                QTLogo.Source = ImageSource.FromResource("QuantTrade.Assets.Images.NoImage.jpg", assembly);
            }
        }

        async void TakeImage_ButtonClicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                UploadImage(result);
                var stream = await result.OpenReadAsync();

                QTLogo.Source = ImageSource.FromStream(() => stream);
            }
        }
        
        async void PickImage_ButtonClicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                UploadImage(result);
                var stream = await result.OpenReadAsync();

                QTLogo.Source = ImageSource.FromStream(() => stream);
            }
        }

        async void UploadImage(FileResult image)
        {
            var userId = Auth.GetCurrentUserId();
            
            await new FirebaseStorage("quanttrade-d7eed.appspot.com",
                    new FirebaseStorageOptions
                    {
                        ThrowOnCancel = true
                    })
                .Child(userId)
                .PutAsync(await image.OpenReadAsync());
        }
    }
}