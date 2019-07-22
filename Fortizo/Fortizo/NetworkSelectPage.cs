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
    [Activity(Label = "NetworkSelectPage", Theme= "@style/MyCustomTheme")]
    public class NetworkSelectPage : Activity
    {

        TextView NetworkSelectPageHeading;
        ImageView NetworkSelectBackArrow;
        TextView NetworkSelecter;
        LinearLayout AvailableMobileNetworks;
        TextView Network1;
        TextView Network2;
        TextView Network3;
        TextView Network4;
        TextView Network5;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SelectMobileNetwork);

            NetworkSelectPageHeading = FindViewById<TextView>(Resource.Id.NetworkPageHeading);
            NetworkSelectBackArrow = FindViewById<ImageView>(Resource.Id.NetworkBackBtn);
            NetworkSelecter = FindViewById<TextView>(Resource.Id.NetworkSelecter);
            Network1 = FindViewById<TextView>(Resource.Id.Network1);
            Network2 = FindViewById<TextView>(Resource.Id.Network2);
            Network3 = FindViewById<TextView>(Resource.Id.Network3);
            Network4 = FindViewById<TextView>(Resource.Id.Network4);
            Network5 = FindViewById<TextView>(Resource.Id.Network5);
            


            var a = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
            string b = a.GetString("SelectedNetwork", null);

            if (b == null)
            {
                NetworkSelecter.Text = "---";
            }
            else
            {
                NetworkSelecter.Text = b;
            }


            var c = Application.Context.GetSharedPreferences("StoredCountry", FileCreationMode.Private);
            string d = c.GetString("CountryName", null);

            if (d == "South Africa")
            {
                Network1.Text = "Cell C";
                Network2.Text = "MTN";
                Network3.Text = "PSB";
                Network4.Text = "Telkom";
                Network5.Text = "Vodacom";
            }

            else if (d == "Dubai")
            {
                Network1.Text = "Du";
                Network2.Text = "Etisalat";
                Network3.Text = "---";
                Network4.Text = "---";
                Network5.Text = "---";
            }
            else if (d == "Zambia")
            {
                Network1.Text = "Airtel";
                Network2.Text = "Cell C";
                Network3.Text = "MTN";
                Network4.Text = "---";
                Network5.Text = "---";
            }
            else if(d == "Malawi")
            {
                Network1.Text = "Airtel";
                Network2.Text = "TNM";
                Network3.Text = "---";
                Network4.Text = "---";
                Network5.Text = "---";
            }

            else if (d == "Botswana")
            {
                Network1.Text = "Orange";
                Network2.Text = "Mascon";
                Network3.Text = "---";
                Network4.Text = "---";
                Network5.Text = "---";


            }
            else if (d == "Ghana")
            {
                Network1.Text = "MTN";
                Network2.Text = "Tigo";
                Network3.Text = "Vodafone";
                Network4.Text = "Airtel";
                Network5.Text = "---";


            }
            else if (d == "Namibia")
            {
                Network1.Text = "Mtc";
                Network2.Text = "Telecel";
                Network3.Text = "---";
                Network4.Text = "---";
                Network5.Text = "---";


            }
            else
            {
                Network1.Text = "Econet";
                Network2.Text = "Net1";
                Network3.Text = "Telecel";
                Network4.Text = "---";
                Network5.Text = "---";
            }



            NetworkSelectPageHeading.Click += NetworkSelectPageHeading_Click;
            NetworkSelectBackArrow.Click += NetworkSelectBackArrow_Click;
            NetworkSelecter.Click += NetworkSelecter_Click;
            Network1.Touch += Network_Touch;
            Network2.Touch += Network_Touch;
            Network3.Touch += Network_Touch;
            Network4.Touch += Network_Touch;
            Network5.Touch += Network_Touch;

           
            

        }

        private void NetworkSelecter_Click(object sender, EventArgs e)
        {
            AvailableMobileNetworks = FindViewById<LinearLayout>(Resource.Id.AvailableMobileNetworks);
            AvailableMobileNetworks.Visibility = ViewStates.Visible;
            
        }

        private void NetworkSelectBackArrow_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }

        private void NetworkSelectPageHeading_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }


        //selected network
        private void Network_Touch(object sender, View.TouchEventArgs e)
        {

            TextView SelectedNetwork = (TextView) sender;

            

            if (SelectedNetwork.Text == "---")
            {



                AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                Alert.SetTitle("Mobile Network");
                Alert.SetMessage("Invalid mobile network");
                Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); StartActivity(typeof(Country)); });
                Alert.Show();

                

            }

            else
            {
                NetworkSelecter.Text = SelectedNetwork.Text;

                //create the folder to store the selected network
                var StoredNetworkFolder = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
                //edit the stored network folder
                var StoredNetwork = StoredNetworkFolder.Edit();
                StoredNetwork.PutString("SelectedNetwork", NetworkSelecter.Text);
                StoredNetwork.Commit();

                AvailableMobileNetworks = FindViewById<LinearLayout>(Resource.Id.AvailableMobileNetworks);
                AvailableMobileNetworks.Visibility = ViewStates.Invisible;
                Toast.MakeText(this, "Saving please wait...", ToastLength.Short).Show();
                StartActivity(typeof(Country));
            }





        }


        public override void OnBackPressed()
        {

            
           StartActivity(typeof(menuPage));

        }

    }
}