using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class AlertPopup : PopupPage
    {
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public AlertPopup()
        {
            InitializeComponent();
            if(Device.OS==TargetPlatform.Android){
              FrameContainer.HeightRequest = (height / 2) - 175;
              LayoutContainer.HeightRequest = (height / 2) - 175;
            }
            FrameContainer.WidthRequest = (width / 2) + 75;

          
            LayoutContainer.WidthRequest = (width / 2) + 75;
           
            if(Device.OS==TargetPlatform.iOS){
                YesButton.HeightRequest = 40;
                YesButton.WidthRequest = 80;
                NoButton.HeightRequest = 40;
                NoButton.WidthRequest = 80;

            }
        }
        private async void okButton_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.LogInPage());
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }
        public static async void CloseAllPopup()
        {

            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);

        }

        private void NoButton_Clicked(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
    }
}