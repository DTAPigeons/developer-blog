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
using System.Web.Http;

namespace DeveloperBlogAPI.Controllers
{
    [AllowAnonymous]
    public abstract class BaseController<TRepository,TEntity,TListModel,TInsertModel,TViewModel> : ApiController
        where TRepository : BaseRepository<TEntity>
        where TEntity : BaseEntity
        where TListModel : BaseModel<TEntity>
        where TInsertModel: BaseModel<TEntity>
        where TViewModel: BaseModel<TEntity>
        {
        private TRepository repository = (TRepository)Activator.CreateInstance<TRepository>();

        [AllowAnonymous]
        public virtual IHttpActionResult GetAll(int page, int pageSize, bool descending, string sortParameter) {
            List<TListModel> models = new List<TListModel>();

            foreach (TEntity entity in repository.GetAll(page, pageSize, descending, sortParameter)) {
                models.Add((TListModel)Activator.CreateInstance(typeof(TListModel),entity));
            }

            return Json(models);
        }

        [AllowAnonymous]
        public virtual IHttpActionResult GetByID(int id) {
            TEntity entity = repository.GetByID(id);
            return Json((TViewModel)Activator.CreateInstance(typeof(TViewModel),entity));
        }

        [HttpPost]
        public virtual IHttpActionResult Save(TInsertModel model) {
            ResponseMessage response = new ResponseMessage();
            if (!model.IsValid()) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Invalid post!";
                return Json(response);
            }

            try {
                TEntity entity = model.ToEntity();
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


        public virtual IHttpActionResult Save() {
            HttpContent content = Request.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            TInsertModel model = JsonConvert.DeserializeObject<TInsertModel>(jsonContent);
            ResponseMessage response = new ResponseMessage();
            if (!model.IsValid()) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Invalid model!";
            }

            try {
                TEntity entity = model.ToEntity();
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


        [HttpDelete]
        public virtual IHttpActionResult Delete(int id) {
            ResponseMessage response = new ResponseMessage();

            try {
                TEntity entity = repository.GetByID(id);
                repository.Delete(entity);
                response.Code = HttpStatusCode.Accepted;
                response.Body = "Deleted object with id = " + id + ".";
            }
            catch (Exception ex) {
                response.Code = HttpStatusCode.InternalServerError;
                response.Body = "Delete failed!: " + ex.Message;
                throw ex;
            }

            return Json(response);
        }
    }
}
