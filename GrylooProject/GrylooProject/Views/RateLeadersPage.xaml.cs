using GrylooProject.Model;
using GrylooProject.Repository;
using GrylooProject.ViewModel;
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
    public partial class RateLeadersPage : ContentPage
    {

        public static bool isBindAgain = false;
        public static bool IsPull = false;

        public static string checkStatus = string.Empty;
      
        public RateLeadersPage()
        {
            InitializeComponent();

            Title = Resx.AppResources.rateLeader;

            binddata();
            

        }


        protected async override void OnAppearing()
        {
          
            base.OnAppearing();
            if (checkStatus == "" && LogInPage.checkGust == true)
            {
                await Navigation.PushPopupAsync(new AlertPopup());
                
            }
            if (LoginDetails.userId != null)
            {
                iosCheck();
            }
        }
        public async void iosCheck()
        {
            //ios check
            try
            {

                if (Device.OS == TargetPlatform.iOS)
                {
                    var result = await CommonLib.GetpostalCodeDob(CommonLib.ws_MainUrl + "GetDobAndPostal?" + "Id=" + LoginDetails.userId);
                    if (result.dob == 0 || string.IsNullOrEmpty(result.code))
                    {
                        DobPostalUpdatePopup popup = new DobPostalUpdatePopup();
                        await App.Current.MainPage.Navigation.PushPopupAsync(popup);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        //bind leader list
        void binddata()
        {
            IsPull = true;
            RateLeadersPageViewModel model = new RateLeadersPageViewModel();
            BindingContext = model;
            IsPull = false;
        }


        private void list_refreshing(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android) { 
            binddata();

            myList.EndRefresh();
            }else{
                myList.EndRefresh();

            }
        }


        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selecteditem = myList.SelectedItem as LeaderListModel;
            myList.SelectedItem = null;
          
        }

       
    }
}