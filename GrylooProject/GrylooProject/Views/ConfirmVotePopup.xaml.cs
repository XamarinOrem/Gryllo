using GrylooProject.Model;
using GrylooProject.Repository;
using GrylooProject.ViewModel;
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
    public partial class ConfirmVotePopup : PopupPage
    {
        public static bool isSave = false;
        public static string partyCode = "";
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public ConfirmVotePopup(string code,string name)
        {
            InitializeComponent();
            msgLbl.Text = Resx.AppResources.wouldYouLikeVote + " " + name.ToUpper() + " ?";
            partyCode = code;

            FrameContainer.HeightRequest = (height / 2) - 180;
            FrameContainer.WidthRequest = (width / 2) + 65;

            LayoutContainer.HeightRequest = (height / 2) - 180;
            LayoutContainer.WidthRequest = (width / 2) + 65;

            if(Device.OS==TargetPlatform.iOS){
                YesButton.HeightRequest = 40;
                YesButton.WidthRequest = 80;
                NoButton.HeightRequest = 40;
                NoButton.WidthRequest = 80;
            }

        }
        private void NoButton_Clicked(object sender, EventArgs e)
        {
            isSave = false;
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


                await App.Current.MainPage.Navigation.PushPopupAsync(new LoadPopup());




                var response = await CommonLib.PartyVote(CommonLib.ws_MainUrlMain + "CandidatureApi/Vote?" + "userId=" + LoginDetails.userId + "&CandidatureCode=" + partyCode + "");


                if (response.Status == 1)
                {
                    

               
                    isSave = true;

                    CloseAllPopup();
                    VoteAlertPopup.textmsg = Resx.AppResources.youVoted;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                }

                else
                {
                    isSave = false;
                    LoadPopup.CloseAllPopup();
                    VoteAlertPopup.textmsg = response.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }
            }

            catch (Exception ex)
            {
                isSave = false;
            }
        }
    }
}