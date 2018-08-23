using GrylooProject.Model;
using GrylooProject.Repository;
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
    public partial class ContactUs : ContentPage
    {
        public ContactUs()
        {
            InitializeComponent();
            Title = Resx.AppResources.contactUs;
            InitializeControls();
        }

        void InitializeControls()
        {

            
            txtDesc.Text = "";
            txtDesc.FontAttributes = FontAttributes.Bold;
            txtDesc.FontSize = 14;
            txtDesc.TextColor = Color.Gray;
        }



      


        private async void SendButtonClicked(EventArgs e)
        {
            string msg = string.Empty;
            string textEditor = txtDesc.Text;



            if (string.IsNullOrEmpty(textEditor) || txtDesc.Text == "")
            {
                msg = Resx.AppResources.entercomment + Environment.NewLine;
            }

           

            if (!string.IsNullOrEmpty(msg))
            {
                VoteAlertPopup.textmsg =msg;
                await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                return;
            }


            bindContactUsData();

        }

        async void bindContactUsData()
        {

            try
            {

                if (!CommonLib.checkconnection())

                {
                    VoteAlertPopup.textmsg = Resx.AppResources.checkInternet;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                }


                await Navigation.PushPopupAsync(new LoadPopup());

                // fetching mobile from user input

                string txtMessage = txtDesc.Text;






                string postData = "userId=" + LoginDetails.userId +"&query="+txtMessage+"";



                var result = await CommonLib.ContactUs(CommonLib.ws_MainUrlMain + "ContactApi/ContactUs?" + postData);
                if (result != null && result.Status != 0)
                {
                    if (LoginDetails.sessionId == result.SessionId)
                    {
                        LoadPopup.CloseAllPopup();


                        VoteAlertPopup.textmsg = Resx.AppResources.Sucess;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                        switch (Device.RuntimePlatform)
                        {
                            case Device.iOS:
                                await Navigation.PopAsync();

                                break;
                            case Device.Android:
                                await Navigation.PopAsync();

                                break;

                        }

                    }

                    else

                    {

                        LoadPopup.CloseAllPopup();
                        VoteAlertPopup.textmsg = Resx.AppResources.yourSession;
                        await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());

                        await Application.Current.MainPage.Navigation.PushAsync(new Views.LogInPage());
                         
                    }

                }

                else
                {
                    LoadPopup.CloseAllPopup();
                    VoteAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                }

            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }
            finally
            {

            }

        }
        private void NotesEditor_Focused(object sender, FocusEventArgs e) //triggered when the user taps on the Editor to interact with it
        {
            if (txtDesc.Text.Equals("")) //if you have the placeholder showing, erase it and set the text color to black
            {
                txtDesc.FontSize = 14;
                txtDesc.FontAttributes = FontAttributes.Bold;
                txtDesc.TextColor = Color.Gray;
                txtDesc.Text = "";
                txtDesc.TextColor = Color.Black;
            }
        }

        private void NotesEditor_Unfocused(object sender, FocusEventArgs e) //triggered when the user taps "Done" or outside of the Editor to finish the editing
        {
            if (txtDesc.Text.Equals("")) //if there is text there, leave it, if the user erased everything, put the placeholder Text back and set the TextColor to gray
            {

                txtDesc.FontSize = 14;
                txtDesc.FontAttributes = FontAttributes.Bold;
                txtDesc.TextColor = Color.Black;
                txtDesc.Text = "";
                txtDesc.TextColor = Color.Gray;
            }
        }


    }
}