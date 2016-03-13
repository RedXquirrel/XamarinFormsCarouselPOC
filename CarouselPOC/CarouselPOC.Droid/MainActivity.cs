using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Xamtastic.Xamarin.Forms.CarouselView.CustomLayouts;

namespace CarouselPOC.Droid
{
    [Activity(Label = "CarouselPOC", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CarouselView.Init(typeof(App));
            LoadApplication(new App());
        }
    }
}
