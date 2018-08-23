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
    public partial class VoteAlertPopup : PopupPage
    {
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public static string textmsg="";
        public VoteAlertPopup()
        {
            InitializeComponent();
            magText.Text = textmsg;
           

            if(Device.OS==TargetPlatform.iOS){
                YesButton.HeightRequest = 40;
                YesButton.WidthRequest = 80;

                FrameContainer.WidthRequest = (width / 2) + 70;

                LayoutContainer.WidthRequest = (width / 2) + 70;

            }else
            {
                //FrameContainer.HeightRequest = (height / 2) - 200;
                FrameContainer.WidthRequest = (width / 2) + 50;

               // LayoutContainer.HeightRequest = (height / 2) - 200;
                LayoutContainer.WidthRequest = (width / 2) + 50;
            }
        }
        private async void okButton_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }
        public static async void CloseAllPopup()
        {

            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);

        }

       
    }
}