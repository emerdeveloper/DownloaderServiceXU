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

namespace MyDownloader
{
    public class DownloadReceiver : BroadcastReceiver
    {
        public event EventHandler<EventArgs> DownloadComplete;
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.GetBooleanExtra("DownloadComplete", false))
                DownloadComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}