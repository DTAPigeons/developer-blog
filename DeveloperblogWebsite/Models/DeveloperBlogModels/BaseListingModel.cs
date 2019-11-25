using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeveloperblogWebsite.Helpers;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public class BaseListingModel<T, TEntity> 
        where T : BaseModel<TEntity>
        where TEntity: BaseEntity<TEntity>
        {
        public PagerModel Pager { get; set; }

        public List<T> Items { get; set; }


        public BaseListingModel() {

        }

        public BaseListingModel(int page) {
            Pager = new PagerModel();
            Pager.Page = page;
        }

        public BaseListingModel(PagerModel pager, List<T> items) {
            Pager = pager;
            Items = items;
        }
    }
}