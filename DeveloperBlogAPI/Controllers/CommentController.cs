using DataAccess.Entities;
using DataAccess.Repositories;
using DeveloperBlogAPI.Misc;
using DeveloperBlogAPI.Models.DeveloperBlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeveloperBlogAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Comment")]
    public class CommentController : BaseController<CommentRepository,CommentEntity,CommentListModel,CommentInsertModel,CommentListModel>
    {
        [Route("")]
        [HttpPost]
        public override IHttpActionResult GetAll() {
            return base.GetAll(new PagerModel());
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
        public override IHttpActionResult Save(CommentInsertModel model) {
            return base.Save(model);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public override IHttpActionResult Delete(int id) {
            return base.Delete(id);
        }

        protected override void SetRepository() {
            repository = new CommentRepository();
        }
    }
}
