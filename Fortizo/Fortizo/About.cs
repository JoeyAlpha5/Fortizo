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
    [Activity(Label = "About", Theme = "@style/MyCustomTheme")]
    public class About : Activity
    {

        TextView BackText;
        ImageView BackArrow;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.About);

            BackArrow = FindViewById<ImageView>(Resource.Id.AboutPageBackArrow);
            BackText = FindViewById<TextView>(Resource.Id.AboutPageBackText);


            BackArrow.Click += BackArrow_Click;
            BackText.Click += BackText_Click;


        }

        //methiods to go back to the previous screen
        private void BackText_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }

        private void BackArrow_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }


        public override void OnBackPressed()
        {
            StartActivity(typeof(menuPage));
        }



        //
    }
}