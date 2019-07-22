using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Fortizo
{
    [Activity(Label = "menuPage", Theme = "@style/MyCustomTheme")]
    public class menuPage : Activity
    {

        ImageView BackArrow;
        TextView SettingsPageHeading;
        TextView MobileNetworkSelectLink;
        TextView ElectricitySettingsLink;
        TextView AboutText;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.menuPage);
            BackArrow = FindViewById<ImageView>(Resource.Id.SettingsBackBtn);
            SettingsPageHeading = FindViewById<TextView>(Resource.Id.SettingsPageHeading);
            MobileNetworkSelectLink = FindViewById<TextView>(Resource.Id.MobileSelectLink);
            ElectricitySettingsLink = FindViewById<TextView>(Resource.Id.PrepaidElectricityLink);
            AboutText = FindViewById<TextView>(Resource.Id.About);

            MobileNetworkSelectLink.Click += MobileNetworkSelectLink_Click; ;
            BackArrow.Click += BackArrow_Click;
            SettingsPageHeading.Click += SettingsPageHeading_Click;
            ElectricitySettingsLink.Click += ElectricitySettingsLink_Click;
            AboutText.Click += AboutText_Click;
        }

        private void AboutText_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(About));
        }

        private void SettingsPageHeading_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void BackArrow_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void MobileNetworkSelectLink_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Country));
        }

        private void ElectricitySettingsLink_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PrepaidElectricitySettings));
        }




        //when back button is clicked on the settings page
        public override void OnBackPressed()
        {
            
            StartActivity(typeof(MainActivity));
        }
    }
}