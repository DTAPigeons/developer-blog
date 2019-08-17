using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Entities;
using DataAccess.Repositories;
using DeveloperBlogAPI.Models.DeveloperBlogModels;
using Newtonsoft.Json;
using DeveloperBlogAPI.Messages;

namespace DeveloperBlogAPI.Controllers
{
    [AllowAnonymous]
    public class PostController : BaseController<PostRepository,PostEntity,PostListModel,PostInsertModel,PostViewModel>
    {
        public override IHttpActionResult GetAll(int page=1, int pageSize = 10, bool descending = true, string sortParameter="Date") {
            return base.GetAll(page, pageSize, descending, sortParameter);
        }

        public override IHttpActionResult GetByID(int id) {
            return base.GetByID(id);
        }

        public override IHttpActionResult Save() {
            return base.Save();
        }

        [HttpPost]
        public override IHttpActionResult Save(PostInsertModel model) {
            return base.Save(model);
        }

        public override IHttpActionResult Delete(int id) {
            return base.Delete(id);
        }
    }
}
