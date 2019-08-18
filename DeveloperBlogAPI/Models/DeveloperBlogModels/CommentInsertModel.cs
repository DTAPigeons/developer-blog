using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class CommentInsertModel:CommentListModel {
        public override CommentEntity ToEntity() {
            CommentEntity entity = base.ToEntity();
            entity.TimePosted = DateTime.Now;
            return entity;
        }
    }
}