using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class UserModel : BaseModel<UserEntity> {
        [StringLength(100)]
        public string UserName { get; set; }
        public string Role { get; set; }

        public virtual List<PostModel> Posts { get; set; }
        public virtual List<CommentModel> Comments { get; set; }

        public UserModel() {

        }

        public UserModel(UserEntity entity):base(entity) {
            UserName = entity.UserName;
            Role = entity.Role;

            Posts = new List<PostModel>();
            Comments = new List<CommentModel>();

            foreach(PostEntity post in entity.Posts) {
                Posts.Add(new PostModel(post));
            }

            foreach(CommentEntity comment in entity.Comments) {
                Comments.Add(new CommentModel(comment));
            }
        }

        public override UserEntity ToEntity() {
            UserEntity entity = new UserEntity();
            entity.ID = ID;
            entity.UserName = UserName;
            entity.Role = Role;
            return entity;
        }
    }
}