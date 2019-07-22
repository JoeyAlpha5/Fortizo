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
    [Activity(Label = "PrepaidElectricitySettings", Theme = "@style/MyCustomTheme")]
    public class PrepaidElectricitySettings : Activity
    {
        Button SaveButton;
        EditText MeterNumber;
        ImageView BackArrow;
        TextView BackText;
        


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ElectricitySettings);
            SaveButton = FindViewById<Button>(Resource.Id.SaveElectricitySettingsBtn);
            MeterNumber = FindViewById<EditText>(Resource.Id.MeterNumberInput);
            BackText = FindViewById<TextView>(Resource.Id.ElectricityPageBackText);
            BackArrow = FindViewById<ImageView>(Resource.Id.ElectricityPageBackArrow);


            SaveButton.Click += SaveButton_Click;
            BackText.Click += BackText_Click;
            BackArrow.Click += BackArrow_Click;

            var StoredElectricitySettings = Application.Context.GetSharedPreferences("StoredMeterNumber", FileCreationMode.Private);
            string b = StoredElectricitySettings.GetString("MeterNumber", null);

            if (b != null)
            {
                MeterNumber.Text = b;
            }

        }

        private void BackArrow_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }

        private void BackText_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            var StoredElectricitySettings = Application.Context.GetSharedPreferences("StoredMeterNumber", FileCreationMode.Private);
            var a = StoredElectricitySettings.Edit();

            if (MeterNumber.Text == "")
            {
               

                AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                Alert.SetTitle("Meter number");
                Alert.SetMessage("Invalid meter number or pin");
                Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                Alert.Show();


            }
            else
            {
                Toast.MakeText(this, "Saved", ToastLength.Long).Show();
                a.PutString("MeterNumber", MeterNumber.Text);
                a.Commit();
            }
        }



        public override void OnBackPressed()
        {
            StartActivity(typeof(menuPage));
        }


    }
}