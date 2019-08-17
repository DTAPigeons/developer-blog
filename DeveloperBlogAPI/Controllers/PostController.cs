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
    public class PostController : ApiController
    {
        private PostRepository repository = new PostRepository();  
        
        [AllowAnonymous]
        public IHttpActionResult GetAll(int page = 1, int pageSize = 10, bool descending = true, string sortParameter = "Date") {
            List<PostListModel> models = new List<PostListModel>();

            foreach(PostEntity entity in repository.GetAll(page, pageSize, descending, sortParameter)) {
                models.Add(new PostViewModel(entity));
            }

            return Json(models);
        }

        [AllowAnonymous]
        public IHttpActionResult GetByID(int id) {
            return Json(new PostViewModel(repository.GetByID(id)));
        }

        [HttpPost]
        public IHttpActionResult Save(PostInsertModel model) {
            ResponseMessage response = new ResponseMessage();
            if (!model.IsValid()) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Invalid post!";
                return Json(response);
            }

            try {
                PostEntity entity = model.ToEntity();
                repository.Save(entity);
                response.Code = HttpStatusCode.Accepted;
                response.Body = "Success!";
            }
            catch (Exception ex) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Save failed!: " + ex.Message;
                throw ex;
            }

            return Json(response);
        }

        /*
        public IHttpActionResult Save() {
            HttpContent content = Request.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            PostModel model = JsonConvert.DeserializeObject<PostModel>(jsonContent);
            ResponseMessage response = new ResponseMessage();
            if (!model.IsValid()) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Invalid post!";
            }

            try {
                PostEntity entity = model.ToEntity();
                repository.Save(entity);
                response.Code = HttpStatusCode.Accepted;
                response.Body = "Success!";
            }
            catch(Exception ex) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Save failed!: "+ex.Message;
            }

            return Json(response);
        }

    */
        [HttpDelete]
    public IHttpActionResult Delete(int id) {
            ResponseMessage response = new ResponseMessage();

            try {
                PostEntity entity = repository.GetByID(id);
                repository.Delete(entity);
                response.Code = HttpStatusCode.Accepted;
                response.Body = "Deleted object with id = " + id + ".";
            }
            catch(Exception ex) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Delete failed!: " + ex.Message;
            }

            return Json(response);
        }
    }
}
