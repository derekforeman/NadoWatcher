using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UIKit;
using NadoWatch.Helpers;
using Plugin.Connectivity;

namespace NadoWatch
{
    public partial class ViewController : UIViewController
    {
        public bool DoIHaveInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GetData();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private async Task<bool> GetData()
        {
            if(DoIHaveInternet()) { 
                var url = Settings.GeneralSettings;
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
            return false;
        }
    }
}
