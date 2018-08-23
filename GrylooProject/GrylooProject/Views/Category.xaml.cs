using GrylooProject.Model;
using GrylooProject.Repository;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GrylooProject.Views
{
    public partial class Category : ContentPage
    {
   
        public Category()
        {

            //if (LoginDetails.userId == null)
            //{
            //    Navigation.PushPopupAsync(new AlertPopup());

            //}
            //else
            //{
            //    LoadCategory();
            //}

        }
        protected async override void OnAppearing()
        {
            UpdateProfile();
            Help.navigationCheck = true;

            if (LoginDetails.userId==null)
            {
                 Navigation.PushPopupAsync(new AlertPopup());

            }
            else{

                 LoadCategory();

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

        //Get all category
        public async void LoadCategory()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());



                var result = await CommonLib.GetCategory(CommonLib.ws_MainUrlMain + "CategoryApi/GetAllCategory");
                if (result != null && result.Status != 0)
                {


                    InitializeComponent();
                    Title = Resx.AppResources.category;
                    myList.ItemsSource = result.CategoryData;

                    LoadPopup.CloseAllPopup1();
                }

                else
                {

                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    LoadPopup.CloseAllPopup1();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                LoadPopup.CloseAllPopup1();
            }


            iosCheck();

        }
        //listview click
        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
               
                var selecteditem = myList.SelectedItem as CategoryData;
                ChartsNamePage.categoryId = selecteditem.chartId;
                myList.SelectedItem = null;
               
                await Navigation.PushAsync(new ChartsNamePage());
            }
            catch (Exception)
            {
            }


        }
    }
}
