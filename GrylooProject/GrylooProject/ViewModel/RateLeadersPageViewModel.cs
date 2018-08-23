using GrylooProject.Model;
using GrylooProject.Repository;
using GrylooProject.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace GrylooProject.ViewModel
{
    public class RateLeadersPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int totalcount = 0;
        public bool IsFirstHit = false;
        public int getLeaderCount = 0;
        public int pageindex = 1;
        public int pageSize = 5;
        public bool HitinProcess = false;

        public string labelStatus, starButtonStatus;

        

        public InfiniteScrollCollection<LeaderListModel> Items { get; set; }

        public bool _isLoadingMore;
        public bool IsLoadingMore
        {
            get
            {
                return _isLoadingMore;
            }
            set
            {
                _isLoadingMore = value;
                OnPropertyChanged(nameof(IsLoadingMore));
            }
        }


        bool _LabelVisible = false;
        public bool LabelVisible
        {
            get
            {
                return _LabelVisible;
            }
            set
            {
                _LabelVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LabelVisible"));
            }
        }


        bool _RateButtonVisible = false;
        public bool RateButtonVisible
        {
            get
            {
                return _RateButtonVisible;
            }
            set
            {
                _RateButtonVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RateButtonVisible"));
            }
        }


        bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsBusy"));
            }
        }

        



        public RateLeadersPageViewModel()
        {
            

            Items = new InfiniteScrollCollection<LeaderListModel>
            {
                OnLoadMore = async () =>
                {
                    var items = new InfiniteScrollCollection<LeaderListModel>();
                    if (totalcount > getLeaderCount && getLeaderCount !=0 || IsFirstHit == false)
                    {

                        if (!HitinProcess)
                        {
                            HitinProcess = true;
                            IsLoadingMore = true;
                            var response = await CommonLib.LeaderList(CommonLib.ws_MainUrlMain + "LeaderApi/Leaders?" + "userId=" +
                                LoginDetails.userId + "&pageIndex=" + pageindex + "&pageSize=" + pageSize);

                            if (response.Status == 1)
                            {
                               
                                try
                                {
                                    if (LoginDetails.sessionId == response.SessionId)
                                    {
                                        RateLeadersPage.checkStatus = response.SessionId;

                                        pageindex++;
                                        IsFirstHit = true;
                                        totalcount = response.Count;
                                        getLeaderCount = response.Leaders.Count;
                                        items = GetItems(true, response.Leaders);
                                        IsLoadingMore = false;
                                        HitinProcess = false;
                                    }
                                    else
                                    {

                                        // await App.Current.MainPage.DisplayAlert("", "Your session is expired!", "ok");
                                        // App.Current.MainPage = new app();
                                        // App.Current.MainPage = new NavigationPage(new Views.LogInPage());

                                        await Application.Current.MainPage.Navigation.PushAsync(new Views.LogInPage());


                                        // App.Current.MainPage = new Views.LogInPage();

                                      //await Task.Run(async () =>
                                      //  {
                                      //      await Task.Delay(300);
                                      //      Device.BeginInvokeOnMainThread(() =>
                                      //      {
                                      //          Application.Current.MainPage = new NavigationPage(new Views.LogInPage());
                                      //      });
                                      //  });
                                    }
                                }

                                catch (Exception ex)
                                {

                                    
                                }
                               
                            }
                            else
                            {
                                HitinProcess = false;
                                IsLoadingMore = false;

                            }
                        }
                    }
                    //Call your Web API next items page.
                    //if (!ProductCategories.IsPull)
                    //    await Task.Delay(1200);

                    return items;
                }
            };
            Items.LoadMoreAsync();
        }

        void OnLoad()
        {


        }


        // LeaderDetailBindData
        /// <summary>
        /// This for integrating data in view model from API
        /// </summary>
        /// <returns></returns>

        InfiniteScrollCollection<LeaderListModel> GetItems(bool clearList, List<Leader> Leaders)
        {
            InfiniteScrollCollection<LeaderListModel> items;
            if (clearList || Items == null)
            {
                items = new InfiniteScrollCollection<LeaderListModel>();
            }
            else
            {
                items = new InfiniteScrollCollection<LeaderListModel>(Items);
            }

            

            foreach (var leaderItem in Leaders)
            {



                items.Add(new LeaderListModel
                {
                    leaderID = Convert.ToString(leaderItem.LeaderCode),

                    leaderImg = leaderItem.Image,

                    leaderName = leaderItem.LeaderName,


                    labelStatus = leaderItem.LabelStatus == "1" ? false : true,

                    starButtonStatus = leaderItem.RateButtonStatus == "1" ? false : true,

                    rateImg = leaderItem.RateButtonStatus == "1" ? "Ratebutton.png" : "rate_button_green.png",


                    fontSize = Device.OS == TargetPlatform.iOS ? "11" : "11",

                });



            }

            return items;
        }







        //Rate Button Binding
        /// <summary>
        /// This is for Rate Button
        /// </summary>
        public Command btnRateCommond
        {
            get
            {
                return new Command(async (e) =>
                {
                    var data = (LeaderListModel)e;


                    string imgRate = data.rateImg.ToString();
                    row_index = Items.ToList().FindIndex(x => x.leaderID == data.leaderID);


                

                    if (data.starButtonStatus == false || imgRate == "File: Ratebutton.png")
                    {
                        //await App.Current.MainPage.DisplayAlert("", "You have already rate today", "OK");
                        VoteAlertPopup.textmsg = Resx.AppResources.alreadyRate;//"You have already rate today!";
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                    else
                    {
                    RateLeaderPopUpPage _popup = new Views.RateLeaderPopUpPage(data);
                    _popup.Disappearing += _popup_Disappearing;
                    await App.Current.MainPage.Navigation.PushPopupAsync(_popup);
                    }

                });
            }
        }



        int row_index;
        private  void _popup_Disappearing(object sender, EventArgs e)
        {

            if (Device.RuntimePlatform == "iOS")
            {
                if (RateLeaderPopUpPage.isSaved)
                {
                    Items[row_index].rateImg = "Ratebutton.png";
                    Items[row_index].labelStatus = false;
                }
                else
                {
                    Items[row_index].rateImg = "rate_button_green.png";
                }
            }
            else
            {
                if (RateLeaderPopUpPage.isSaved)
                {
                    Items = new InfiniteScrollCollection<LeaderListModel>();
                    RaisePropertyChanged(nameof(Items)); // raise a property change in whatever way is right for your VM
                    Items.CollectionChanged += CollectionDidChange;

                    HitinProcess = false;
                    pageindex = 1;
                    getLeaderCount = 0;
                    totalcount = 0;
                    IsFirstHit = false;
                    Items = new InfiniteScrollCollection<LeaderListModel>
                    {
                        OnLoadMore = async () =>
                        {
                            var items = new InfiniteScrollCollection<LeaderListModel>();
                            if (totalcount > getLeaderCount && getLeaderCount != 0 || IsFirstHit == false)
                            {
                                if (!HitinProcess)
                                {
                                    HitinProcess = true;
                                    IsLoadingMore = true;
                                    var response = await CommonLib.LeaderList(CommonLib.ws_MainUrlMain + "LeaderApi/Leaders?" + "userId=" +
                                        LoginDetails.userId + "&pageIndex=" + pageindex + "&pageSize=" + pageSize);

                                    if (response.Status == 1)
                                    {

                                        pageindex++;
                                        IsFirstHit = true;
                                        totalcount = response.Count;
                                        getLeaderCount = response.Leaders.Count;
                                        items = GetItems(true, response.Leaders);
                                        IsLoadingMore = false;
                                        HitinProcess = false;


                                    }
                                    else
                                    {
                                        HitinProcess = false;
                                        IsLoadingMore = false;

                                    }
                                }
                            }
                        //Call your Web API next items page.
                        //if (!ProductCategories.IsPull)
                        //    await Task.Delay(1200);

                        return items;
                        }
                    };


                    Items.LoadMoreAsync();
                    //Items = new InfiniteScrollCollection<LeaderListModel>();
                    RaisePropertyChanged(nameof(Items)); // raise a property change in whatever way is right for your VM
                    Items.CollectionChanged += CollectionDidChange;
                }
            }
        }


        public void CollectionDidChange(object sender,NotifyCollectionChangedEventArgs e)
        {

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
