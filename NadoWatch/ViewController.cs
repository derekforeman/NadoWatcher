using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UIKit;
using Plugin.Connectivity;
using Foundation;

namespace NadoWatch
{
    public partial class ViewController : UIViewController
    {
        private const string UrlKey = "name_preference";
        private static readonly string UrlDefault = "https://gist.githubusercontent.com/derekforeman/7f0a1914f623530499340d4c2aa20a93/raw/45e1e258992b6236a526977e78d3d14b0d674cf4/description.txt";


        public bool DoIHaveInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            //txtDescription.AdjustsFontForContentSizeCategory = true;
            await GetData();

            BtnRefresh.TouchUpInside += async delegate {
                await GetData();
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        private async Task<bool> GetData()
        {
            if(DoIHaveInternet()) 
            {
                try {
                    var url = GetUrlFromSettings();
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));

                    request.Method = "GET";

                    using (WebResponse response = await request.GetResponseAsync())
                    {
                        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                        {
                            var s = stream.ReadToEnd();
                            txtDescription.Text = s;
                            return true;
                            //process the response
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the URL from settings.
        /// </summary>
        /// <returns>The URL from settings.</returns>
        private string GetUrlFromSettings()
        {
            
            var setList = NSUserDefaults.StandardUserDefaults;

            var prefUrl = setList.StringForKey(UrlKey);
            if (String.IsNullOrEmpty(prefUrl))
            {
                setList.SetString(UrlDefault, UrlKey);
            }


            return setList.StringForKey(UrlKey);

        }

        /// <summary>
        /// Iterates the settings. Useful for debugging. 
        /// </summary>
        private void IterateSettings()
        {
            var settingsDict = new NSDictionary(NSBundle.MainBundle.PathForResource("Settings.bundle/Root.plist", null));

            var prefSpecifierArray = settingsDict["PreferenceSpecifiers"] as NSArray;
            foreach (var prefItem in NSArray.FromArray<NSDictionary>(prefSpecifierArray))
            {
                var key = (NSString)prefItem["Key"];
                if (key == null)
                    continue;

                var val = prefItem["DefaultValue"];

                Console.WriteLine(key.ToString());
                Console.WriteLine(val.ToString());

            }
        }

    }
}
