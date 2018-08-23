using GrylooProject.DependencyInterface;
using GrylooProject.Model;
using GrylooProject.Views;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrylooProject.Views
{
    public class HomeMasterPage : MasterDetailPage
    {

        MenuList masterPage;
        public HomeMasterPage()
        {
            this.Resources = new ResourceDictionary();
            this.Resources.Add("UlycesColor", Color.FromHex("#007f4f"));
        
            var navigationStyle = new Style(typeof(NavigationPage));

            var barTextColorSetter = new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White };
            var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.FromHex("#007f4f") };

            navigationStyle.Setters.Add(barTextColorSetter);
            navigationStyle.Setters.Add(barBackgroundColorSetter);

            this.Resources.Add(navigationStyle);

            //this.Padding = new Thickness(0, 10, 0, 0);
            //this.BackgroundImage = "splash.png";
            masterPage = new MenuList();
            Master = masterPage;
            Detail = new NavigationPage(new HomePage()) { BarBackgroundColor = Color.FromHex("#007f4f") };
            Padding = new Thickness(0);
            NavigationPage.SetHasNavigationBar(this, false);

            //Detail = new NavigationPage(new TicketPage())
            //{
            //    //BarBackgroundColor = Color.FromHex("#77D065"),
            //    //BarTextColor = Color.White
            //};
            masterPage.ListView.ItemSelected += OnItemSelected;
        }


        protected override bool OnBackButtonPressed()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:


                    break;
                case Device.Android:
                    DependencyService.Get<ICloseApplication>().closeApplication();

                    break;

            }



            return base.OnBackButtonPressed();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {

                if (item.Title == "")
                {
                   
                   

                  
                }

                if (item.Title == "Rate App")
                {
                    var urlStore = Device.OnPlatform("https://itunes.apple.com/ca/app/id683290832?mt=8", "https://play.google.com/store/apps/details?id=com.credential.csi", "");
                    Device.OpenUri(new Uri(urlStore));

                    IsPresented = false;
                    masterPage.ListView.SelectedItem = null;
                }

                //if (item.Title == "RecommendToAFriend")
                //{
                //   // await Navigation.PushPopupAsync(new RecommendToAFriend());


                //}
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }

    }
}