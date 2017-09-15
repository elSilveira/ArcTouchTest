using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using AppTesting.Models;
using AppTesting.Resources.adapters;
using Android.Content;
using System;

namespace AppTesting
{
    [Activity(Label = "Movie List", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected ListView lv, lvTop;
        List<Movie> movies = new List<Movie>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            StartApplication();
            

        }

        protected void StartApplication()
        {
            var movieAdapter = new MovieAdapter(this, 1);
            lv = (ListView)FindViewById(Resource.Id.lista);
            lv.Adapter = movieAdapter;
            lv.ItemClick += Lv_ItemClick;
            var movieAdapterTop = new MovieAdapter(this, 2);
            lvTop = (ListView)FindViewById(Resource.Id.listaTop);
            lvTop.Adapter = movieAdapterTop;
            lvTop.ItemClick += Lv_ItemTopClick;

        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var x = sender;
            var p = e.Position;
            var details = new Intent(this, typeof(DetailsActivity));
            details.PutExtra("id", Convert.ToInt32(e.Id));
            details.PutExtra("tipo", 1);
            StartActivity(details);
        }
        private void Lv_ItemTopClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var x = sender;
            var p = e.Position;
            var details = new Intent(this, typeof(DetailsActivity));
            details.PutExtra("id", Convert.ToInt32(e.Id));
            details.PutExtra("tipo", 2);
            StartActivity(details);
        }
    }
}

