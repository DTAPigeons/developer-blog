using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class PostViewModel:PostListModel {

        public virtual UserListModel Author { get; set; }
        public virtual List<ImageModel> Images { get; set; }
        public virtual List<CommentModel> Comments { get; set; }

        public PostViewModel(PostEntity entity):base(entity) {

            Author = new UserListModel(entity.Author);

            Images = new List<ImageModel>();
            Comments = new List<CommentModel>();

            foreach (ImageEntity image in entity.Images) {
                Images.Add(new ImageModel(image));
            }

            foreach (CommentEntity comment in entity.Comments) {
                Comments.Add(new CommentModel(comment));
            }
        }
    }
}