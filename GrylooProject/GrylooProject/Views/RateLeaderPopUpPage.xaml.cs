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
    public partial class RateLeaderPopUpPage : PopupPage
    {
        public static bool isSaved = false;
        string leaderID;
        string leaderName="";

        string starValue = string.Empty;

        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public RateLeaderPopUpPage(LeaderListModel leaderDetails )
        {
            InitializeComponent();

            leaderID = leaderDetails.leaderID;
            leaderName = leaderDetails.leaderName;
            CloseWhenBackgroundIsClicked = true;

             
            

            rateSlider.Maximum = 10;
            rateSlider.Minimum = 0;
            rateSlider.SetBinding(Slider.ValueProperty, "CurrentZoom");

            rateSlider.Value = 5;

            rateSlider.ValueChanged += RateSlider_ValueChanged; ;

            FrameContainer.HeightRequest = (height / 2)-150;
            FrameContainer.WidthRequest = (width / 2) + 100;

            LayoutContainer.HeightRequest = (height / 2)-150;
            LayoutContainer.WidthRequest = (width / 2) + 100;

            BackButton.WidthRequest = (width / 2) - 70;

            DoneButton.WidthRequest = (width / 2) - 70;

            
        }

        private void RateSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var yy = Math.Round(e.NewValue);
            ratingVal.Text = yy.ToString();
            
        }

        

        private async void RateSubmitClicked(EventArgs e)
        {
            starValue = ratingVal.Text;
            await Navigation.PushPopupAsync(new ConfirmRatePopup(leaderID, starValue, leaderName));
        }

       // Binding data for popUp rating
        async void bindRatingLeader()
        {
            
          
            var ans = await DisplayAlert("", Resx.AppResources.wouldYouLikeRate+" " + leaderName, Resx.AppResources.yes, Resx.AppResources.no);

            if (ans == true)
            {
                try
                {

                    if (!CommonLib.checkconnection())

                    {
                        await DisplayAlert("", Resx.AppResources.checkInternet, Resx.AppResources.ok);
                        return;
                    }


                    await Navigation.PushPopupAsync(new LoadPopup());



                    string starValue = ratingVal.Text;




                    var result = await CommonLib.leaderRate(CommonLib.ws_MainUrlMain + "LeaderApi/GiveRate?" + "userId=" + LoginDetails.userId + "&leaderCode=" + leaderID + "&rate=" + starValue + "");
                    if (result != null && result.Status != 0)
                    {

                        if (LoginDetails.sessionId == result.SessionId)
                        {
                            LoadPopup.CloseAllPopup();

                            isSaved = true;

                            CloseAllPopup();
                            VoteAlertPopup.textmsg = Resx.AppResources.youRatedSucessfull;
                            await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                        }

                        else
                        {
                            isSaved = false;
                            CloseAllPopup();
                            LoadPopup.CloseAllPopup();
                            VoteAlertPopup.textmsg = Resx.AppResources.yourSession;
                            await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                            App.Current.MainPage = new Views.LogInPage();
                        }


                    }
                    else
                    {
                        isSaved = false;
                        LoadPopup.CloseAllPopup();
                        VoteAlertPopup.textmsg = result.msg;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                    }
                }

                catch (Exception ex)
                {
                    isSaved = false;
                    LoadPopup.CloseAllPopup();
                    await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

                }
                finally
                {

                }
            }

        }


        
        private void BackClicked(EventArgs e)

        {
            isSaved = false ;
            CloseAllPopup();


        }
        public static async void CloseAllPopup()
        {
            
            await App.Current.MainPage.Navigation.PopPopupAsync(false);

        }
    }
}