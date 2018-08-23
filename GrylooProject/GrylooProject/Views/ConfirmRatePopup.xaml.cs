using GrylooProject.Model;
using GrylooProject.Repository;
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
    public partial class ConfirmRatePopup : PopupPage
    {
        public static string leaderID = string.Empty;
        public static string starValue = string.Empty;
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public ConfirmRatePopup(string leaderId, string starvalue, string leaderName)
        {

            InitializeComponent();
            msgLbl.Text = Resx.AppResources.wouldYouLikeRate + " " + leaderName.ToUpper() + " ?";
            leaderID = leaderId;
            starValue = starvalue;

            FrameContainer.HeightRequest = (height / 2) - 180;
            FrameContainer.WidthRequest = (width / 2) + 65;

            LayoutContainer.HeightRequest = (height / 2) - 180;
            LayoutContainer.WidthRequest = (width / 2) + 65;
            if (Device.OS == TargetPlatform.iOS)
            {
                YesButton.HeightRequest = 40;
                YesButton.WidthRequest = 80;
                NoButton.HeightRequest = 40;
                NoButton.WidthRequest = 80;
            }
        }

        private void NoButton_Clicked(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
        public static async void CloseAllPopup()
        {

            await App.Current.MainPage.Navigation.PopPopupAsync(false);

        }
        private async void YesButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (!CommonLib.checkconnection())

                {
                    VoteAlertPopup.textmsg = Resx.AppResources.checkInternet;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                }


                await Navigation.PushPopupAsync(new LoadPopup());

                var result = await CommonLib.leaderRate(CommonLib.ws_MainUrlMain + "LeaderApi/GiveRate?" + "userId=" + LoginDetails.userId + "&leaderCode=" + leaderID + "&rate=" + starValue + "");
                if (result != null && result.Status != 0)
                {

                    if (LoginDetails.sessionId == result.SessionId)
                    {
                        LoadPopup.CloseAllPopup();


                        RateLeaderPopUpPage.isSaved = true;

                      



                        await Navigation.PopAllPopupAsync();

                        VoteAlertPopup.textmsg = Resx.AppResources.youRatedSucessfull;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                    }

                    else
                    {
                        RateLeaderPopUpPage.isSaved = false;
                        CloseAllPopup();
                        LoadPopup.CloseAllPopup();
                        VoteAlertPopup.textmsg = Resx.AppResources.yourSession;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                        App.Current.MainPage = new Views.LogInPage();
                    }


                }
                else
                {
                    RateLeaderPopUpPage.isSaved = false;
                    LoadPopup.CloseAllPopup();
                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }
            }

            catch (Exception ex)
            {
                RateLeaderPopUpPage.isSaved = false;
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }
        }
    }
}