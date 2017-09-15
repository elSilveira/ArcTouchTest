using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using AppTesting.Models;
using RestSharp;
using System.Net;
using System.Linq;

namespace AppTesting.Resources.adapters
{
    class MovieAdapter : BaseAdapter
    {

        Context context;
        Activity _activity;
        List<Genre> _genres = new List<Genre>();
        int _tipo = 1;

        public MovieAdapter(Activity activity, int tipo)
        {
            _activity = activity;
            _tipo = tipo;
            MovieRequest();
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return (_tipo == 1 ? Movie._ReleasedMovies : Movie._TopMovies )[position].id;
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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //MovieRequest(1);
            var view = convertView ?? _activity.LayoutInflater.Inflate(
                    (_tipo == 1 ? Resource.Layout.MovieItemAdapter : Resource.Layout.TopMovieItemAdapter), parent, false);

            //Android.Net.Uri url = Android.Net.Uri.Parse(_movies[position].poster_path);
            if (_tipo == 1)
            {
                var imageBitmap = GetImageBitmapFromUrl(Movie._ReleasedMovies[position].poster_path);
                view.FindViewById<ImageView>(Resource.Id.imgMovie).SetImageBitmap(imageBitmap);
                view.FindViewById<TextView>(Resource.Id.txtMovieName).Text = "Name: " + Movie._ReleasedMovies[position].title;
                string genres = "";
                foreach (var g in Movie._ReleasedMovies[position].genres) genres += (genres.Equals("") ? g.name.ToUpper() : ", " + g.name.ToUpper());
                view.FindViewById<TextView>(Resource.Id.txtMovieGenre).Text = "Genre: " + genres;
                view.FindViewById<TextView>(Resource.Id.txtMovieRelease).Text = "Release: " + Movie._ReleasedMovies[position].release_date.ToShortDateString();
            }
            else
            {
                var imageBitmap = GetImageBitmapFromUrl(Movie._TopMovies[position].poster_path);
                view.FindViewById<ImageView>(Resource.Id.imgTopMovie).SetImageBitmap(imageBitmap);
                view.FindViewById<TextView>(Resource.Id.txtTopMovieName).Text = "Name: " + Movie._TopMovies[position].title;
                view.FindViewById<TextView>(Resource.Id.txtTopMovieRate).Text = "Rate: " + Movie._TopMovies[position].vote_average;
                view.FindViewById<TextView>(Resource.Id.txtTopMovieRelease).Text = "Release: " + Movie._TopMovies[position].release_date.ToShortDateString(); 
            }

            return view;
        }

        protected void MovieRequest()
        {
            string url = "https://api.themoviedb.org/3/";
            string apikey = "1f54bd990f1cdfb230adb312546d765d";
            string search = "/search/movie?api_key=" + apikey + "&query=";
            string language = "en-US";
            string upcoming = "/movie/upcoming?api_key=" + apikey + "&language=" + language;
            string toprated = "/movie/top_rated?api_key=" + apikey + "&language=" + language;
            string urlRequest;
            var client = new RestClient("https://api.themoviedb.org/3/");

            var request = new RestRequest("/genre/movie/list?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US", Method.GET);
            var result = client.Execute<Results>(request);
            foreach (var x in result.Data.genres as List<Object>)
            {
                var mov = Genre.ConvertToGenre(x);
                if (mov != null) _genres.Add(mov);
            }
          
            for (var page = 1; page <= 2; page++)
            {
                urlRequest = url + (_tipo == 1 ? upcoming : toprated) ;
                request = new RestRequest(urlRequest, Method.GET);
                request.Resource = (_tipo == 1 ? upcoming : toprated) + "&page=" + page;
                request.RequestFormat = DataFormat.Json;
                var result2 = client.Execute<Results>(request);
                foreach (var x in result2.Data.results as List<Object>)
                {
                    var mov = Movie.ConvertToMovie(x, _genres);
                    if (mov != null)
                    {
                        if (_tipo == 1)
                        {
                            if (Models.Movie._ReleasedMovies == null) Models.Movie._ReleasedMovies = new List<Movie>();
                            Models.Movie._ReleasedMovies.Add(mov);
                        }
                        else
                        {
                            if (Models.Movie._TopMovies == null) Models.Movie._TopMovies = new List<Movie>();
                            Models.Movie._TopMovies.Add(mov);
                        }
                    }
                }
            }
            if(Models.Movie._TopMovies != null)
                Models.Movie._TopMovies = (from x in Models.Movie._TopMovies orderby x.vote_average descending select x).ToList();
            if (Models.Movie._ReleasedMovies != null)
                Models.Movie._ReleasedMovies = (from x in Models.Movie._ReleasedMovies orderby x.release_date descending select x).ToList();
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return _tipo == 1 ? Movie._ReleasedMovies.Count : Movie._TopMovies.Count;
            }
        }

    }

    class Adapter1ViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}