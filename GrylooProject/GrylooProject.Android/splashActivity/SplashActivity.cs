﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GrylooProject.Droid.splashActivity
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private static int SPLASH_TIME = 1 * 1000;// 1 seconds
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            //Resources.GetText(Resource.String.app_slogan);
            SetContentView(Resource.Layout.Splash);


            try
            {
                new Handler().PostDelayed(() =>
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();

                }, SPLASH_TIME);

            }
            catch (Exception e) { }

        }
    }


}