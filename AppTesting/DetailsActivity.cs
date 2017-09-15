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
using AppTesting.Models;
using System.Net;

namespace AppTesting
{
    [Activity(Label = "Details")]
    public class DetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            int id = this.Intent.GetIntExtra("id", 0);
            int tipo = Intent.GetIntExtra("tipo", 0);
            var idx = this.Intent.GetIntExtra("id", 0);
            if (id != 0)
            {
                SetContentView(Resource.Layout.Details);
                Movie m = (tipo == 1 ? Movie._ReleasedMovies : Movie._TopMovies).Where(x => x.id == id).FirstOrDefault();
                var imageBitmap = GetImageBitmapFromUrl(m.poster_path);
                FindViewById<ImageView>(Resource.Id.imgMovieDetail).SetImageBitmap(imageBitmap);
                string genres = "";
                foreach (var g in m.genres) genres += (genres.Equals("") ? g.name.ToUpper() : ", " + g.name.ToUpper());
                FindViewById<TextView>(Resource.Id.txtMovieGenreDetail).Text = "Genre: " + genres;
                FindViewById<TextView>(Resource.Id.txtMovieNameDetail).Text = m.original_title;
                FindViewById<TextView>(Resource.Id.txtMovieRealNameDetail).Text = m.original_title;
                FindViewById<TextView>(Resource.Id.txtMovieOverviewDetail).Text = "Overview: " + m.overview;
                FindViewById<TextView>(Resource.Id.txtMovieReleaseDetail).Text = "Release: " + m.release_date.ToShortDateString();
                FindViewById<TextView>(Resource.Id.txtMovieRateDetail).Text = "Rate: " + m.vote_average;
            }
        }

        private Android.Graphics.Bitmap GetImageBitmapFromUrl(string url)
        {
            Android.Graphics.Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = Android.Graphics.BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}