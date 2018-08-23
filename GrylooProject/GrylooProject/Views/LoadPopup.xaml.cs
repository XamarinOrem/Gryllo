using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class LoadPopup : PopupPage
    {
        public LoadPopup()
        {
            InitializeComponent();
        }
        public static async void CloseAllPopup()
        {
            // await App.Current.MainPage.Navigation.PopPopupAsync(false);
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }

        public static async void CloseAllPopup1()
        {
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }
        public static async void CloseAllPopup3()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(false);
        }
    }
}