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

namespace AppTesting.Models
{
    public class Results
    {
        public Object results { get; set; }
        public Object genres { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public Object dates { get; set; }
        public int total_pages { get; set; }
    }
}