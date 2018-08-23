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
    public partial class VotePartiesPage : ContentPage
    {

        public static bool isBindAgain = false;
        public static bool IsPull = false;
        public static bool checkonemonth = false;

        public static string checkLabel = string.Empty;
        public VotePartiesPage()
        {
            InitializeComponent();
            Title = Resx.AppResources.VoteCandidature;

            binddata();

            checkonemonth = false;
            
        }

        protected  override void OnAppearing()
        {

            base.OnAppearing();
            if (checkLabel == "" && LogInPage.checkGust == true)
            {
                 Navigation.PushPopupAsync(new AlertPopup());
            }

            if (checkLabel == "0" && checkonemonth == false)
            {
                App.Current.MainPage.DisplayAlert("", Resx.AppResources.youhavenotvote, Resx.AppResources.ok);
                checkonemonth = true;
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
        //bind data by view model
        public  void binddata()
        {
            IsPull = true;
            BindingContext = new VotePartiesPageViewModel();
            IsPull = false;
        }


        private void list_refreshing(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Android)
            {
                binddata();

                myList.EndRefresh();
            }else
            {
                myList.EndRefresh();
            }
        }


        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selecteditem = myList.SelectedItem as PartyListModel;
            myList.SelectedItem = null;

        }

    }
}