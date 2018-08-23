using Plugin.Share;
using Plugin.Share.Abstractions;
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
    public partial class ShareApp : ContentPage
    {
        public ShareApp()
        {
            InitializeComponent();
            Title = "Share";
            if (Device.OS == TargetPlatform.Android)
            {
                BackgroundColor = Color.White;

            }
        }

        private async void share_Clicked(object sender, System.EventArgs e)
        {


            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                   
                    var msgtext = "Application Name:- Grylloo,Link:-http://grylloo.com";
                    ShareMessage msg = new ShareMessage();
                    msg.Text = msgtext;
                    await CrossShare.Current.Share(msg,null);
                    break;

                    case Device.Android:
                    ShareMessage txt = new ShareMessage();
                    txt.Text = "Application Name:- Grylloo,Link:-http://grylloo.com";
                    CrossShare.Current.Share(txt, null);

                    break;

            }

        }
    }
}