using GrylooProject.CustomControls;
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
    public partial class VerificationsPage : ContentPage
    {

        /// <summary>
        /// Below part is for variable declaration
        /// </summary>
        string MobileNumber,FirstNumber,SecondNumber,ThirdNumber,FourthNumber;

        

        /// <summary>
        /// Below is constructor part of this page
        /// </summary>
        /// <param name="mobileNumber"></param>
        public VerificationsPage(string mobileNumber )
            {
                InitializeComponent();


                NavigationPage.SetHasNavigationBar(this, false);

                MobileNumber = mobileNumber;

                txtFirstNumber.Text = string.Empty;
                txtSecondNumber.Text = string.Empty;
                txtThirdNumber.Text = string.Empty;
                txtFourthNumber.Text = string.Empty;



                txtFirstNumber.ReturnKeyType = ReturnKeyTypes.Next;
                txtSecondNumber.ReturnKeyType = ReturnKeyTypes.Next;
                txtThirdNumber.ReturnKeyType = ReturnKeyTypes.Next;
                txtFourthNumber.ReturnKeyType = ReturnKeyTypes.Done;


                txtFirstNumber.Next = txtSecondNumber;
                txtSecondNumber.Next = txtThirdNumber;
                txtThirdNumber.Next = txtFourthNumber;
                txtFourthNumber.DoneCommand = new Command(() =>
                 txtFourthNumber.Unfocus());

            }


        /// <summary>
        /// Bellow code is for verfication 
        /// </summary>
        /// <param name="e"></param>
      private async void verficationConfirmedClicked(EventArgs e)
      {
            
            //Validation Part
            string msg = string.Empty;

            FirstNumber = txtFirstNumber.Text;
            SecondNumber = txtSecondNumber.Text;

            ThirdNumber = txtThirdNumber.Text;

            FourthNumber = txtFourthNumber.Text;

            

            if (string.IsNullOrEmpty(FirstNumber))
            {
                msg = "Enter your first digit" + Environment.NewLine;
            }



            if (string.IsNullOrEmpty(SecondNumber))
            {
                msg += "Enter your second digit" + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(ThirdNumber))
            {
                msg += "Enter your third digit" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(FourthNumber))
            {
                msg += "Enter your fourth digit" + Environment.NewLine;
            }


            if (!string.IsNullOrEmpty(msg))
            {
                VoteAlertPopup.textmsg = msg;
                await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                return;
            }


            bindData();

        }

        public async void UpdateDeviceId()
        {

            string aa = LoginDetails.userId;
            if (aa != null)
            {
                try
                {
                    string deviceType = "";
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        deviceType = "2";
                    }
                    else
                    {
                        deviceType = "1";
                    }
                    string postData = "UserId=" + LoginDetails.userId + "&token=" + App.deviceToken + "&type=" + deviceType;

                    var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrlMain + "UserApi/UpdateToken?" + postData);

                }
                catch (Exception ex)
                {

                }

            }
        }
        private async void bindData()
        {
            try
                {
                    if (!CommonLib.checkconnection())
                    {
                    VoteAlertPopup.textmsg = "Check your internet connection.";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                    }

                // Fetching all input digit

                     FirstNumber = txtFirstNumber.Text;
                     SecondNumber = txtSecondNumber.Text;
                     ThirdNumber = txtThirdNumber.Text;
                     FourthNumber= txtFourthNumber.Text;

                    string otp = FirstNumber + SecondNumber + ThirdNumber + FourthNumber;

                    await Navigation.PushPopupAsync(new LoadPopup());


                    string postData = "phone=" + MobileNumber + "&otp=" +otp + "";



                    var result = await CommonLib.VerificationCode(CommonLib.ws_MainUrl + "VerifyOtp?" + postData);
                    if (result != null && result.Status != 0)
                    {
                        LoadPopup.CloseAllPopup();

                    
                    // Save data in sqlite for login check
                        Model.loggedInUser objUser = new Model.loggedInUser();
                        objUser.userId = Convert.ToString( result.User.id);
                        objUser.mobile = result.User.phone;
                        objUser.sessionId = result.SessionId;
                        objUser.userType = result.User.typeOfUser;
                        objUser.lifeLine = result.User.Lifeline;
                         App.Database.SaveLoggedUser(objUser);


                    // Get the data in login detail model,For further purpose

                        LoginDetails.userId = Convert.ToString(result.User.id);
                        LoginDetails.mobile = result.User.phone;
                        LoginDetails.sessionId = result.SessionId;
                    LoginDetails.userType = result.User.typeOfUser;
                    LoginDetails.lifeLine = result.User.Lifeline;
                    UpdateDeviceId();


                     await Application.Current.MainPage.Navigation.PushAsync(new Views.BottomPageView());
                    

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


        /// <summary>
        /// Below code is for resend OTP
        /// </summary>
        /// <param name="e"></param>
        private void Resend_Tapped(EventArgs e)
        {
            txtFirstNumber.Text = string.Empty;
            txtSecondNumber.Text = string.Empty;
            txtThirdNumber.Text = string.Empty;
            txtFourthNumber.Text = string.Empty;
            bindResendData();

        }
        
        async void bindResendData()
        {
             try
                {
                    if (!CommonLib.checkconnection())
                    {
                    VoteAlertPopup.textmsg = "Check your internet connection.";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                    }


                    await Navigation.PushPopupAsync(new LoadPopup());


                    string postData = "phone=" + MobileNumber + "";



                    var result = await CommonLib.ResendVerificationCode(CommonLib.ws_MainUrl + "ResendOtp?" + postData);
                    if (result != null && result.Status != 0)
                    {
                        LoadPopup.CloseAllPopup();

                   // VoteAlertPopup.textmsg = Convert.ToString(result.Otp);
                   // await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

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



        //Auto cursor implementation

        private void click_otp1(object sender, EventArgs e)
        {
            string _text = txtFirstNumber.Text;
            if (txtFirstNumber.Text.Length >= 1)
            {
                if (txtFirstNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFirstNumber.Text = _text;
                }
                txtSecondNumber.Focus();

            }
            else
            {
                txtFirstNumber.Focus();
            }
        }
        private void click_otp2(object sender, EventArgs e)
        {
            string _text = txtSecondNumber.Text;
            if (txtSecondNumber.Text.Length >= 1)
            {
                if (txtSecondNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtSecondNumber.Text = _text;
                }
                txtThirdNumber.Focus();
            }
            else
            {
                txtSecondNumber.Focus();
            }
        }
        private void click_otp3(object sender, EventArgs e)
        {
            string _text = txtThirdNumber.Text;
            if (txtThirdNumber.Text.Length >= 1)
            {
                if (txtThirdNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtThirdNumber.Text = _text;
                }
                txtFourthNumber.Focus();

            }
            else
            {
                txtFourthNumber.Focus();
            }
        }
        private void click_otp4(object sender, EventArgs e)
        {
            string _text = txtFourthNumber.Text;
            if (txtFourthNumber.Text.Length >= 1)
            {
                if (txtFourthNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFourthNumber.Text = _text;

                }


            }
            else
            {
                txtFourthNumber.Focus();
            }



        }



    }
  
}