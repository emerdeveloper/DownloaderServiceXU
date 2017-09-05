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
using System.Threading.Tasks;

namespace MyDownloader
{
    [Service(Label = "MyDownloadService", Icon = "@drawable/Icon")]
    public class MyDownloadService : Service
    {
        const string tag = "MyDownloadService";
        bool isDownloaded;
        bool isCancelled;

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(tag, "OnBind called");
            Toast.MakeText(this, "OnBind called", ToastLength.Short).Show();
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent,//The Intent used to start the service
            StartCommandFlags flags, //Indicates whether the service was restarted
            int startId)//A unique integer for this call to start the service
        {
            Log.Debug(tag, "OnStartCommand called");
            Toast.MakeText(this, "OnStartCommand called", ToastLength.Short).Show();
            //Get Value intent
            var steps = intent.GetIntExtra("LoopCount", 10);
            isDownloaded = false;
            isCancelled = false;
            //MOCK logic
            //int steps = 15;

            Task.Run(() =>
            {
                for (int i = 0; i < steps && isCancelled == false; i++)
                {
                    int percent = 100 * (i + 1) / steps;
                    var msg = String.Format("[{0}] download in progress: {1}% complete", startId, percent);
                    Log.Debug(tag, msg);

                    Java.Lang.Thread.Sleep(500);
                }

                if (isCancelled == false)
                {
                    isDownloaded = true;
                    StopSelf();

                    Intent broadcast = new Intent();
                    broadcast.SetAction("DownloadServiceFilter");
                    broadcast.PutExtra("DownloadComplete", true);
                    SendBroadcast(broadcast);
                }
            });

            /*for (int i = 0; i < steps; i++)
            {
                int percent = 100 * (i + 1) / steps;
                var msg = String.Format("[{0}] download in progress: {1}% complete", startId, percent);
                Log.Debug(tag, msg);

                Java.Lang.Thread.Sleep(500);
            }*/

            return StartCommandResult.RedeliverIntent;//Restarts with the original intent
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Toast.MakeText(this, "Service created", ToastLength.Short).Show();
            Log.Debug(tag, "Service created");
        }

        public override void OnDestroy()
        {
            isCancelled = true;

            if (isDownloaded)
                Toast.MakeText(this, "Download Complete", ToastLength.Long).Show();
            else
                Toast.MakeText(this, "Download Cancelled", ToastLength.Long).Show();

            Toast.MakeText(this, "Service destroyed", ToastLength.Short).Show();
            Log.Debug(tag, "Service destroyed");
        }
    }
}