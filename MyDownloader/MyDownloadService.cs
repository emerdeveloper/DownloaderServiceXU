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
using Android.Util;

namespace MyDownloader
{
    [Service(Label = "MyDownloadService", Icon = "@drawable/Icon")]
    public class MyDownloadService : Service
    {
        const string tag = "MyDownloadService";

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(tag, "OnBind called");
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent,//The Intent used to start the service
            StartCommandFlags flags, //Indicates whether the service was restarted
            int startId)//A unique integer for this call to start the service
        {
            Log.Debug(tag, "OnStartCommand called");
            return StartCommandResult.RedeliverIntent;//Restarts with the original intent
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug(tag, "Service created");
        }

        public override void OnDestroy()
        {
            Log.Debug(tag, "Service destroyed");
        }
    }
}