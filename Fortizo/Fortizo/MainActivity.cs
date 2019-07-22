using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using ZXing.Mobile;
using System;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;
using Plugin.Messaging;

namespace Fortizo
{
    [Activity(Label = "Fortizo", MainLauncher = true, Theme = "@style/MyCustomTheme", Icon ="@drawable/Icon")]
    public class MainActivity : Activity
    {

        Button Mybutton;
        TextView SettingsLink;
        TextView BalanceCheck;
        TextView DataBundles;
        TextView CustomerCare;
        TextView ElectricityScanLink;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //declare variables
            Mybutton = FindViewById<Button>(Resource.Id.button);
            SettingsLink = FindViewById<TextView>(Resource.Id.SettingsLink);
            BalanceCheck = FindViewById<TextView>(Resource.Id.BalanceCheck);
            DataBundles = FindViewById<TextView>(Resource.Id.DataBundles);
            CustomerCare = FindViewById<TextView>(Resource.Id.CustomerCare);
            ElectricityScanLink = FindViewById<TextView>(Resource.Id.ElectricityScan);

            //scanner initialization
            MobileBarcodeScanner.Initialize(Application);

            Mybutton.Click += Mybutton_Click;
            BalanceCheck.Click += BalanceCheck_Click;
            DataBundles.Click += DataBundles_Click;
            CustomerCare.Click += CustomerCare_Click;
            ElectricityScanLink.Click += ElectricityScanLink_ClickAsync;
            SettingsLink.Click += SettingsLink_Click;

        }

