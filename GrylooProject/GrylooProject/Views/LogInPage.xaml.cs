using GrylooProject.DependencyInterface;
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
    public partial class LogInPage : ContentPage
    {
        public static bool checkGust = false;
        string mobileNumber;
        public LogInPage()
        {
            InitializeComponent();

            checkGust = false;



            NavigationPage.SetHasNavigationBar(this, false);



            switch (Device.RuntimePlatform)
            {
                case Device.iOS:

                    gstBtn.HeightRequest = 50;
                    gstBtn.WidthRequest = 150;




                    NavigationPage.SetBackButtonTitle(this, "Atrás");
                    break;
                case Device.Android:
                    

                    break;

            }

            txtLogInMobile.Text = string.Empty;










        }

       

        private async void Login_Clicked(EventArgs e)
        {

           
            string msg = string.Empty;
            string mobileNumber = txtLogInMobile.Text;



            if (string.IsNullOrEmpty(mobileNumber))
            {
                msg = Resx.AppResources.enterMobileNumber + Environment.NewLine;
            }

            else
            {
                if (mobileNumber.Length < 9 || mobileNumber.Length > 9)
                {
                    msg += Resx.AppResources.yourMobileNumberMustbe + Environment.NewLine;
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
               
                VoteAlertPopup.textmsg = msg;
                await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                return;
            }

            bindLoginData();

        }


        async void bindLoginData()
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

                // fetching mobile from user input

                    mobileNumber = txtLogInMobile.Text;

                    string postData = "phone=" + mobileNumber + "";



                    var result = await CommonLib.LoginUser(CommonLib.ws_MainUrl + "Login?" + postData);
                    if (result != null && result.Status != 0)
                    {
                        LoadPopup.CloseAllPopup();
                       
                    //VoteAlertPopup.textmsg = Convert.ToString(result.Otp);
                    //await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    await Navigation.PushAsync(new VerificationsPage(mobileNumber));
                         

                }
                    else
                    {
                        LoadPopup.CloseAllPopup();
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


    
        private  void LoginGuestClicked(EventArgs e)
        {

            checkGust = true;
             Application.Current.MainPage.Navigation.PushAsync(new Views.BottomPageView());
        }


        async void LoginCreateAccount_Clicked(object sender, System.EventArgs e)
        {

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    await Navigation.PushAsync(new RegistrationPage(),true);

                    break;
                case Device.Android:
                    await Navigation.PushAsync(new RegistrationPage());

                    break;

            }


        }


        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
                DependencyService.Get<ICloseApplication>().closeApplication();

            return base.OnBackButtonPressed();
        }

        private void txtLogInMobile_Focused(object sender, FocusEventArgs e)
        {

        }


    }
}