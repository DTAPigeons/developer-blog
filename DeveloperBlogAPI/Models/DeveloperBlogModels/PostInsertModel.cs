﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class PostInsertModel: PostListModel {
        public int AuthorID { get; set; }
        public override PostEntity ToEntity() {
            PostEntity entity = base.ToEntity();
            entity.AuthorID = AuthorID;
            entity.ID = -1;
            entity.Views = 0;
            entity.TimePosted = DateTime.Now;
            return entity;
        }
    }
}