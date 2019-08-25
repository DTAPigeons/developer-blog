using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Helpers {
    public class PagerModel {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool Desending { get; set; }
        public string SortParameter { get; set; }
    }
}