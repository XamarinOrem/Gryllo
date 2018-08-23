using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using GrylooProject.DependencyInterface;

namespace GrylooProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        double height = App.ScreenHeight;
        double width = App.ScreenWidth;

        float labelTextSize;

        List<entry> entries = new List<entry>
                 {
                     new entry(212)
                     {
                         Label = "Democatic",
                         ValueLabel = "212",
                         
                         
                         Color = SKColor.Parse("#2c3e50")
                     },
                     new entry(248)
                     {
                         Label = "Republic",
                         ValueLabel = "248",
                         Color = SKColor.Parse("#77d065")
                     },
                     new entry(128)
                     {
                         Label = "PP",
                         ValueLabel = "128",
                         Color = SKColor.Parse("#b455b6")
                     },
                     new entry(514)
                     {
                         Label = "UV",
                         ValueLabel = "514",
                         Color = SKColor.Parse("#3498db")
                } };



        List<entry> Leaders = new List<entry>
                 {
                     new entry(212)
                     {
                         Label = "Shelodn",
                         ValueLabel = "212",

                         Color = SKColor.Parse("#2c3e50")
                     },
                     new entry(248)
                     {
                         Label = "Naincy",
                         ValueLabel = "248",
                         Color = SKColor.Parse("#77d065")
                     },
                     new entry(128)
                     {
                         Label = "Beckett",
                         ValueLabel = "128",
                         Color = SKColor.Parse("#b455b6")
                     },
                     new entry(514)
                     {
                         Label = "Castle",
                         ValueLabel = "514",
                         Color = SKColor.Parse("#3498db")
                } };

        public HomePage() :base()
        {
            InitializeComponent();

           // NavigationPage.SetHasNavigationBar(this, false);


            CandituresButton.WidthRequest = (width / 2) - 10;

            leadersButton.WidthRequest= (width / 2) - 10;


            Title = "Grylloo";
            CandituresButton.BackgroundColor = Color.FromHex("#fcbf49");
            leadersButton.BackgroundColor =Color.White ;


            CanditatureView.IsVisible = true;
            leaderView.IsVisible = false;

           

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    
                    chartOne.WidthRequest = (width / 2) - 20;

                    chartOne.HeightRequest = (height / 10) * 3;

                    charttwo.WidthRequest = (width / 2) - 10;

                    charttwo.HeightRequest = (height / 10) * 3;

                    labelTextSize = 18;


                    //region of frame layout

                    frameInsideStacklayout.Padding = new Thickness(-5);

                    CandituresButton.Margin = new Thickness(0);

                    leadersButton.Margin= new Thickness(0);

                    break;


                case Device.Android:


                    if (width <= 359.833333333333)
                    {
                        chartOne.WidthRequest = (width / 2) - 20;

                        chartOne.HeightRequest = (height / 10) * 3;

                        charttwo.WidthRequest = (width / 2) - 10;

                        charttwo.HeightRequest = (height / 10) * 3;

                        labelTextSize = 12;
                    }
                    else if (width <= 361.1877740750141)
                    {
                        chartOne.WidthRequest = (width / 2) - 20;

                        chartOne.HeightRequest = (height / 10) * 3;

                        charttwo.WidthRequest = (width / 2) - 10;

                        charttwo.HeightRequest = (height / 10) * 3;

                        labelTextSize = 12;
                    }
                    else
                    {
                        chartOne.WidthRequest = (width / 2) - 10;

                        chartOne.HeightRequest = (height / 10) * 4;

                        charttwo.WidthRequest = (width / 2) - 10;

                        charttwo.HeightRequest = (height / 10) * 3;


                        labelTextSize = 20;
                    }

                    break;

            }

         

           

            chartOne.Chart = new Microcharts.DonutChart { Entries = entries,LabelTextSize= labelTextSize, HoleRadius=0,Margin=10 };
            charttwo.Chart = new Microcharts.RadialGaugeChart { Entries = Leaders,LabelTextSize= labelTextSize };
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



        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


        private async void canditureClicked(EventArgs e)
        {
            if (CandituresButton.BackgroundColor == Color.White)
            {
                leadersButton.BackgroundColor = Color.White;
                CandituresButton.BackgroundColor = Color.FromHex("#fcbf49");
                
               
                
            }
            CanditatureView.IsVisible = true;
            leaderView.IsVisible = false;


        }
        

         private async void leadersClicked(EventArgs e)
        {
            if (leadersButton.BackgroundColor == Color.White)
            {
                CandituresButton.BackgroundColor = Color.White;
                leadersButton.BackgroundColor = Color.FromHex("#fcbf49");

            }
            CanditatureView.IsVisible = false;
            leaderView.IsVisible = true;

        }


    }
}