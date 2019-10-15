using DataAccess.Entities;
using DataAccess.Repositories;
using DeveloperBlogAPI.Messages;
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
            SetRepository();
            ResponseMessage response = new ResponseMessage();
            CommentEntity comment = repository.GetByID(id);
            if (comment == null) {
                response.Code = HttpStatusCode.NotFound;
                response.Body = "Delete failed!: ";
                return Json(response);
            }
            if (comment.Responses == null || comment.Responses.Count <= 0) {
                return base.Delete(id);
            }
            else {
                comment.Content = "-comment deleted-";
                repository.Save(comment);
                response.Code = HttpStatusCode.Accepted;
                response.Body = "Success!";

                return Json(response);
            }           
        }

        protected override void SetRepository() {
            repository = new CommentRepository();
        }

        protected override CommentInsertModel GetModelFromRequest(HttpContent content) {
            CommentInsertModel model =  base.GetModelFromRequest(content);
            UserRepository userRep = new UserRepository();
            model.AuthorID = userRep.GetIDByUserName(model.Author);
            model.TimePosted = DateTime.Now;
            return model;
        }
    }
}
