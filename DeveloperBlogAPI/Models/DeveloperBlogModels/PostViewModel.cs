using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class PostViewModel:PostListModel {

        public virtual List<ImageModel> Images { get; set; }
        public virtual List<CommentListModel> Comments { get; set; }

        public PostViewModel(PostEntity entity):base(entity) {

            Images = new List<ImageModel>();
            Comments = new List<CommentListModel>();

            foreach (ImageEntity image in entity.Images) {
                Images.Add(new ImageModel(image));
            }

            foreach (CommentEntity comment in entity.Comments) {
                Comments.Add(new CommentListModel(comment));
            }
        }
    }
}