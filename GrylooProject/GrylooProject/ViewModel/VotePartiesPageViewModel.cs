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
    public class VotePartiesPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int totalcount = 0;
        public bool IsFirstHit = false;
        public int getLeaderCount = 0;
        public int pageindex = 1;
        public int pageSize = 5;
        public bool HitinProcess = false;
        public bool checkonemonth = false;

        public InfiniteScrollCollection<PartyListModel> Items { get; set; }


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

        public VotePartiesPageViewModel()
        {
            Items = new InfiniteScrollCollection<PartyListModel>
            {
                OnLoadMore = async () =>
                {
                    var items = new InfiniteScrollCollection<PartyListModel>();
                    if (totalcount > getLeaderCount && getLeaderCount != 0 || IsFirstHit == false)
                    {

                        if (!HitinProcess)
                        {
                            HitinProcess = true;
                            IsLoadingMore = true;
                            var response = await CommonLib.PartyList(CommonLib.ws_MainUrlMain + "CandidatureApi/Candidatures?" + "userId=" +
                                LoginDetails.userId + "&pageIndex=" + pageindex + "&pageSize=" + pageSize);

                          
                            if (response.Status == 1)
                            {
                                VotePartiesPage.checkLabel = response.LabelStatus;
                                VotePartiesPage.checkonemonth = false;
                               


                                if (LoginDetails.sessionId == response.SessionId)
                                {
                                    pageindex++;
                                    IsFirstHit = true;
                                    totalcount = response.Count;
                                    getLeaderCount = response.Candidatures.Count;
                                    items = GetItems(true, response.Candidatures, response.VoteButtonStatus, response.LabelStatus);
                                    IsLoadingMore = false;
                                    HitinProcess = false;
                                }
                                else
                                {

                                    //  await App.Current.MainPage.DisplayAlert("", "Your session is expired!", "ok");
                                   await Application.Current.MainPage.Navigation.PushAsync( new Views.LogInPage());
                                    // App.Current.MainPage = new NavigationPage(new Views.LogInPage());

                                    //await Task.Run(async () =>
                                    //{
                                    //    await Task.Delay(300);
                                    //    Device.BeginInvokeOnMainThread(() =>
                                    //    {
                                    //        Application.Current.MainPage = new NavigationPage(new Views.LogInPage());
                                    //    });
                                    //});
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


        // PartyDetailBindData
        /// <summary>
        /// This for integrating data in view model from API
        /// </summary>
        /// <returns></returns>

        InfiniteScrollCollection<PartyListModel> GetItems(bool clearList, List<Candidature> Party,string voteButtonStatus, string VotepopUpStatus)
        {
            InfiniteScrollCollection<PartyListModel> items;
            if (clearList || Items == null)
            {
                items = new InfiniteScrollCollection<PartyListModel>();
            }
            else
            {
                items = new InfiniteScrollCollection<PartyListModel>(Items);
            }
           
            foreach (var partyItem in Party)
            {
                items.Add(new PartyListModel
                {
                    partyID = Convert.ToString(partyItem.CandidatureCode),

                    partyImg = partyItem.Image,

                    partyName = partyItem.CandidatureName,

                    partySortName = partyItem.CandidatureSortName,

                    voteButtonVisiblity = voteButtonStatus=="1" ? false : true,

                    voteImag= voteButtonStatus == "1" ? "votepartiesNew1.png" : "vote_button_green1.png"







                });
            }

            return items;
        }



        int row_index;

        //Vote image Button Binding
        /// <summary>
        /// This is for vote party
        /// </summary>
        public Command VotePartCommond
        {

            get
            {
                return new Command(async (e) =>
                {
                    var data = (PartyListModel)e;

                    string imgVote = data.voteImag.ToString();

                    row_index = Items.ToList().FindIndex(x => x.partyID == data.partyID);
                    if (data.voteButtonVisiblity == false || imgVote=="File: votepartiesNew1.png")
                    {
                        // await App.Current.MainPage.DisplayAlert("", "You have already vote today", "OK");
                        VoteAlertPopup.textmsg = Resx.AppResources.alreadyVote;//"You have already vote today!";
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    }
                    else
                    {

                        string partyCode = data.partyID;
                        ConfirmVotePopup popup = new ConfirmVotePopup(data.partyID, data.partyName);
                        popup.Disappearing += Popup_Disappearing;
                        await App.Current.MainPage.Navigation.PushPopupAsync(popup);
                    }



                    //var ans = await App.Current.MainPage.DisplayAlert("", "Would you like to vote this candidature?", "Yes", "No");
                    //if (ans == true)
                    //{
                    //    try
                    //    {

                    //        if (!CommonLib.checkconnection())

                    //        {
                    //            await App.Current.MainPage.DisplayAlert("", "Check your internet connection.", "Okay");
                    //            return;
                    //        }


                    //        await App.Current.MainPage.Navigation.PushPopupAsync(new LoadPopup());




                    //        var response = await CommonLib.PartyVote(CommonLib.ws_MainUrlMain + "CandidatureApi/Vote?" + "userId=" + LoginDetails.userId + "&CandidatureCode=" + partyCode + "");


                    //        if (response.Status == 1)
                    //        {

                    //            LoadPopup.CloseAllPopup();
                    //            await App.Current.MainPage.DisplayAlert("", "You voted successfully!", "ok");
                    //            _popup_Disappearing();

                    //        }

                    //        else
                    //        {
                    //            LoadPopup.CloseAllPopup();
                    //            await App.Current.MainPage.DisplayAlert("", response.msg, "ok");

                    //        }
                    //    }

                    //    catch (Exception ex)
                    //    {


                    //    }
                    //}




                });
            }
        }

        private void Popup_Disappearing(object sender, EventArgs e)
        {

            if (Device.RuntimePlatform == "iOS")
            {
                if (ConfirmVotePopup.isSave)
                {
                    

                    for (int i = 0; i < Items.Count; i++)
                    {
                        Items[i].voteImag = "votepartiesNew1.png";
                    }
                }
                else
                {
                    Items[row_index].voteImag = "vote_button_green1.png";
                }
            }
            else
            {
                if (ConfirmVotePopup.isSave)
                {
                    Items = new InfiniteScrollCollection<PartyListModel>();
                    RaisePropertyChanged(nameof(Items)); // raise a property change in whatever way is right for your VM
                    Items.CollectionChanged += CollectionDidChange;

                    HitinProcess = false;
                    pageindex = 1;
                    getLeaderCount = 0;
                    totalcount = 0;
                    IsFirstHit = false;

                    Items = new InfiniteScrollCollection<PartyListModel>
                    {
                        OnLoadMore = async () =>
                        {
                            var items = new InfiniteScrollCollection<PartyListModel>();
                            if (totalcount > getLeaderCount && getLeaderCount != 0 || IsFirstHit == false)
                            {

                                if (!HitinProcess)
                                {
                                    HitinProcess = true;
                                    IsLoadingMore = true;
                                    var response = await CommonLib.PartyList(CommonLib.ws_MainUrlMain + "CandidatureApi/Candidatures?" + "userId=" +
                                        LoginDetails.userId + "&pageIndex=" + pageindex + "&pageSize=" + pageSize);

                                    if (response.Status == 1)
                                    {

                                        if (LoginDetails.sessionId == response.SessionId)
                                        {
                                            pageindex++;
                                            IsFirstHit = true;
                                            totalcount = response.Count;
                                            getLeaderCount = response.Candidatures.Count;
                                            items = GetItems(true, response.Candidatures, response.VoteButtonStatus, response.LabelStatus);
                                            IsLoadingMore = false;
                                            HitinProcess = false;
                                        }
                                        else
                                        {
                                            LoadPopup.CloseAllPopup();
                                        //await App.Current.MainPage.DisplayAlert("", "Your session is expired!", "ok");
                                        VoteAlertPopup.textmsg = "Your session is expired!";
                                            await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                                            App.Current.MainPage = new Views.LogInPage();
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
                    //Items = new InfiniteScrollCollection<LeaderListModel>();
                    RaisePropertyChanged(nameof(Items)); // raise a property change in whatever way is right for your VM
                    Items.CollectionChanged += CollectionDidChange;
                }
            }
        }


        public void CollectionDidChange(object sender, NotifyCollectionChangedEventArgs e)
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
