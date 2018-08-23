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
    public partial class ChangePostalCodePopUp : PopupPage
    {
        string postalCodeOld;
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public ChangePostalCodePopUp(string PostalCode )
        {
            InitializeComponent();

            newPostalCodeEntry.Text = string.Empty;

            postalCodeOld = PostalCode;

            FrameContainer.HeightRequest = (height / 2)-100;
            FrameContainer.WidthRequest = (width / 2)+100;

            LayoutContainer.HeightRequest = (height / 2)-100;
            LayoutContainer.WidthRequest = (width / 2) + 100;

            BackButton.WidthRequest = (width / 2) -70;

            DoneButton.WidthRequest = (width / 2) - 70;
        }
        
        private async  void Done_Clicked(EventArgs e)
        {
            //Validation Part
            string msg = string.Empty;

           string  postalCode = newPostalCodeEntry.Text;
         

          

            if (string.IsNullOrEmpty(postalCode))
            {
                msg += Resx.AppResources.entervalidpostalcode + Environment.NewLine;
            }

            else
            {
                if (postalCode.Length < 5 || postalCode.Length > 5)
                {
                    msg += Resx.AppResources.yourPostalCodeMust + Environment.NewLine;
                }
            }

          
            if (!string.IsNullOrEmpty(msg))
            {
               // await App.Current.MainPage.DisplayAlert("", msg, "OK");
                VoteAlertPopup.textmsg = msg;
                await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                return;
            }

            bindChangePostalCodeData();
        }

        private void Back_Clicked(EventArgs e)
        {

            CloseAllPopup();
        }

        //get postal code data
        async void bindChangePostalCodeData()
        {

            try
            {

                if (!CommonLib.checkconnection())

                {
                    //await DisplayAlert("", "Check your internet connection.", "Okay");

                    VoteAlertPopup.textmsg = Resx.AppResources.checkInternet;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                    return;
                }


                await Navigation.PushPopupAsync(new LoadPopup());

                // fetching detail from uder input

                string newPostalCode = newPostalCodeEntry.Text;






                var result = await CommonLib.ChangePostalCode(CommonLib.ws_MainUrl + "UpdatePostalCode?" + "Id=" + LoginDetails.userId+ "&newCode="+ newPostalCode+"");
                if (result != null && result.Status != 0)
                {
                    if (LoginDetails.sessionId == result.SessionId)
                    {
                        
                        VoteAlertPopup.textmsg = Resx.AppResources.Postalcodeupdated;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }

                    else
                    {
                        LoadPopup.CloseAllPopup();
                       
                        VoteAlertPopup.textmsg = Resx.AppResources.yourSession;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                        App.Current.MainPage = new Views.LogInPage();

                    }
                    


                }
                else
                {
                    LoadPopup.CloseAllPopup();
                    //await App.Current.MainPage.DisplayAlert("", result.msg, "ok");
                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }
            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }
            finally
            {

            }

        }

        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(false);
        }
    }
}