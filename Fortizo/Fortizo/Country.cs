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
    [Activity(Label = "Country", Theme = "@style/MyCustomTheme")]
    public class Country : Activity
    {
        TextView CountrySelecter;
        TextView Dubai;
        TextView Malawi;
        TextView Ghana;
        TextView Botswana;
        TextView Namibia;
        TextView SA;
        TextView Zambia;
        TextView Zim;
        TextView StoredNetwork;
        LinearLayout AvailableCountriesContainer;
        TextView BackText;
        ImageView BackArrow;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Country);

            CountrySelecter = FindViewById<TextView>(Resource.Id.CountrySelecter);
            BackText = FindViewById<TextView>(Resource.Id.CountryPageBackText);
            BackArrow = FindViewById<ImageView>(Resource.Id.CountryBackArrow);
            Dubai = FindViewById<TextView>(Resource.Id.Dubai);
            Malawi = FindViewById<TextView>(Resource.Id.Malawi);
            SA = FindViewById<TextView>(Resource.Id.SA);
            Zambia = FindViewById<TextView>(Resource.Id.Zambia);
            Ghana = FindViewById<TextView>(Resource.Id.Ghana);
            Botswana = FindViewById<TextView>(Resource.Id.Botswana);
            Namibia = FindViewById<TextView>(Resource.Id.Namibia);
            Zim = FindViewById<TextView>(Resource.Id.Zim);
            StoredNetwork = FindViewById<TextView>(Resource.Id.StoredNetwork);

            //stored country
            var SavedCountry = Application.Context.GetSharedPreferences("StoredCountry", FileCreationMode.Private);
            string CountryName = SavedCountry.GetString("CountryName", null);

            //stored network
            var a = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
            string b = a.GetString("SelectedNetwork", null);

            if (CountryName != null)
            {
                CountrySelecter.Text = CountryName;
            }

            if (b != null) {

                StoredNetwork.Text = b;

            }


            //clicks
            CountrySelecter.Click += CountrySelecter_Click;
            BackText.Click += BackText_Click;
            BackArrow.Click += BackArrow_Click;
            Dubai.Click += Country_Click;
            Botswana.Click += Country_Click;
            Namibia.Click += Country_Click; ;
            Ghana.Click += Country_Click; ;
            Malawi.Click += Country_Click;
            SA.Click += Country_Click;
            Zambia.Click += Country_Click;
            Zim.Click += Country_Click;
        }

        private void BackArrow_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }

        private void BackText_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }

        private void Country_Click(object sender, EventArgs e)
        {

            TextView SelectedCountry = (TextView) sender;

            var SavedCountry = Application.Context.GetSharedPreferences("StoredCountry", FileCreationMode.Private);
            

            CountrySelecter = FindViewById<TextView>(Resource.Id.CountrySelecter);
            

            AvailableCountriesContainer = FindViewById<LinearLayout>(Resource.Id.AvailableCountriesContainer);
            AvailableCountriesContainer.Visibility = ViewStates.Invisible;

            AlertDialog.Builder alert  = new AlertDialog.Builder(this);
            alert.SetTitle("Mobile Network");
            alert.SetMessage("Selected Country is " + SelectedCountry.Text + " Click continue to select your mobile network");
            alert.SetNegativeButton("Cancel", delegate { alert.Dispose(); });
            alert.SetPositiveButton("Continue", delegate {


                //save selected country
                CountrySelecter.Text = SelectedCountry.Text;

                var a = SavedCountry.Edit();
                a.PutString("CountryName", SelectedCountry.Text);
                a.Commit();


                StartActivity(typeof(NetworkSelectPage));

                //create the folder to store the selected network
                var StoredNetworkFolder = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
                //edit the stored network folder
                var StoredNetwork = StoredNetworkFolder.Edit();
                StoredNetwork.PutString("SelectedNetwork", "---");
                StoredNetwork.Commit();

            });
            alert.Show();

        }

        private void CountrySelecter_Click(object sender, EventArgs e)
        {
            AvailableCountriesContainer = FindViewById<LinearLayout>(Resource.Id.AvailableCountriesContainer);
            AvailableCountriesContainer.Visibility = ViewStates.Visible;
        }


        public override void OnBackPressed()
        {
            StartActivity(typeof(menuPage));
        }

    }
}