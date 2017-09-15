using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppTesting.Models
{
    

    public class Movie
    {
        public double vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<Genre> genres{ get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public DateTime release_date { get; set; }
        public static List<Movie> _TopMovies { get; set; }
        public static List<Movie> _ReleasedMovies { get; set; }
        public static Movie ConvertToMovie(Object obj, List<Genre> genres)
        {
            try
            {
                Movie m = new Movie()
                {
                    vote_count = Convert.ToDouble((obj as IDictionary)["vote_count"]),
                    id = Convert.ToInt32((obj as IDictionary)["id"]),
                    video = Convert.ToBoolean((obj as IDictionary)["video"]),
                    vote_average = Convert.ToDouble((obj as IDictionary)["vote_average"]),
                    title = (obj as IDictionary)["title"].ToString(),
                    popularity = Convert.ToDouble((obj as IDictionary)["popularity"]),
                    poster_path = "http://image.tmdb.org/t/p/w185//" + (obj as IDictionary)["poster_path"].ToString(),
                    original_language = (obj as IDictionary)["original_language"].ToString(),
                    original_title = (obj as IDictionary)["original_title"].ToString(),                    
                    backdrop_path = (obj as IDictionary)["backdrop_path"].ToString(),
                    adult = Convert.ToBoolean((obj as IDictionary)["adult"]),
                    overview = (obj as IDictionary)["overview"].ToString(),
                    release_date = Convert.ToDateTime((obj as IDictionary)["release_date"])
                };
                m.genres = new List<Genre>();
                var listId = (obj as IDictionary)["genre_ids"] as List<Object>;
                for (var p = 0; p < listId.Count(); p++)
                {
                    var teste = (from g in genres where g.id == Convert.ToInt32(listId[p]) select g).FirstOrDefault();
                    Genre g2 = (from g in genres where g.id == Convert.ToInt32(listId[p]) select g).FirstOrDefault() as Genre;
                    m.genres.Add((from g in genres where g.id == Convert.ToInt32(listId[p]) select g).FirstOrDefault() as Genre);
                }
                    
                return m;
            } catch (Exception err){
                return null;
            };
        }
    }


}