using GrylooProject.DependencyInterface;
using GrylooProject.Model;
using GrylooProject.Resx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using GrylooProject.Repository;
using Plugin.FirebasePushNotification;

namespace GrylooProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntializerPage : ContentPage
    {
        public static bool checkLoad =false;
        public IntializerPage()
        {
            InitializeComponent();


            if (Device.OS == TargetPlatform.iOS)
            {
                if (checkLoad == false)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(50);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            GetMainPage();
                        });

                    });
                    checkLoad = true;
                }
            }
            else
            {



                Task.Run(async () =>
                {
                    await Task.Delay(300);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        GetMainPage();
                    });

                });
            }

           
        }

        void GetMainPage()
        {
            
            L10n.SetLocale();

            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();

            AppResources.Culture = new CultureInfo(netLanguage);

            loggedInUser user = new loggedInUser();

            try
            {



                if (App.Database.GetLoginUser(out user))
                {
                    LoginDetails.userId = user.userId;
                    LoginDetails.mobile = user.mobile;
                    LoginDetails.sessionId = user.sessionId;

                    LoginDetails.userType = user.userType;
                    LoginDetails.lifeLine = user.lifeLine;

                    UpdateDeviceId();
                    UpdateProfile();


                    Application.Current.MainPage.Navigation.PushAsync(new Views.BottomPageView());
                    
                 



                }
                else
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {

                        App.Current.MainPage = new NavigationPage(new LogInPage());

                    }
                    else
                    {


                        Application.Current.MainPage.Navigation.PushAsync(new Views.LogInPage());


                    } 
                   
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {


            }

        }
        public async void UpdateDeviceId()
        {

            




            string aa = LoginDetails.userId;
            if (aa != null)
            {
                try
                {
                    string deviceType = "";
                    if (Device.OS == TargetPlatform.Android)
                    {
                        deviceType = "1";
                    }
                    else
                    {
                        deviceType = "2";
                    }
                    if (string.IsNullOrEmpty(App.deviceToken))
                    {
                        App.deviceToken = Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.Token;
                    }



                    string postData = "UserId=" + LoginDetails.userId + "&token=" + App.deviceToken + "&type=" + deviceType;

                    var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrlMain + "UserApi/UpdateToken?" + postData);

                }
                catch (Exception ex)
                {

                }

            }
        }

        public async void UpdateProfile()
        {
            string aa = LoginDetails.userId;
            if (aa != null)
            {
                try
                {
                    string postData = "Id=" + LoginDetails.userId;
                    var result = await CommonLib.UpdateAccountDetails(CommonLib.ws_MainUrlMain + "UserApi/UserAccount?" + postData);
                    if(result.Status==1){
                        Model.loggedInUser objUser = new Model.loggedInUser();
                        objUser.userId = Convert.ToString(result.profile.Id);
                        objUser.mobile = result.profile.Phone;
                        objUser.sessionId = LoginDetails.sessionId;
                        objUser.userType = result.profile.TypeOfUser;
                        objUser.lifeLine = result.profile.Lifeline;
                        App.Database.UpdateLoggedUser(objUser);


                        Model.LoginDetails.userType = result.profile.TypeOfUser;
                        Model.LoginDetails.lifeLine=result.profile.Lifeline;

                    }
                }
                catch (Exception ex)
                {
                }

            }
        }


    }
}