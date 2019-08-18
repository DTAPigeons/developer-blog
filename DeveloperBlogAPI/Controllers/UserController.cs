﻿using DataAccess.Entities;
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
    [AllowAnonymous]
    [RoutePrefix("api/User")]
    public class UserController : BaseController<UserRepository,UserEntity,UserListModel,UserInsertModel,UserViewModel> {
        [Route("")]
        [HttpGet]
        public override IHttpActionResult GetAll(int page = 1, int pageSize = 10, bool descending = true, string sortParameter = "Date") {
            return base.GetAll(page, pageSize, descending, sortParameter);
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
        public override IHttpActionResult Save(UserInsertModel model) {
            return base.Save(model);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public override IHttpActionResult Delete(int id) {
            return base.Delete(id);
        }
    }
}