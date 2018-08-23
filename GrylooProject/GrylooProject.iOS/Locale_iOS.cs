using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using GrylooProject.DependencyInterface;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(GrylooProject.iOS.Locale_iOS))]
namespace GrylooProject.iOS
{
    public class Locale_iOS : ILocale
    {
        public void SetLocale()
        {
            try
            {
                var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
                var netLocale = iosLocaleAuto.Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch (Exception)
            {
            }
        }


        public string GetCurrent()
        {
            var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;

            var netLocale = iosLocaleAuto.Replace("_", "-");
            var netLanguage = iosLanguageAuto.Replace("_", "-");


            //if (netLocale == "en-US")
            //{ 
            //    netLanguage = "en-US";
            //    netLocale = "en-US";
            //}
            //else
            //{
            //    netLanguage = "es-ES";
            //    netLocale = "es-ES";
            //}
            netLanguage = "es-ES";
            netLocale = "es-ES";



            return netLanguage;

            #region Debugging Info
            // prefer *Auto updating properties
            //   var iosLocale = NSLocale.CurrentLocale.LocaleIdentifier;
            //   var iosLanguage = NSLocale.CurrentLocale.LanguageCode;
            //   var netLocale = iosLocale.Replace ("_", "-");
            //   var netLanguage = iosLanguage.Replace ("_", "-");

            Console.WriteLine("nslocaleid:" + iosLocaleAuto);
            Console.WriteLine("nslanguage:" + iosLanguageAuto);
            Console.WriteLine("ios:" + iosLanguageAuto + " " + iosLocaleAuto);
            Console.WriteLine("net:" + netLanguage + " " + netLocale);

            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("thread:  " + Thread.CurrentThread.CurrentCulture);
            Console.WriteLine("threadui:" + Thread.CurrentThread.CurrentUICulture);
            #endregion

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = pref.Replace("_", "-");
                Console.WriteLine("preferred:" + netLanguage);
            }
            else
            {
                netLanguage = "en"; // default, shouldn't really happen :)
            }
            return netLanguage;
        }

    }

}