using GrylooProject.DependencyInterface;
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
    public partial class PaymentAlertPopup : PopupPage
    {
        public static string price = "";
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;
        public PaymentAlertPopup()
        {
            InitializeComponent();
           FrameContainer.WidthRequest = (width / 2) + 65;

            LayoutContainer.WidthRequest = (width / 2) + 65;

            if (Device.OS == TargetPlatform.iOS)
            {
                YesButton.HeightRequest = 40;
                YesButton.WidthRequest = 80;
                NoButton.HeightRequest = 40;
                NoButton.WidthRequest = 80;

            }
            PackagePrice();
        }
        private async void okButton_Clicked(object sender, EventArgs e)
        {
            PackageDetails();

        }
        public static async void CloseAllPopup()
        {

            await App.Current.MainPage.Navigation.PopPopupAsync(false);

        }

        private void NoButton_Clicked(object sender, EventArgs e)
        {
            MyProfilePage.checkPremimun = false;
            CloseAllPopup();
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

                    try
                    {
                        string msg = string.Empty;
                        if (string.IsNullOrEmpty(txtcardNumber.Text))
                        {
                            msg += Resx.AppResources.entercard + Environment.NewLine;
                        }
                        else
                        {
                            if (txtcardNumber.Text.Length != 16)
                            {
                                msg += Resx.AppResources.entervalidcard + Environment.NewLine;
                            }
                        }
                        if (string.IsNullOrEmpty(txtCVVNumber.Text))
                        {
                            msg += Resx.AppResources.entercvv + Environment.NewLine;
                        }
                        else
                        {
                            if (txtCVVNumber.Text.Length != 3)
                            {
                                msg += Resx.AppResources.entervalidcvv + Environment.NewLine;
                            }
                        }
                        if (string.IsNullOrEmpty(txtMonth.Text))
                        {
                            msg += Resx.AppResources.entermonth + Environment.NewLine;
                        }
                        else
                        {
                            if (Convert.ToInt32(txtMonth.Text) > 12)
                            {
                                msg += Resx.AppResources.entervalidmonth + Environment.NewLine;
                            }
                        }
                        if (string.IsNullOrEmpty(txtYear.Text))
                        {
                            msg += Resx.AppResources.enteryear + Environment.NewLine;
                        }
                        else
                        {
                            int curYear = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(1));
                            if (Convert.ToInt32(txtYear.Text) < curYear)
                            {
                                msg += Resx.AppResources.entervalidyear + Environment.NewLine;
                            }
                        }
                        if (!string.IsNullOrEmpty(msg))
                        {
                            LoadPopup.CloseAllPopup3();
                            await App.Current.MainPage.DisplayAlert("", msg, "OK");
                            return;
                        }


                        StripeCardInfo cardInfo = new StripeCardInfo();
                        cardInfo.CardNumber = txtcardNumber.Text;
                        cardInfo.CCV = txtCVVNumber.Text;
                        cardInfo.Month = Convert.ToInt32(txtMonth.Text);
                        cardInfo.Year = Convert.ToInt32(txtYear.Text);


                        double Amount = Convert.ToDouble(price);


                        var paymentresult = await DependencyService.Get<ICustomStripeService>().MakePayment(cardInfo, Amount);
                      

                        if (!paymentresult.IsPaid)
                        {
                            LoadPopup.CloseAllPopup3();
                            MyProfilePage.checkPremimun = false;
                           
                            await App.Current.MainPage.DisplayAlert("", paymentresult.FailureMessage, "OK");




                            return;
                        }else
                        {
                            await App.Current.MainPage.DisplayAlert("", Resx.AppResources.PaymentSuccessful, "OK");


                            MyProfilePage.checkPremimun = true;
                            PremimumUser();
                        }
                    }
                    catch (Exception)
                    {
                    }


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
        public async void PremimumUser()
        {

            string aa = Model.LoginDetails.userId;
            if (aa != null)
            {
                try
                {
                   
                    string postData = "UserId=" + Model.LoginDetails.userId;

                    var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrlMain + "UserApi/MakePremimum?" + postData);

                }
                catch (Exception ex)
                {

                }

            }
        }

        public async void PackagePrice()
        {

            try
            {
               



                var result = await CommonLib.GetPackage(CommonLib.ws_MainUrlMain + "PackageApi/GetPackage");
                if (result != null && result.Status != 0)
                {
                    string time = result.details.PackageTime;
                    string[] words = time.Split(' ');
                    string timeresult = words[0].ToString();
                    priceTxt.Text = Resx.AppResources.pricePackage+": "+result.details.PackagePrice+" € "+Resx.AppResources.fora+" "+ timeresult + " "+Resx.AppResources.monthLicence;
                   
                }

                else
                {
                    
                }
            }
            catch (Exception ex)
            {
               
            }


        }

    }
}