        //display the settings or menu page
        private void SettingsLink_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(menuPage));
        }


        private async void ElectricityScanLink_ClickAsync(object sender, EventArgs e)
        {
            
            var StoredElectricitySettings = Application.Context.GetSharedPreferences("StoredMeterNumber", FileCreationMode.Private);
            string MeterNumber = StoredElectricitySettings.GetString("MeterNumber", null);
           


            if (MeterNumber == null)
            {

                AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                Alert.SetTitle("Meter number");
                Alert.SetMessage("Please enter your meter number under settings");
                Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                Alert.Show();
            }
            else
            {
                if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) != (int)Permission.Granted)
                {
                   

                    AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                    Alert.SetTitle("Meter number");
                    Alert.SetMessage("Allow permission and click once more");
                    Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                    Alert.Show();

                    int REQUEST_CALLPHONE = 0;
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, REQUEST_CALLPHONE);

                }
                else
                {

                    Toast.MakeText(this, "Loading...", ToastLength.Long).Show();

                    var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                    var result = await scanner.Scan();

                    if (result != null)
                    {
                        string end = "%23";
                        string Begin = "*120*41589";
                        var uri = Android.Net.Uri.Parse("tel:" + Begin + "*" + result + "*" + MeterNumber + end);
                        var intent = new Intent(Intent.ActionCall, uri);
                        StartActivity(intent);
                    }

                   
                }
            }
        }


        //customer care method
        private void CustomerCare_Click(object sender, EventArgs e)
        {
            var StoredNetworkFolder = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
            string StoredNetwork = StoredNetworkFolder.GetString("SelectedNetwork", null);

            if (StoredNetwork == null || StoredNetwork == "---")
            {

                AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                Alert.SetTitle("Mobile network");
                Alert.SetMessage("Please select a mobile network under settings");
                Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                Alert.Show();

                
            }
            else
            {
                //check if phone permission is disabled
                if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) != (int)Permission.Granted)
                {


                    AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                    Alert.SetTitle("Permission Request");
                    Alert.SetMessage("Allow permission and click once more");
                    Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                    Alert.Show();

                    int REQUEST_CALLPHONE = 0;
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, REQUEST_CALLPHONE);


                }
                else
                {
                    string NumberToDial;
                    var SavedCountry = Application.Context.GetSharedPreferences("StoredCountry", FileCreationMode.Private);
                    string CountryName = SavedCountry.GetString("CountryName", null);

                    if (CountryName == "South Africa")
                    {
                        if (StoredNetwork == "Cell C" || StoredNetwork == "PSB")
                        {
                            NumberToDial = "084140";
                        }
                        else if (StoredNetwork == "Vodacom")
                        {
                            NumberToDial = "082111";
                        }
                        else if (StoredNetwork == "Telkom")
                        {
                            NumberToDial = "081180";
                        }
                        else
                        {
                            NumberToDial = "083173";
                        }


                    }
                    
                    else
                    {

                        NumberToDial = "0";
                    }

                    
                    var uri = Android.Net.Uri.Parse("tel:" + NumberToDial);
                    var intent = new Intent(Intent.ActionCall, uri);
                    StartActivity(intent);


                }
            }
        }

        private void DataBundles_Click(object sender, EventArgs e)
        {
          
            var StoredNetworkFolder = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
            string StoredNetwork = StoredNetworkFolder.GetString("SelectedNetwork", null);

            if (StoredNetwork == null || StoredNetwork == "---")
            {
                
                AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                Alert.SetTitle("Mobile network");
                Alert.SetMessage("Please select a mobile network under settings");
                Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                Alert.Show();

            }
            else
            {
                //check if phone permission is disabled
                if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) != (int)Permission.Granted)
                {



                    AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                    Alert.SetTitle("Mobile network");
                    Alert.SetMessage("Allow permission and click once more");
                    Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                    Alert.Show();


                    int REQUEST_CALLPHONE = 0;
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, REQUEST_CALLPHONE);


                }
                else
                {
                    string NumberToDial;
                    var SavedCountry = Application.Context.GetSharedPreferences("StoredCountry", FileCreationMode.Private);
                    string CountryName = SavedCountry.GetString("CountryName", null);


                    if (CountryName == "South Africa")
                    {

                        if (StoredNetwork == "Cell C" || StoredNetwork == "PSB")
                        {
                            NumberToDial = "*147";
                        }
                        else if (StoredNetwork == "Vodacom")
                        {
                            NumberToDial = "*111";
                        }
                        else if (StoredNetwork == "Telkom")
                        {
                            NumberToDial = "*147";
                        }
                        else
                        {
                            NumberToDial = "*141*2";
                        }


                    }

                    else if (CountryName == "Botswana")
                    {
                        if (StoredNetwork == "Orange")
                        {
                            NumberToDial = "*148";
                        }
                        else
                        {
                            NumberToDial = "*123";
                        }

                    }

                    else if (CountryName == "Zambia")
                    {

                        if (StoredNetwork == "MTN")
                        {

                            NumberToDial = "*335";

                        }

                        else 
                        {

                            NumberToDial = "*575";

                        }

                    }

                    else if (CountryName == "Dubai")
                    {

                        if (StoredNetwork == "Du")
                        {
                            NumberToDial = "*135*244";
                        }
                        else
                        {
                            NumberToDial = "0";
                        }

                    }

                    else if (CountryName == "Malawi")
                    {
                        if (StoredNetwork == "TNM")
                        {
                            NumberToDial = "*200*55";
                        }
                        else
                        {
                            NumberToDial = "0";
                        }
                    }

                    else if (CountryName == "Zimbabwe")
                    {

                        if (StoredNetwork == "Econet")
                        {
                            NumberToDial = "*143";

                        }else if (StoredNetwork == "Telecel")
                        {
                            NumberToDial = "*144";
                        }
                        else
                        {
                            NumberToDial = "0";
                        }

                    }

                    else if (CountryName == "Ghana")
                    {
                        if (StoredNetwork == "MTN")
                        {
                            NumberToDial = "*138*1";
                        }
                        else if (StoredNetwork == "Tigo")
                        {
                            NumberToDial = "*500";
                        }

                        else if (StoredNetwork == "Vodafone")
                        {
                            NumberToDial = "*700";
                        }
                        else
                        {
                            NumberToDial = "*125";
                        }
                    }

                    else
                    {
                        NumberToDial = "0";
                    }
                    


                    string end = "%23";
                    var uri = Android.Net.Uri.Parse("tel:" + NumberToDial + end);
                    var intent = new Intent(Intent.ActionCall, uri);
                    StartActivity(intent);


                }
            }


        }

        //balance check method
        private void BalanceCheck_Click(object sender, EventArgs e)
        {

            var StoredNetworkFolder = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
            string StoredNetwork = StoredNetworkFolder.GetString("SelectedNetwork", null);

            var SavedCountry = Application.Context.GetSharedPreferences("StoredCountry", FileCreationMode.Private);
            string CountryName = SavedCountry.GetString("CountryName", null);

            if (StoredNetwork == null || StoredNetwork == "---")
            {
              

                AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                Alert.SetTitle("Mobile network");
                Alert.SetMessage("Please select a mobile network under settings");
                Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                Alert.Show();


            }
            else
            {
                //check if phone permission is disabled
                if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) != (int)Permission.Granted)
                {

                    AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                    Alert.SetTitle("Mobile network");
                    Alert.SetMessage("Allow permission and click once more");
                    Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                    Alert.Show();


                    int REQUEST_CALLPHONE = 0;
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, REQUEST_CALLPHONE);


                }
                else
                {

                    string NumberToDial;

                    if (CountryName == "South Africa") {

                        if (StoredNetwork == "Cell C" || StoredNetwork == "PSB")
                        {
                            NumberToDial = "*101";
                        }
                        else if (StoredNetwork == "Vodacom")
                        {
                            NumberToDial = "*100";
                        }
                        else if (StoredNetwork == "Telkom")
                        {
                            NumberToDial = "*188";
                        }
                        else
                        {
                            NumberToDial = "*141";
                        }

                    }
                    else if (CountryName == "Dubai")
                    {
                        if (StoredNetwork == "Du")
                        {
                            NumberToDial = "*135";


                        }
                        else
                        {
                            NumberToDial = "*121";
                        }

                    }
                    else if (CountryName == "Zambia")
                    {
                        NumberToDial = "*114";
                    }
                    else if (CountryName == "Malawi")
                    {
                        if (StoredNetwork == "Airtel")
                        {
                            NumberToDial = "*137";


                        }
                        else
                        {
                            NumberToDial = "*123";
                        }
                    }
                    else if (CountryName == "Botswana")
                    {

                        if (StoredNetwork == "Orange")
                        {
                            NumberToDial = "*155";


                        }
                        else
                        {
                            NumberToDial = "*102";
                        }


                    }
                    else if (CountryName == "Ghana")
                    {
 
                      NumberToDial = "*124";


                    }


                    else if (CountryName == "Namibia")
                    {
                        NumberToDial = "*131";

                    }

                    else
                    {
                        NumberToDial = "0";
                    }

                   
                    string end = "%23";
                    var uri = Android.Net.Uri.Parse("tel:" + NumberToDial + end);
                    var intent = new Intent(Intent.ActionCall, uri);
                    StartActivity(intent);


                }
            }

            
        }

        //scanner code
        private async void Mybutton_Click(object sender, System.EventArgs e)
        {
            
            var StoredNetworkFolder = Application.Context.GetSharedPreferences("FortizoSelectedNetwork", FileCreationMode.Private);
            string StoredNetwork = StoredNetworkFolder.GetString("SelectedNetwork", null);

            var SavedCountry = Application.Context.GetSharedPreferences("StoredCountry", FileCreationMode.Private);
            string CountryName = SavedCountry.GetString("CountryName", null);

            if (StoredNetwork == null || StoredNetwork == "---")
            {


                AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                Alert.SetTitle("Mobile network");
                Alert.SetMessage("Please select a mobile network under settings");
                Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                Alert.Show();



            }
            else
            {
                //check if phone & sms permission is disabled
                if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) != (int)Permission.Granted)
                {

                    

                    AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                    Alert.SetTitle("Mobile network");
                    Alert.SetMessage("Allow permissions and click scan once more");
                    Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                    Alert.Show();

                    int REQUEST_CALLPHONE = 0;
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, REQUEST_CALLPHONE);


                }

                else if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) != (int)Permission.Granted)
                {
                    AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                    Alert.SetTitle("Mobile network");
                    Alert.SetMessage("Allow permissions and click scan once more");
                    Alert.SetPositiveButton("Ok", delegate { Alert.Dispose(); });
                    Alert.Show();

                    int REQUEST_SENDSMS = 0;
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.SendSms }, REQUEST_SENDSMS);
                }

                else
                {


                    AlertDialog.Builder Alert = new AlertDialog.Builder(this);
                    Alert.SetTitle("Scan Options");
                    Alert.SetMessage("Do you wish to scan and share or simply scan and recharge?");
                    
                    //scan and regarge
                    Alert.SetNegativeButton("Recharge", async delegate {

                            Toast.MakeText(this, "Loading...", ToastLength.Long).Show();                 
                            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                            var result = await scanner.Scan();

                            if (result != null)
                            {
                                //what to do when scan is successful
                                string Begin;
                                string end = "%23";


                                if (CountryName == "South Africa")
                                {
                                    if (StoredNetwork == "Cell C")
                                    {
                                        Begin = "*102*";


                                    }
                                    else if (StoredNetwork == "MTN")
                                    {
                                        Begin = "*141*";
                                    }
                                    else if (StoredNetwork == "PSB")
                                    {
                                        Begin = "*102*";
                                    }
                                    else if (StoredNetwork == "Telkom")
                                    {
                                        Begin = "*188*";
                                    }
                                    else
                                    {
                                        Begin = "*100*01*";
                                    }
                                }

                                else if (CountryName == "Dubai")
                                {
                                    if (StoredNetwork == "Du")
                                    {
                                        Begin = "*135*";


                                    }
                                    else 
                                    {
                                        Begin = "*222*";
                                    }
                            
                            

                                }

                                else if (CountryName == "Ghana")
                                {
                                    Begin = "*134*";


                                }

                                else if (CountryName == "Zambia")

                                {
                                    Begin = "*113*";

                                }

                                else if (CountryName == "Namibia")
                                {
                                    Begin = "*132";


                                }

                                else if (CountryName == "Malawi")
                                {
                                    if (StoredNetwork == "Airtel")
                                    {
                                        Begin = "*136*";


                                    }
                                    else
                                    {
                                        Begin = "*111*";
                                    }
                                
                            
                                } else if (CountryName == "Zimbabwe")
                                {
                                    if (StoredNetwork == "Econet")
                                    {
                                        Begin = "*121*";


                                    }
                                    else if (StoredNetwork == "Telecel")
                                    {
                                        Begin = "*151*";
                                    }
                                    else
                                    {
                                        Begin = "*134*";
                                    }
                                }
                                else
                                {
                                    Begin = "0";
                                }


                                var uri = Android.Net.Uri.Parse("tel:" + Begin + result + end);
                                var intent = new Intent(Intent.ActionCall, uri);
                                StartActivity(intent);

                            }



                    });
                    


                    //scan and share
                    Alert.SetPositiveButton("Share", async delegate {

                        Toast.MakeText(this, "Loading...", ToastLength.Long).Show();
                        var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                        var result = await scanner.Scan();

                        if (result != null)
                        {
                            SendSms(result.ToString());
                        }
                        


                    });
                    Alert.Show();


                    
                }

            }


        }

        private void SendSms(string result)
        {
            var smsMessenger = CrossMessaging.Current.SmsMessenger;

            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms("", "Recharge voucher sent from Fortizo mobile app: " + result);
            }

        }

        public override void OnBackPressed()
        {

            this.FinishAffinity();
        }


    }
}

