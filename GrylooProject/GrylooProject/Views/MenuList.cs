using GrylooProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;



namespace GrylooProject.Views
{
    public class MenuList : ContentPage
    {
        public static string Logout = "";

        public ListView ListView { get { return listView; } }
        public static ListView listView;

        public MenuList()
        {
            BackgroundImage = "splash.png";
            var masterPageItems = new List<MasterPageItem>();



            masterPageItems.Add(new MasterPageItem
            {
                Title = "Home",
                IconSource = "home.png",
                TargetType = typeof(HomePage),

            });


            masterPageItems.Add(new MasterPageItem
            {
                Title = "Results",
                IconSource = "chart",
                TargetType = typeof(ChartsPage),

            });


            masterPageItems.Add(new MasterPageItem
            {
                Title = "Parties",
                IconSource = "voteWhite.png",
                TargetType = typeof(VotePartiesPage),

            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Leaders",
                IconSource = "leaders.png",
                TargetType = typeof(RateLeadersPage),


            });


          



            masterPageItems.Add(new MasterPageItem
            {
                Title = "Rate App",
                
                IconSource = "whitestar.png",
                // TargetType = typeof(ShareApp),


            });


          

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Configure",
                IconSource = "settingsNew.png",
                TargetType = typeof(SettingPage),


            });












            listView = new ListView
            {

                ItemsSource = masterPageItems,
                Margin = new Thickness(0, 10, 0, 0),
                ItemTemplate = new DataTemplate(typeof(CustomCell)),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None,
                //  BackgroundColor = Color.FromHex("#000000"),
                BackgroundColor = Color.Transparent,

            };

            Padding = new Thickness(0,30, 0, 0);
            Icon = ("hamburger");
            Title = "Home";
            //BackgroundColor = Color.FromHex("#000000");
            BackgroundColor = Color.Transparent;

            Image userImg = new Image()
            {
                Source = "logo.png",
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
            };


            //Label userName = new Label()
            //{
            //    Text = "Username",
            //    FontSize = 25,
            //    TextColor = Color.White,
            //    HorizontalOptions = LayoutOptions.Center,
            //};

            //BoxView boxView = new BoxView
            //{
            //    HeightRequest = 0.5,
            //    BackgroundColor = Color.FromHex("#d4d4d4"),
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    Margin = new Thickness(0, 0, 0, 0),


            //};


            StackLayout _layoutuser = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Padding = new Thickness(20, 10, 20, 25),
                BackgroundColor = Color.Transparent,
                Children = {
                    userImg
                }

            };
            var lblClose_tgr = new TapGestureRecognizer();
            lblClose_tgr.Tapped += LblClose_tgr_Tapped;
            _layoutuser.GestureRecognizers.Add(lblClose_tgr);


            StackLayout _layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15,

                Children = {
                   _layoutuser,listView
                }
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,

                Children = {
                   _layout
                }
            };
        }

        private async void LblClose_tgr_Tapped(object sender, EventArgs e)
        {
            //  await Navigation.PushAsync(new UpdateProfilePhoto());
        }
        public class CustomCell : ViewCell
        {
            int fontSize = 18;
            public CustomCell()
            {
                Image img = new Image
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(20, 0, 0, 0),
                    WidthRequest = 25,
                    HeightRequest = 25,
                };
                img.SetBinding(Image.SourceProperty, new Binding("IconSource"));

                Label lbl = new Label
                {
                    FontSize = fontSize,
                    HorizontalTextAlignment = TextAlignment.Start,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White,
                    Margin = new Thickness(10, 0, 0, 0),
                };
                lbl.SetBinding(Label.TextProperty, new Binding("Title"));



                var sl_projectinfo = new StackLayout
                {
                    HeightRequest = 50,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(5, 0, 5, 0),
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                    Children =
                    {
                        img,lbl
                    }
                };
                View = sl_projectinfo;
            }

        }
    }
}