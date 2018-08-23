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
    public partial class MyProfilePage : ContentPage
    {
        public static string price = "";
        public static bool checkPremimun = true;
        string postalCode;
        public static string exp = "";
        public MyProfilePage()
        {
            InitializeComponent();
            Title = Resx.AppResources.profile;

            Help.navigationCheck = false;



            bindMyProfileData();

            checkUserType();
        }

        //Binding data in view from api

        protected override void OnAppearing()
        {

            UpdateProfile();

            base.OnAppearing();


        }

        //refresh user type
        public async void UpdateProfile()
        {
            string aa = LoginDetails.userId;
            if (aa != null)
            {
                try
                {
                    string postData = "Id=" + LoginDetails.userId;
                    var result = await CommonLib.UpdateAccountDetails(CommonLib.ws_MainUrlMain + "UserApi/UserAccount?" + postData);
                    if (result.Status == 1)
                    {
                        Model.loggedInUser objUser = new Model.loggedInUser();
                        objUser.userId = Convert.ToString(result.profile.Id);
                        objUser.mobile = result.profile.Phone;
                        objUser.sessionId = LoginDetails.sessionId;
                        objUser.userType = result.profile.TypeOfUser;
                        objUser.lifeLine = result.profile.Lifeline;
                        App.Database.UpdateLoggedUser(objUser);


                        Model.LoginDetails.userType = result.profile.TypeOfUser;
                        Model.LoginDetails.lifeLine = result.profile.Lifeline;

                    }
                }
                catch (Exception ex)
                {
                }

            }
        }



        async void bindMyProfileData()
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





                var result = await CommonLib.EditMyProfile(CommonLib.ws_MainUrl + "MyProfile?" + "Id=" + LoginDetails.userId);
                if (result != null && result.Status != 0)
                {
                    if (LoginDetails.sessionId == result.SessionId)
                    {
                        LoadPopup.CloseAllPopup();

                        yearOfBirthText.Text = Convert.ToString(result.Myprofile.birthYear);

                        postalCodeText.Text = Convert.ToString(result.Myprofile.postalCode);

                        provinceText.Text = result.Myprofile.province;

                        postalCode = Convert.ToString(result.Myprofile.postalCode);
                    }
                    else
                    {
                        LoadPopup.CloseAllPopup();

                        VoteAlertPopup.textmsg = Resx.AppResources.yourSession;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                        await Application.Current.MainPage.Navigation.PushAsync(new Views.LogInPage());


                    }


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




        private async void changeButtonClicked(EventArgs e)
        {
            ChangePostalCodePopUp _popup = new Views.ChangePostalCodePopUp(postalCode);
            _popup.Disappearing += _popup_Disappearing;
            await App.Current.MainPage.Navigation.PushPopupAsync(_popup);

        }

        private void _popup_Disappearing(object sender, EventArgs e)
        {
            bindMyProfileData();
        }


        private async void UserType_Clicked(object sender, EventArgs e)
        {




            if (Device.RuntimePlatform == Device.iOS)
            {


                await Navigation.PushPopupAsync(new LoadPopup());

                var result = await CommonLib.IOSPaymentData(CommonLib.ws_MainUrl + "IOSPayment");
                if (result.Status == 1)
                {
                    LoadPopup.CloseAllPopup();
                    string textLbl  = Resx.AppResources.pricePackage + ": " + result.keyPrice + " € " + Resx.AppResources.fora + " " + result.keyTime + " " + Resx.AppResources.monthLicence;
                    var ans = await DisplayAlert("", textLbl, Resx.AppResources.yes, Resx.AppResources.cancel);
                    if (ans == true)
                    { 


                        var paymentresult = await DependencyService.Get<DependencyInterface.IPremiumMembership>().MakePayment(result.keyName);
                        if (paymentresult.IsPaid == true)
                        {
                            PremimumUser();
                        }



                    }
                }
                else
                {
                    LoadPopup.CloseAllPopup();
                }




            }

            else
            {
                PaymentAlertPopup popup = new PaymentAlertPopup();

                popup.Disappearing += Popup_Disappearing;

                await App.Current.MainPage.Navigation.PushPopupAsync(popup);
           }
        }

        //ios check
        public async void PremimumUser()
        {

            string aa = Model.LoginDetails.userId;
            if (aa != null)
            {
                try
                {

                    string postData = "UserId=" + Model.LoginDetails.userId;

                    var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrlMain + "UserApi/MakePremimum?" + postData);
                    checkPremimun = true;
                    PremimumUser1();
                }
                catch (Exception ex)
                {

                }

            }
        }
        public  void PremimumUser1()
        {
            if (checkPremimun == true)
            {
                IosUserType();
                ButtonLayout.IsVisible = false;
                PremimumLbl.Text = Resx.AppResources.alreadypremimum;
                expDateLbl.Text = exp;

                
                Model.LoginDetails.userType = 2;


                Model.loggedInUser objUser = new Model.loggedInUser();
                objUser.userId = Model.LoginDetails.userId;
                objUser.mobile = Model.LoginDetails.mobile;
                objUser.sessionId = Model.LoginDetails.sessionId;
                objUser.userType = 2;
                objUser.lifeLine = Model.LoginDetails.lifeLine;
                App.Database.UpdateLoggedUser(objUser);

            }
        }
        //


        private void Popup_Disappearing(object sender, EventArgs e)
        {
            if (checkPremimun == true)
            {
                ButtonLayout.IsVisible = false;
                PremimumLbl.Text = Resx.AppResources.alreadypremimum;
                expDateLbl.Text = exp;

                //
                Model.LoginDetails.userType = 2;


                Model.loggedInUser objUser = new Model.loggedInUser();
                objUser.userId = Model.LoginDetails.userId;
                objUser.mobile = Model.LoginDetails.mobile;
                objUser.sessionId = Model.LoginDetails.sessionId;
                objUser.userType = 2;
                objUser.lifeLine = Model.LoginDetails.lifeLine;
                App.Database.UpdateLoggedUser(objUser);
                IosUserType();
            }

        }



        async void checkUserType()
        {

            try
            {

                if (!CommonLib.checkconnection())

                {

                    VoteAlertPopup.textmsg = Resx.AppResources.checkInternet;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                }








                var result = await CommonLib.UserTypeResult(CommonLib.ws_MainUrlMain + "UserApi/GetUserType?" + "UserId=" + LoginDetails.userId);
                if (result != null)
                {

                    string aa = Model.LoginDetails.userId;

                    if (result.UserResult.TypeOfUser == 2 || result.UserResult.Lifeline == true)
                    {
                        ButtonLayout.IsVisible = false;
                        PremimumLbl.Text = Resx.AppResources.alreadypremimum;
                        if (result.UserResult.Lifeline == false)
                        {
                            expDateLbl.Text = result.expDate;
                            exp = result.expDate;
                        }
                        else
                        {
                            expDateLbl.Text = "";
                            exp = "";
                        }
                    }



                }
                else
                {

                }
            }
            catch (Exception ex)
            {


            }
            finally
            {

            }

        }
        public async void PackageDetails()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());



                var result = await CommonLib.GetPackage(CommonLib.ws_MainUrlMain + "PackageApi/GetPackage");
                if (result != null && result.Status != 0)
                {
                    price = result.details.PackagePrice;




                    LoadPopup.CloseAllPopup1();
                }

                else
                {
                    LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                LoadPopup.CloseAllPopup1();
            }


        }



        public async void IosUserType()
        {

            var result = await CommonLib.UserTypeResult(CommonLib.ws_MainUrlMain + "UserApi/GetUserType?" + "UserId=" + LoginDetails.userId);
            if (result != null)
            {

                string aa = Model.LoginDetails.userId;

                if (result.UserResult.TypeOfUser == 2 || result.UserResult.Lifeline == true)
                {
                    ButtonLayout.IsVisible = false;
                    PremimumLbl.Text = Resx.AppResources.alreadypremimum;
                    if (result.UserResult.Lifeline == false)
                    {
                        expDateLbl.Text = result.expDate;
                        exp = result.expDate;
                    }
                    else
                    {
                        expDateLbl.Text = "";
                        exp = "";
                    }
                }



            }
        }



    }
}
