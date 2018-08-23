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
    public partial class Help : ContentPage
    {
        public static bool navigationCheck = true;
        public Help()
        {
            InitializeComponent();
            Title = Resx.AppResources.help;
            navigationCheck=false;
            bindhelpData();
            if (Device.OS == TargetPlatform.Android)
            {
                BackgroundColor = Color.White;

            }
        }
        //get help text
        async void bindhelpData()
        {

            try
            {

                if (!CommonLib.checkconnection())

                {
                    VoteAlertPopup.textmsg = "Check your internet connection.";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new VoteAlertPopup());
                    return;
                }


                await Navigation.PushPopupAsync(new LoadPopup());




                var result = await CommonLib.HelpResult(CommonLib.ws_MainUrlMain + "ContactApi/HelpText");

                if (result != null && result.Status != 0)
                {
                    
                    LoadPopup.CloseAllPopup();

                    helpText.Text = result.Note.HelpText;
                    

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
       
    }
}