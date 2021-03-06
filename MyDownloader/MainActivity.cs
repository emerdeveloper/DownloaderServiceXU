﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace MyDownloader
{
    [Activity(Label = "MyDownloader", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        DownloadReceiver receiver;
        ProgressBar progressBar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            FindViewById<Button>(Resource.Id.buttonStart).Click += ButtonStartClick;
            FindViewById<Button>(Resource.Id.buttonCancel).Click += ButtonCancelClick;
        }

        protected override void OnResume()
        {
            base.OnResume();

            var filter = new IntentFilter("DownloadServiceFilter");
            filter.AddAction("DownloadComplete");

            receiver = new DownloadReceiver();
            receiver.DownloadComplete += ReceiverDownloadComplete;

            RegisterReceiver(receiver, filter);
        }

        private void ReceiverDownloadComplete(object sender, EventArgs e)
        {
            progressBar.Indeterminate = false;
        }

        void ButtonStartClick(object sender, System.EventArgs e)
        {
            progressBar.Indeterminate = true;

            var intent = new Intent(this, typeof(MyDownloadService));
            intent.PutExtra("LoopCount", 8);
            StartService(intent);
        }

        void ButtonCancelClick(object sender, System.EventArgs e)
        {
            progressBar.Indeterminate = false;

            var intent = new Intent(this, typeof(MyDownloadService));
            StopService(intent);
        }
    }
}