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
using System.Collections;

namespace AppTesting.Models
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }

        public static Genre ConvertToGenre(Object obj)
        {
            try
            {
                Genre m = new Genre()
                {
                    id = Convert.ToInt32((obj as IDictionary)["id"]),
                    name = (obj as IDictionary)["name"].ToString()
                };
               return m;
            }
            catch
            {
                return null;
            };
        }
    }
}