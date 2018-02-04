using System;
using System.Collections.Generic;
using System.Text;

namespace MySampleApplication
{
    public class rowdetails
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imageHref { get; set; }
    }
    public class ItemsViewModel
    {
        public string title { get; set; }
        public List<rowdetails> rows { get; set; }
    }
}
