using GrylooProject.Model;
using GrylooProject.Repository;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrylooProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            Title = Resx.AppResources.configure;
           
        }
        protected async override void OnAppearing()
        {
            if (LoginDetails.userId != null)
            {
                iosCheck();
            }
        }
            private async void MyProfileTapped(object sender, EventArgs e)
        {
            var id = LoginDetails.userId;
            if (id == null && LogInPage.checkGust == true)
            {
             await   Navigation.PushPopupAsync(new AlertPopup());
            }
            else
            {
                await Navigation.PushAsync(new MyProfilePage());
            }
        }

        private async void ContacTapped(object sender, EventArgs e)
        {
            
            Device.OpenUri(new Uri(String.Format("mailto:{0}", "info@grylloo.com")));
        }

        private async void RateAppTapped(object sender, EventArgs e)
        {

            var urlStore = Device.OnPlatform("https://itunes.apple.com/us/app/grylloo/id1394520245?ls=1&mt=8", "https://play.google.com/store/apps/details?id=com.Grylloo", "");
            Device.OpenUri(new Uri(urlStore));

        }

        
        private async void LegalNotesTapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new LegalNotesPage());

        }

        private async void ShareAppTapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ShareApp());

        }
        
        private async void HeplpAppTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Help());
        }


        public async void iosCheck()
        {
            //ios check
            try
            {

                if (Device.OS == TargetPlatform.iOS)
                {
                    var result = await CommonLib.GetpostalCodeDob(CommonLib.ws_MainUrl + "GetDobAndPostal?" + "Id=" + LoginDetails.userId);
                    if (result.dob == 0 || string.IsNullOrEmpty(result.code))
                    {
                        DobPostalUpdatePopup popup = new DobPostalUpdatePopup();
                        await App.Current.MainPage.Navigation.PushPopupAsync(popup);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}