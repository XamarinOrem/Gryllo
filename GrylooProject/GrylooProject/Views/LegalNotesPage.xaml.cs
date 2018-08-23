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
    public partial class LegalNotesPage : ContentPage

    {
        public static string aa = "";
        public LegalNotesPage() :base()
        {
            InitializeComponent();
            Title = Resx.AppResources.legal;

            bindLegalNoteData();
            if(Device.OS==TargetPlatform.Android){
                BackgroundColor = Color.White;

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
        }

        //Below for Binding legalNote text
        async void bindLegalNoteData()
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




                var result = await CommonLib.LegalNote(CommonLib.ws_MainUrlMain + "ContactApi/LegalNote");

                if (result != null && result.Status != 0)
                {
                    
                        LoadPopup.CloseAllPopup();


                
                     legalNoteText.Text = result.Note.LegalText;


                    string ww = "<html><head></head><body><div>";
                    ww += result.Note.LegalText;
                        ww+= "</div></body></html>";
                    var jj = new HtmlWebViewSource();
                    jj.Html = ww;
                    legalNoteTextWeb.Source = jj;
                    if (Device.OS == TargetPlatform.Android) {
                        legalNoteText.IsVisible = true;
                    }else
                    {
                        legalNoteTextWeb.IsVisible = true;
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

    }
}