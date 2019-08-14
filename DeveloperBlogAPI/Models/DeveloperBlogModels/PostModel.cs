using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class PostModel : BaseModel<PostEntity> {
        public int AuthorID { get; set; }
        public DateTime TimePosted { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        [MaxLength]
        public string Content { get; set; }
        public int Views { get; set; }

        public virtual UserModel Author { get; set; }
        public virtual List<ImageModel> Images { get; set; }
        public virtual List<CommentModel> Comments { get; set; }

        public PostModel() {

        }

        public PostModel(PostEntity entity):base(entity) {
            AuthorID = entity.AuthorID;
            TimePosted = entity.TimePosted;
            Title = entity.Title;
            Content = entity.Content;
            Views = entity.Views;

            Author = new UserModel(entity.Author);

            Images = new List<ImageModel>();
            Comments = new List<CommentModel>();

            foreach(ImageEntity image in entity.Images) {
                Images.Add(new ImageModel(image));
            }

            foreach(CommentEntity comment in entity.Comments) {
                Comments.Add(new CommentModel(comment));
            }
        }

        public override PostEntity ToEntity() {
            PostEntity entity = new PostEntity();
            entity.ID = ID;
            entity.AuthorID = AuthorID;
            entity.TimePosted = TimePosted;
            entity.Title = Title;
            entity.Content = Content;
            entity.Views = Views;
            return entity;
        }
    }
}