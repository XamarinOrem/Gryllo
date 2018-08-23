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
    public partial class PreviewOfRegistrationPage : ContentPage
    {

        string MobileNumber, PostalCode, YearOfBirth, Gender, PrefixCode, ProvinceName;


        public PreviewOfRegistrationPage(string mobileNumber, string birthOfYear, string postalCode, string wholeMobileNumber, string genderType, string prefixCode, string provinceName)
        {
            InitializeComponent();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    NavigationPage.SetHasNavigationBar(this, true);

                  

                    ConfirmRegistrationBtn.WidthRequest = 160;
                    break;

                case Device.Android:

                    NavigationPage.SetHasNavigationBar(this, false);

                    break;

            }


            PrefixCode = "34";
            MobileNumber = mobileNumber;
            YearOfBirth = birthOfYear;
            PostalCode = postalCode;
            ProvinceName = provinceName;
            Gender = genderType;

            //bindind data in view
            txtCreateNwAccnMobile.Placeholder = mobileNumber;
            txtCreateNwAccnDOB.Placeholder = birthOfYear;
            txtCreateNwAccnPostalCode.Placeholder = postalCode;
            txtCreateNwAccnProvinceName.Placeholder = provinceName;
            if (genderType == "1")
            {
                maleButton.Source = "radioButton.png";
                femaleButton.Source = "radiobuttonnormal.png";
            }


            if (genderType == "2")
            {
                femaleButton.Source = "radioButton.png";
                maleButton.Source = "radiobuttonnormal.png";
            }
            
        }


        //save user data
        async void BindData()
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

                if (Device.OS == TargetPlatform.iOS)
                {
                    if (string.IsNullOrEmpty(YearOfBirth)) { YearOfBirth = "0"; }
                }

                    string postData = "phone=" + MobileNumber + "&PhonePrefix=" + PrefixCode + "&birthYear=" + YearOfBirth + "&gender=" + Gender + "&postalCode=" + PostalCode + "";



                var result = await CommonLib.RegisterUser(CommonLib.ws_MainUrl + "Register?" + postData);
                if (result != null && result.Status != 0)
                {
                    LoadPopup.CloseAllPopup();
                    //VoteAlertPopup.textmsg = result.Otp;
                   // await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());




                    await Navigation.PushAsync(new VerificationsPage(MobileNumber));

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

        private void Submit_Clicked(EventArgs e)
        {

            BindData();
        }

        private async void EditProfile_Clicked(object sender, System.EventArgs e)
        {


            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    await Navigation.PopAsync();

                    break;
                case Device.Android:
                    await Navigation.PopAsync();

                    break;

            }

        }



    }
}