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
using DeveloperBlogAPI.Misc;

namespace DeveloperBlogAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Post")]
    public class PostController : BaseController<PostRepository, PostEntity, PostListModel, PostInsertModel, PostViewModel> {
        [Route("")]
        [HttpPost]
        public override IHttpActionResult GetAll() {
            HttpContent content = Request.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            PagerModel model = JsonConvert.DeserializeObject<PagerModel>(jsonContent);
            return GetAll(model);
        }

        [HttpGet]
        [Route("{id}")]
        public override IHttpActionResult GetByID(int id) {
            return base.GetByID(id);
        }


        [HttpPost]
        [Route("Save")]
        public override IHttpActionResult Save() {
            return base.Save();
        }

        [HttpPost]
        [AllowAnonymous]
        [AcceptVerbs("POST")]
        [Route("Save/{model}")]
        public override IHttpActionResult Save(PostInsertModel model) {
            return base.Save(model);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public override IHttpActionResult Delete(int id) {
            return base.Delete(id);
        }

        protected override void SetRepository() {
            repository = new PostRepository();
        }
    }
}
