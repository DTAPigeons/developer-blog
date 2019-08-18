using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class UserViewModel:UserListModel {
        public virtual List<PostListModel> Posts { get; set; }
        public virtual List<CommentListModel> Comments { get; set; }

        public UserViewModel(UserEntity entity) : base(entity) {
            Posts = new List<PostListModel>();
            Comments = new List<CommentListModel>();

            foreach (PostEntity post in entity.Posts) {
                Posts.Add(new PostViewModel(post));
            }

            foreach (CommentEntity comment in entity.Comments) {
                Comments.Add(new CommentListModel(comment));
            }
        }
    }
}