using GrylooProject.Data;
using GrylooProject.DependencyInterface;
using GrylooProject.Model;
using GrylooProject.Repository;
using GrylooProject.Resx;
using GrylooProject.Views;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GrylooProject
{
    public partial class App : Application
    {

        public static double ScreenHeight;
        public static double ScreenWidth;
        public static string deviceToken;
        
        public App()
        {
            InitializeComponent();

            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(netLanguage);



            Current.Resources = new ResourceDictionary();
            Current.Resources.Add("UlycesColor", Color.FromHex("#007f4f"));

            var navigationStyle = new Style(typeof(NavigationPage));

            var barTextColorSetter = new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White };
            var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.FromHex("#007f4f") };

            navigationStyle.Setters.Add(barTextColorSetter);
            navigationStyle.Setters.Add(barBackgroundColorSetter);

            


           

              Current.Resources.Add(navigationStyle);

          MainPage = new NavigationPage( new IntializerPage());

         //  GetMainPage();

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

                    App.Current.MainPage.Navigation.PushAsync(new Views.BottomPageView());
                }
                else
                {
                    App.Current.MainPage.Navigation.PushAsync(new Views.LogInPage());
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {


            }
        }


        private static DBgrylloo database;
        public static DBgrylloo Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new DBgrylloo(DependencyService.Get<IFileHelper>().GetLocalFilePath("DBgrylloo.db3"));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return database;
            }
        }


        protected override void OnStart()
       {
            IntializerPage.checkLoad = false;
          CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                if (Device.RuntimePlatform == "Android")
                {
                  deviceToken = p.Token;
                }
                System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            };

            var aa = CrossFirebasePushNotification.Current.Token;
            if (Device.RuntimePlatform == "iOS")
            {
                deviceToken = aa;
            }
            System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Received");
                    if (p.Data.ContainsKey("body"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            // mPage.Message = $"{p.Data["body"]}";
                        });

                    }
                }
                catch (Exception ex)
                {

                }

            };

          

        }



    

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
