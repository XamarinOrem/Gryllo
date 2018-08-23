using GrylooProject.CustomControls;
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
    public partial class RegistrationPage : ContentPage
    {
        string pic, pic2;


        public string mobileNumber;

        public string birthOfYear;

        public string postalCode;
        public string ProvinceName;

        public string prefixCode;

        public string wholeMobileNumber;

        public string genderType { get; set; }



        public RegistrationPage()
        {
            InitializeComponent();

            // fetching user input
            txtCreateNwAccnMobile.Text = string.Empty;


            txtCreateNwAccnDOB.Text = string.Empty;




            txtCreateNwAccnPostalCode.Text = string.Empty;


            txtCreateNwAccnMobile.ReturnKeyType = ReturnKeyTypes.Next;

            txtCreateNwAccnPostalCode.ReturnKeyType = ReturnKeyTypes.Done;


            txtCreateNwAccnMobile.Next = txtCreateNwAccnDOB;

            txtCreateNwAccnPostalCode.DoneCommand = new Command(() =>
            txtCreateNwAccnPostalCode.Unfocus());


            List<string> dobYear = new List<string>();
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                dobYear.Add(Convert.ToString(i));
            }
            DOBPicker1.ItemsSource = dobYear;




            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    
                    NavigationPage.SetHasNavigationBar(this, true);
                    NavigationPage.SetBackButtonTitle(this, "Atrás");

                    break;
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);

                    break;

            }

            DOBPicker1.SelectedIndexChanged += (sender, e) =>
            {
                txtCreateNwAccnDOB.Text = DOBPicker1.SelectedItem.ToString();
                txtCreateNwAccnDOB.Unfocus();
            };

            string pic0 = Convert.ToString(maleButton.BindingContext);
            string pic1 = Convert.ToString(femaleButton.BindingContext);

            pic = pic0;
            pic2 = pic1;

        }


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

                mobileNumber = txtCreateNwAccnMobile.Text;
                birthOfYear = txtCreateNwAccnDOB.Text;

                postalCode = txtCreateNwAccnPostalCode.Text;

                prefixCode = "34";


                await Navigation.PushPopupAsync(new LoadPopup());


                string postData = "phone=" + mobileNumber + "&PhonePrefix=" + prefixCode + "&birthYear=" + birthOfYear + "&gender=" + genderType + "&postalCode=" + postalCode + "";



                var result = await CommonLib.RegisterUser(CommonLib.ws_MainUrl + "Register?" + postData);
                if (result != null && result.Status != 0)
                {
                    LoadPopup.CloseAllPopup();
                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
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





        private async void NextClickedButton(EventArgs e)
        {
            //Validation Part
            string msg = string.Empty;

            mobileNumber = txtCreateNwAccnMobile.Text;
            birthOfYear = txtCreateNwAccnDOB.Text;

            postalCode = txtCreateNwAccnPostalCode.Text;


            wholeMobileNumber = txtCreateNwAccnMobile.Text;







            //for ios
            if (Device.OS == TargetPlatform.iOS && !string.IsNullOrEmpty(postalCode))
            {


                if (string.IsNullOrEmpty(postalCode))
                {
                    msg = Resx.AppResources.validPostalCode + Environment.NewLine;
                }

                else
                {
                    if (postalCode.Length < 5 || postalCode.Length > 5)
                    {
                        msg = Resx.AppResources.postalcodefivedigit + Environment.NewLine;
                    }
                    else
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
                            string postData = "postalCode=" + postalCode;
                            var result = await CommonLib.GetProvince(CommonLib.ws_MainUrl + "GetProvinceName?" + postData);
                            if (result.Status == 1)
                            {
                                LoadPopup.CloseAllPopup();
                                ProvinceName = result.ProvinceName;
                            }
                            else
                            {
                                LoadPopup.CloseAllPopup();
                                msg = Resx.AppResources.validPostalCode + Environment.NewLine;
                            }
                        }
                        catch (Exception ex)
                        {
                            LoadPopup.CloseAllPopup();
                            await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                        }
                    }
                }


            }




            //for ios
            if (Device.OS == TargetPlatform.Android)
            {


                if (string.IsNullOrEmpty(postalCode))
                {
                    msg = Resx.AppResources.validPostalCode + Environment.NewLine;
                }

                else
                {
                    if (postalCode.Length < 5 || postalCode.Length > 5)
                    {
                        msg = Resx.AppResources.postalcodefivedigit + Environment.NewLine;
                    }
                    else
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
                            string postData = "postalCode=" + postalCode;
                            var result = await CommonLib.GetProvince(CommonLib.ws_MainUrl + "GetProvinceName?" + postData);
                            if (result.Status == 1)
                            {
                                LoadPopup.CloseAllPopup();
                                ProvinceName = result.ProvinceName;
                            }
                            else
                            {
                                LoadPopup.CloseAllPopup();
                                msg = Resx.AppResources.validPostalCode + Environment.NewLine;
                            }
                        }
                        catch (Exception ex)
                        {
                            LoadPopup.CloseAllPopup();
                            await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                        }
                    }
                }


            }

            //for ios
            if (Device.OS == TargetPlatform.Android)
            {
                if (string.IsNullOrEmpty(genderType))
                {
                    msg = Resx.AppResources.pleaseselectgender + Environment.NewLine;
                }
            }

            //for ios
            if (Device.OS == TargetPlatform.Android)
            {
                if (string.IsNullOrEmpty(birthOfYear))
                {
                    msg = Resx.AppResources.selectBirthYear + Environment.NewLine;
                }
            }


            if (string.IsNullOrEmpty(mobileNumber))
            {
                msg = Resx.AppResources.enterMobile + Environment.NewLine;
            }

            else
            {
                if (mobileNumber.Length < 9 || mobileNumber.Length > 9)
                {
                    msg = Resx.AppResources.yourMobileNumberMustbe + Environment.NewLine;
                }
            }




            if (!string.IsNullOrEmpty(msg))
            {
             
                VoteAlertPopup.textmsg = msg;
                await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                return;
            }




            //Fetching Part
            mobileNumber = txtCreateNwAccnMobile.Text;
            birthOfYear = txtCreateNwAccnDOB.Text;

            postalCode = txtCreateNwAccnPostalCode.Text;




            await Navigation.PushAsync(new PreviewOfRegistrationPage(mobileNumber, birthOfYear, postalCode, wholeMobileNumber, genderType, prefixCode, ProvinceName), true);




        }

        private void txtLoginDOB_Focused(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                DOBPicker1.Focus();
            });
           
           
        }

        private async void LoginCreateAccount_Clicked(object sender, System.EventArgs e)
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


        private void maleButton_Clicked(object sender, System.EventArgs e)
        {

            genderType = "1";
            if (pic == "0")
            {
                pic = "1";
                maleButton.Source = "radioButton.png";

                pic2 = "0";
                femaleButton.Source = "radiobuttonnormal.png";

            }
            txtCreateNwAccnPostalCode.Unfocus();
            txtCreateNwAccnPostalCode.Focus();

        }

        private void femaleButton_Clicked(object sender, System.EventArgs e)
        {

            genderType = "2";
            if (pic2 == "0")
            {
                pic2 = "1";
                femaleButton.Source = "radioButton.png";

                pic = "0";
                maleButton.Source = "radiobuttonnormal.png";

            }
            txtCreateNwAccnPostalCode.Unfocus();
            txtCreateNwAccnPostalCode.Focus();

        }


    }
}