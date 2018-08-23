using BottomBar.XamarinForms;
using GrylooProject.DependencyInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace GrylooProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomPageView :BottomBarPage
    {

        static Page _page;
        public BottomPageView( )
        {

            InitializeComponent();
            this.BackgroundColor = Color.Green;
            NavigationPage.SetHasNavigationBar(this, false);
            _page = this;
        }
        protected override void OnAppearing()
        {
            
        }
        public static void hh()
        {
            var hhh = (BottomPageView)_page;
            hhh.CurrentPage = new RateLeadersPage();
        }

        protected override bool OnBackButtonPressed()
        {


            if (Device.OS == TargetPlatform.Android)
            {
                if (Help.navigationCheck == true){
                   // DependencyService.Get<ICloseApplication>().closeApplication();
                    return true;
            }

                else
                {
                    Help.navigationCheck = true;
                    return base.OnBackButtonPressed();
                }

            }
            else
            {
                return true;
            }


        }

    }



}
