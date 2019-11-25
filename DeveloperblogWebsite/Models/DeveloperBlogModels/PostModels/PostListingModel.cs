using DataAccess.Entities;
using DeveloperblogWebsite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels.PostModels {
    public class PostListingModel: BaseListingModel<PostListedModel, PostEntity> {
        public PostListingModel(PagerModel pager, List<PostListedModel> items) : base(pager, items) {

        }
    }
}