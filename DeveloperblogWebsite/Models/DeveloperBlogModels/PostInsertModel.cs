﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public class PostInsertModel: PostListModel {
        /*
        public override PostEntity ToEntity() {
            PostEntity entity = base.ToEntity();
            entity.ID = -1;
            entity.Views = 0;
            entity.TimePosted = DateTime.Now;
            return entity;
        }
        */
    }
}