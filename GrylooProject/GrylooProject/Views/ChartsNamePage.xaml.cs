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
    public partial class ChartsNamePage : ContentPage
    {
        public static string categoryId = "";
        public static string selectedItem = "";

        public ChartsNamePage()
        {
            InitializeComponent();
           
        }
        protected override void OnAppearing()
        {
            UpdateProfile();
            Help.navigationCheck = false;
            LoadCharts();
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

        //all charts list
        public async void LoadCharts()
        {

            try
            {
                await Navigation.PushPopupAsync(new LoadPopup());

                string postData = "categoryId=" + categoryId;

                var result = await CommonLib.GetChartName(CommonLib.ws_MainUrlMain + "CategoryApi/GetAllChartName?" + postData);
                if (result != null && result.Status != 0)
                {


                    InitializeComponent();
                    Title = Resx.AppResources.category;
                    List<ResultData1> ResultData = new List<ResultData1>();
                    foreach (var item in result.resultData)
                    {
                        string imgUrl = "";
                        if (item.ChartId == 1)
                        {
                            imgUrl = "Votes";
                        }
                        if (item.ChartId == 2 || item.ChartId == 10 ||item.ChartId == 11)
                        {
                            imgUrl = "Percentage.png";
                        }
                        if (item.ChartId == 4)
                        {
                            imgUrl = "Voteevolution.png";
                        }
                        if (item.ChartId == 5)
                        {
                            imgUrl = "VotersProfile.png";
                        }
                        if (item.ChartId == 6)
                        {
                            imgUrl = "RegionProvincesMap.png";
                        }
                        if (item.ChartId == 3)
                        {
                            imgUrl = "Votes.png";
                        }
                        if (item.ChartId == 7||item.ChartId == 12||item.ChartId == 13)
                        {
                            imgUrl = "Voteevolution.png";
                        }
                        if (item.ChartId == 8)
                        {
                            imgUrl = "VotersProfile.png";
                        }
                        if (item.ChartId == 9)
                        {
                            imgUrl = "RegionProvincesMap.png";
                        }
                        ResultData.Add(new ResultData1 { img = imgUrl, CategoryId = item.CategoryId, ChartId = item.ChartId, ChartName = item.ChartName, Id = item.Id, NGust = item.NGust, NPremium = item.NPremium, NRegister = item.NRegister, PGust = item.PGust, PPremium = item.PPremium, PRegister = item.PRegister, RGust = item.RGust, RPremium = item.RPremium, RRegister = item.RRegister });
                    }

                    myList.ItemsSource = ResultData;

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

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selecteditem = myList.SelectedItem as ResultData1;


                //chart 1 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 1)
                {
                    if (selecteditem.PGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.RGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.NGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                   
                   
                    else
                    {

                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }

                //chart 1 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine==false) && selecteditem.ChartId == 1)
                {
                   
                    if (selecteditem.PRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }

                    else if (selecteditem.RRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.NRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }

                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 1 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 1)
                {
                     if (selecteditem.PPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.RPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }

                    else if (selecteditem.NPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }

                //chart 2 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 2)
                {
                   

                    if (selecteditem.PGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.RGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.NGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {

                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 2 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 2)
                {
                    if (selecteditem.PRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }

                    else if (selecteditem.RRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.NRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 2 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 2)
                {
                    if (selecteditem.PPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.RPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }

                    else if (selecteditem.NPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }

                //chart 3 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 3)
                {
                    if (selecteditem.PGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.RGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.NGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 3 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 3)
                {
                    if (selecteditem.PRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }

                    else if (selecteditem.RRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.NRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 3 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 3)
                {
                    if (selecteditem.PPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "P";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else if (selecteditem.RPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "R";
                        await Navigation.PushAsync(new ChartsPage());
                    }

                    else if (selecteditem.NPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }

                //chart 4 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 4)
                {
                    //if (selecteditem.PGust == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "P";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                    //else if (selecteditem.RGust == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "R";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                    if (selecteditem.NGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 4 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 4)
                {
                    //if (selecteditem.PRegister == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "P";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}

                    //else if (selecteditem.RRegister == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "R";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                     if (selecteditem.NRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 4 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 4)
                {
                    //if (selecteditem.PPremium == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "P";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                    //else if (selecteditem.RPremium == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "R";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}

                     if (selecteditem.NPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }

                //chart 7 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 7)
                {
                    //if (selecteditem.PGust == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "P";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                    //else if (selecteditem.RGust == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "R";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                     if (selecteditem.NGust == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 7 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 7)
                {
                    //if (selecteditem.PRegister == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "P";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}

                    //else if (selecteditem.RRegister == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "R";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                     if (selecteditem.NRegister == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 7 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 7)
                {
                    //if (selecteditem.PPremium == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "P";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}
                    //else if (selecteditem.RPremium == true)
                    //{
                    //    myList.SelectedItem = null;
                    //    ChartsPage.ChartNameId = selecteditem.ChartId;
                    //    ChartsPage.ChartName = selecteditem.ChartName;
                    //    ChartsPage.ChartsType = "R";
                    //    await Navigation.PushAsync(new ChartsPage());
                    //}

                     if (selecteditem.NPremium == true)
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        ChartsPage.ChartsType = "N";
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //
                //chart 5 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 5)
                {
                    if (LoginDetails.userId == null && selecteditem.ChartId == 5 && (selecteditem.NGust == true || selecteditem.RGust || selecteditem.PGust))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 5 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 5)
                {
                    if (LoginDetails.userType == 1 && selecteditem.ChartId == 5 && (selecteditem.NRegister == true || selecteditem.RRegister || selecteditem.PRegister))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 5 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 5)
                {
                    if (LoginDetails.userType == 2 && selecteditem.ChartId == 5 && (selecteditem.NPremium == true || selecteditem.RPremium || selecteditem.PPremium))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }

                //chart 6 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 6)
                {
                    selectedItem = "";
                   
                    if (selecteditem.RGust == true)
                    { selectedItem = "R"; }
                    if (selecteditem.PGust == true)
                    { selectedItem = "P"; }
                  

                    if (selectedItem != "")
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                
                //chart 6 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 6 )
                {
                    selectedItem = "";
                  
                    if (selecteditem.RRegister == true)
                    { selectedItem = "R"; }
                    if (selecteditem.PRegister == true)
                    { selectedItem = "P"; }
                   

                    if (selectedItem != "")
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                
                //chart 6 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 6)
                {
                    selectedItem = "";
                   
                    if (selecteditem.RPremium == true)
                    { selectedItem = "R"; }
                    if (selecteditem.PPremium == true)
                    { selectedItem = "P"; }
                   

                    if (selectedItem != "")
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }

                //chart 8 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 8)
                {
                    if (LoginDetails.userId == null && selecteditem.ChartId == 8 && (selecteditem.NGust == true ||selecteditem.RGust == true || selecteditem.PGust == true))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 8 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 8)
                {
                    if (LoginDetails.userType == 1 && selecteditem.ChartId == 8 && (selecteditem.NRegister == true || selecteditem.RRegister == true || selecteditem.RPremium == true))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 8 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 8)
                {
                    if (LoginDetails.userType == 2 && selecteditem.ChartId == 8 && (selecteditem.PGust == true || selecteditem.PRegister == true || selecteditem.PPremium == true))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }

                //chart 9 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 9 )
                {
                    selectedItem = "";
                   
                    if (selecteditem.RGust == true)
                    { selectedItem = "R"; }
                    if (selecteditem.PGust == true)
                    { selectedItem = "P"; }
                  

                    if (selectedItem != "")
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                
                //chart 9 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 9)
                {
                    selectedItem = "";
                   
                    if (selecteditem.RRegister == true)
                    { selectedItem = "R"; }
                    if (selecteditem.PRegister == true)
                    { selectedItem = "P"; }
                  

                    if (selectedItem != "")
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                
                
                //chart 9 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 9)
                {
                    selectedItem = "";
                   
                    if (selecteditem.RPremium == true)
                    { selectedItem = "R"; }
                    if (selecteditem.PPremium == true)
                    { selectedItem = "P"; }
                  

                    if (selectedItem != "")
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                



                //chart 10 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 10)
                {
                    if (LoginDetails.userId == null && selecteditem.ChartId == 10 && (selecteditem.NGust == true || selecteditem.RGust || selecteditem.PGust))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 10 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 10)
                {
                    if (LoginDetails.userType == 1 && selecteditem.ChartId == 10 && (selecteditem.NRegister == true || selecteditem.RRegister || selecteditem.PRegister))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 10 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 10)
                {
                    if (LoginDetails.userType == 2 && selecteditem.ChartId == 10 && (selecteditem.NPremium == true || selecteditem.RPremium || selecteditem.PPremium))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }


                //chart 11 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 11)
                {
                    if (LoginDetails.userId == null && selecteditem.ChartId == 11 && (selecteditem.NGust == true || selecteditem.RGust || selecteditem.PGust))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 11 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 11)
                {
                    if (LoginDetails.userType == 1 && selecteditem.ChartId == 11 && (selecteditem.NRegister == true || selecteditem.RRegister || selecteditem.PRegister))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 11 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 11)
                {
                    if (LoginDetails.userType == 2 && selecteditem.ChartId == 11 && (selecteditem.NPremium == true || selecteditem.RPremium || selecteditem.PPremium))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }



                //chart 12 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 12)
                {
                    if (LoginDetails.userId == null && selecteditem.ChartId == 12 && (selecteditem.NGust == true || selecteditem.RGust || selecteditem.PGust))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 12 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 12)
                {
                    if (LoginDetails.userType == 1 && selecteditem.ChartId == 12 && (selecteditem.NRegister == true || selecteditem.RRegister || selecteditem.PRegister))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 12 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 12)
                {
                    if (LoginDetails.userType == 2 && selecteditem.ChartId == 12 && (selecteditem.NPremium == true || selecteditem.RPremium || selecteditem.PPremium))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }


                //chart 13 gust
                if (LoginDetails.userId == null && selecteditem.ChartId == 13)
                {
                    if (LoginDetails.userId == null && selecteditem.ChartId == 13 && (selecteditem.NGust == true || selecteditem.RGust || selecteditem.PGust))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new AlertPopup());
                    }
                }
                //chart 13 register
                if ((LoginDetails.userType == 1 && LoginDetails.lifeLine == false) && selecteditem.ChartId == 13)
                {
                    if (LoginDetails.userType == 1 && selecteditem.ChartId == 13 && (selecteditem.NRegister == true || selecteditem.RRegister || selecteditem.PRegister))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Pleasebecomepremimumuser;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }
                //chart 12 premimum
                if ((LoginDetails.userType == 2 || LoginDetails.lifeLine==true) && selecteditem.ChartId == 13)
                {
                    if (LoginDetails.userType == 2 && selecteditem.ChartId == 13 && (selecteditem.NPremium == true || selecteditem.RPremium || selecteditem.PPremium))
                    {
                        myList.SelectedItem = null;
                        ChartsPage.ChartNameId = selecteditem.ChartId;
                        ChartsPage.ChartName = selecteditem.ChartName;
                        await Navigation.PushAsync(new ChartsPage());
                    }
                    else
                    {
                        VoteAlertPopup.textmsg = Resx.AppResources.Thischartisnotvisible;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                }




            }
            catch (Exception)
            {
            }


        }
    }
}
