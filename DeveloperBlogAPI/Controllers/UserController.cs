using DataAccess.Entities;
using DataAccess.Repositories;
using DeveloperBlogAPI.Messages;
using DeveloperBlogAPI.Models.DeveloperBlogModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DeveloperBlogAPI.Controllers {
    public class UserController : ApiController {
        private UserRepository repository;

        public IHttpActionResult GetAll(int page = 1, int pageSize = 10, bool descending = true, string sortParameter = "User Name") {
            List<UserListModel> models = new List<UserListModel>();

            foreach (UserEntity entity in repository.GetAll(page, pageSize, descending, sortParameter)) {
                models.Add(new UserListModel(entity));
            }

            return Json(models);
        }

        public IHttpActionResult GetByID(int id) {
            return Json(new UserViewModel(repository.GetByID(id)));
        }

        [HttpPost]
        public IHttpActionResult Save(UserInsertModel model) {
            ResponseMessage response = new ResponseMessage();
            if (!model.IsValid()) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Invalid post!";
                return Json(response);
            }

            try {
                UserEntity entity = model.ToEntity();
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


        public IHttpActionResult Save() {
            HttpContent content = Request.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            UserInsertModel model = JsonConvert.DeserializeObject<UserInsertModel>(jsonContent);
            ResponseMessage response = new ResponseMessage();
            if (!model.IsValid()) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Invalid post!";
            }

            try {
                UserEntity entity = model.ToEntity();
                repository.Save(entity);
                response.Code = HttpStatusCode.Accepted;
                response.Body = "Success!";
            }
            catch (Exception ex) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Save failed!: " + ex.Message;
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            ResponseMessage response = new ResponseMessage();

            try {
                UserEntity entity = repository.GetByID(id);
                repository.Delete(entity);
                response.Code = HttpStatusCode.Accepted;
                response.Body = "Deleted object with id = " + id + ".";
            }
            catch (Exception ex) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Delete failed!: " + ex.Message;
            }

            return Json(response);
        }
    }
}