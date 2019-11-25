using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Helpers {
    public class PagerModel {
        private int pageSize = 10;

        public int Page { get; set; }

        public int PagesCount { get; set; }
        public bool Desending { get; set; }
        public string SortParameter { get; set; }

        public int PageSize {
            get { return pageSize; }
            set { pageSize = value; } }
    }
}