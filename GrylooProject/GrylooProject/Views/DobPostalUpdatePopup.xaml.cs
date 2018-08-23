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
    public partial class DobPostalUpdatePopup : PopupPage
    {
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public string genderType;

        public DobPostalUpdatePopup()
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;

            FrameContainer.WidthRequest = (width / 2) + 65;

            LayoutContainer.WidthRequest = (width / 2) + 65;

            if (Device.OS == TargetPlatform.iOS)
            {

                submitButton.HeightRequest = 40;
                submitButton.WidthRequest = 90;

            }
            List<string> dobYear = new List<string>();
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                dobYear.Add(Convert.ToString(i));
            }
            DOBPicker1.ItemsSource = dobYear;

            DOBPicker1.SelectedIndexChanged += (sender, e) =>
            {
                txtCreateNwAccnDOB.Text = DOBPicker1.SelectedItem.ToString();
                txtCreateNwAccnDOB.Unfocus();
            };
        }
        private void txtLoginDOB_Focused(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                DOBPicker1.Focus();
            });
        }
        private void maleButton_Clicked(object sender, System.EventArgs e)
        {

            genderType = "1";
           
             
                maleButton.Source = "radioButton.png";

                
                femaleButton.Source = "radiobuttonnormal.png";



        }

        private void femaleButton_Clicked(object sender, System.EventArgs e)
        {

            genderType = "2";
            
                femaleButton.Source = "radioButton.png";

               
                maleButton.Source = "radiobuttonnormal.png";



        }
        private async void submitButton_Clicked(object sender, EventArgs e)
        {
            //Validation Part
            string msg = string.Empty;

           string  birthOfYear = txtCreateNwAccnDOB.Text;

            string postalCode = txtCreateNwAccnPostalCode.Text;

          

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
                            await App.Current.MainPage.DisplayAlert("", Resx.AppResources.checkInternet, "OK");
                            return;
                        }
                        await Navigation.PushPopupAsync(new LoadPopup());
                        string postData = "postalCode=" + postalCode;
                        var result = await CommonLib.GetProvince(CommonLib.ws_MainUrl + "GetProvinceName?" + postData);
                        if (result.Status == 1)
                        {
                            LoadPopup.CloseAllPopup3();
                        }
                        else
                        {
                            LoadPopup.CloseAllPopup3();
                            msg = Resx.AppResources.validPostalCode + Environment.NewLine;
                        }
                    }
                    catch (Exception ex)
                    {
                        LoadPopup.CloseAllPopup3();
                        await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                    }
                }

            }
            if (string.IsNullOrEmpty(birthOfYear))
            {
                msg = Resx.AppResources.selectBirthYear + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(genderType))
            {
                msg = Resx.AppResources.pleaseselectgender + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(msg))
            {
                await App.Current.MainPage.DisplayAlert("", msg, "OK");
                return;
            }
            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());
                var result = await CommonLib.ChangePostalCode(CommonLib.ws_MainUrl + "UpdateDobAndPostal?" + "Id=" + LoginDetails.userId + "&Code=" + postalCode + "&Dob=" + birthOfYear+ "&gender=" + genderType);
                if (result.Status != 0)
                {
                    await App.Current.MainPage.DisplayAlert("", Resx.AppResources.Sucess, "OK");
                    LoadPopup.CloseAllPopup();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }



        }
    }
}