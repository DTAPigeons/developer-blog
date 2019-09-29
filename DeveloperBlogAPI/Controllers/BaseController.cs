using DataAccess.Entities;
using DataAccess.Repositories;
using DeveloperBlogAPI.Messages;
using DeveloperBlogAPI.Misc;
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
        where TEntity : BaseEntity<TEntity>
        where TListModel : BaseModel<TEntity>
        where TInsertModel: BaseModel<TEntity>
        where TViewModel: BaseModel<TEntity>
        {
        protected TRepository repository;

        [AllowAnonymous]
        [HttpPost]
        public abstract IHttpActionResult GetAll();

        protected abstract void SetRepository();

        protected virtual IHttpActionResult GetAll(PagerModel pagerModel) {
            SetRepository();
            List<TListModel> models = new List<TListModel>();

            foreach (TEntity entity in repository.GetAll(pagerModel.Page, pagerModel.PageSize, pagerModel.Desending, pagerModel.SortParameter)) {
                models.Add((TListModel)Activator.CreateInstance(typeof(TListModel),entity));
            }

            return Json(models);
        }

        [AllowAnonymous]
        [HttpGet]
        public virtual IHttpActionResult GetByID(int id) {
            SetRepository();
            TEntity entity = repository.GetByID(id);
            TViewModel model = (TViewModel)Activator.CreateInstance(typeof(TViewModel), entity);
            return Json(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual IHttpActionResult Save(TInsertModel model) {
            SetRepository();
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

        [HttpPost]
        [AllowAnonymous]
        public virtual IHttpActionResult Save() {
            SetRepository();
            TInsertModel model = GetModelFromRequest(Request.Content);
            ResponseMessage response = new ResponseMessage();
            if (model == null || !model.IsValid()) {
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

        protected virtual TInsertModel GetModelFromRequest(HttpContent content) {
            string jsonContent = content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TInsertModel>(jsonContent);
        }

        [HttpDelete]
        public virtual IHttpActionResult Delete(int id) {
            SetRepository();
            ResponseMessage response = new ResponseMessage();

            try {
                repository.Delete(id);
                response.Code = HttpStatusCode.OK;
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
